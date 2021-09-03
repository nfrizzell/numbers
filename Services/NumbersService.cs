using System;

using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;

using MySql.Data;
using MySql.Data.MySqlClient;

using numbers.Models;

namespace numbers
{
	public class NumbersInputValidator 
	{
		public NumbersInputValidator()
		{

		}

		public bool ValidateNumberInput(string input, UInt64 max)
		{
			// Check for non-digit characters
			foreach (char c in input)	
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
			}

			if (input.Length == 0 || UInt64.Parse(input) > max)
			{
				return false;
			}

			return true;
		}
	}

	public abstract class NumbersService
	{
		protected UInt64 maxPrimeInput;
		protected UInt64 maxFactorialInput;

		protected NumbersInputValidator validator;
		protected const string PRIME = "Prime";
		protected const string NOT_PRIME = "Composite (not prime)";
		protected const string ERROR = "Too large or invalid input";

		public NumbersService(IConfiguration configuration)
		{
			this.validator = new NumbersInputValidator();
		}

		public abstract string CalculateFactorial(string strInput);

		public abstract string CheckPrime(string strInput);
	}

	public class NumbersOnDemand : NumbersService
	{
		public NumbersOnDemand(IConfiguration configuration) : base(configuration)
		{
			this.maxFactorialInput = 0x10000;
			this.maxPrimeInput = 792606555396977;
		}

		public override string CheckPrime(string strInput)
		{
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, maxPrimeInput))
			{
				return ERROR;
			}

			UInt64 input;
			if (!UInt64.TryParse(strInput, out input))
			{
				return ERROR;
			}

			if (input == 2 || input == 3)	
			{
				return PRIME;
			}

			if (input <= 1 || input % 2 == 0 || input % 3 == 0)
			{
				return NOT_PRIME;
			}

			for (UInt64 i = 5; i * i <= input; i += 6)
			{
				if (input % i == 0 || input % (i + 2) == 0)
					return NOT_PRIME;
			}

			return PRIME;
		}

		public override string CalculateFactorial(string strInput)
		{
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, maxFactorialInput))
			{
				return ERROR;
			}

			UInt64 input;
			if(!UInt64.TryParse(strInput, out input))
			{
				return ERROR;
			}

			var multiplicand = new System.Numerics.BigInteger(input);
			for (UInt64 i = input-1; i > 0; i--)
			{
				multiplicand *= i;
			}

			return multiplicand.ToString();
		}
	}

	public class NumbersDBContext : NumbersService
	{
		private readonly string dbConnString;

		public NumbersDBContext(IConfiguration configuration) : base(configuration)
		{
			this.maxFactorialInput = 10000;
			this.maxPrimeInput = 0xFFFFFF;

			this.dbConnString = configuration["DBConnectionString"];
		}

		public override string CheckPrime(string strInput)
		{
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, maxPrimeInput))
			{
				return ERROR;
			}

			string sql = "SELECT EXISTS (SELECT * FROM prime WHERE prime_num=@integer);";

			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				MySqlCommand command = new MySqlCommand(sql, dbConnection);
				command.Parameters.AddWithValue("@integer", strInput);
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						bool value = (UInt64)reader.GetUInt64(0) != 0;
						if (value)
						{
							return PRIME;
						}

						else
						{
							return NOT_PRIME;
						}
					}
				}
			}

			return ERROR;
		}

		public override string CalculateFactorial(string strInput)
		{
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, maxFactorialInput))
			{
				return ERROR;
			}

			string sql;

			int number = int.Parse(strInput);
			if (number < 3000)
			{
				sql = "SELECT value FROM factorial WHERE input=@integer;";
			}
			else
			{
				sql = "SELECT value FROM big_factorial WHERE input=@integer;";
			}

			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				MySqlCommand command = new MySqlCommand(sql, dbConnection);
				command.Parameters.AddWithValue("@integer", strInput);

				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						string value = reader.GetString(0);
						return value;
					}
				}
			}

			return ERROR;
		}
	}
}