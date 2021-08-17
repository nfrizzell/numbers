using System;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;

using MySql.Data;
using MySql.Data.MySqlClient;

using numbers.Models;

namespace numbers
{
	public class NumbersDBContext
	{
		private readonly string dbConnString;
		public readonly MySqlConnection dbConnection;

		public NumbersDBContext(IConfiguration configuration)
		{
			this.dbConnString = configuration["DBConnectionString"];

			this.dbConnection = new MySqlConnection(dbConnString);
			dbConnection.Open();
		}

		private bool ValidateInput(string input, int max)
		{
			// Check for non-digit characters
			foreach (char c in input)	
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
			}

			if (input.Length == 0 || Int32.Parse(input) > max)
			{
				return false;
			}

			return true;
		}

		public FormResult CheckNumberPrime(string input)
		{
			if (String.IsNullOrEmpty(input))
			{
				return new FormResult("", "");
			}

			if (!ValidateInput(input, 1000000))
			{

				return new FormResult("Invalid input", "");
			}

			string sql = "SELECT EXISTS (SELECT * FROM prime WHERE prime_num=@integer);";
			MySqlCommand command = new MySqlCommand(sql, dbConnection);
			command.Parameters.AddWithValue("@integer", input);

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					bool value = (Int64)reader.GetInt64(0) != 0;
					if (value)
					{
						return new FormResult(input, "Prime");
					}

					else
					{
						return new FormResult(input, "Not prime");
					}
				}
			}

			return new FormResult("Error", "");
		}

		public FormResult RetrieveFactorial(string input)
		{
			if (String.IsNullOrEmpty(input))
			{
				return new FormResult("", "");
			}

			if (!ValidateInput(input, 10000))
			{
				return new FormResult("Invalid input", "");
			}

			string sql;

			int number = int.Parse(input);
			if (number < 3000)
			{
				sql = "SELECT value FROM factorial WHERE input=@integer;";
			}
			else
			{
				sql = "SELECT value FROM big_factorial WHERE input=@integer;";
			}

			MySqlCommand command = new MySqlCommand(sql, dbConnection);
			command.Parameters.AddWithValue("@integer", input);

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					string value = reader.GetString(0);
					return new FormResult(input, value);
				}
			}

			return new FormResult("Error", "");
		}
	}
}