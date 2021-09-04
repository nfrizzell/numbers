using System;

using Microsoft.Extensions.Configuration;

using MySql.Data.MySqlClient;

namespace numbers
{
	
	public abstract class PrimeHandler
	{

		public const string ERROR = "Too large or invalid input";
		public const string PRIME = "Prime";
		public const string COMPOSITE = "Composite (not prime)";

		public readonly UInt64 maxInput = 792606555396977;

		public abstract bool CheckPrime(UInt64 input);	
	}

	public class RuntimePrimeHandler : PrimeHandler
	{
		public RuntimePrimeHandler()
		{
		}

		public override bool CheckPrime(UInt64 input)
		{
			if (input == 2 || input == 3)	
			{
				return true;
			}

			// Easy test for eliminating many non-prime numbers
			if (input <= 1 || input % 2 == 0 || input % 3 == 0)
			{
				return false;
			}

			// Check integers in the set of potential primes
			for (UInt64 i = 5; i * i <= input; i += 6)
			{
				if (input % i == 0 || input % (i + 2) == 0)
				{
					return false;
				}
			}

			return true;
		}
	}

	public class DBPrimeHandler : PrimeHandler
	{
		private readonly string dbConnString;

		public DBPrimeHandler(IConfiguration configuration)
		{
			this.dbConnString = configuration["DBConnectionString"];
		}

		public override bool CheckPrime(UInt64 input)
		{
			string sql = "SELECT EXISTS (SELECT * FROM prime WHERE prime_num=@integer);";

			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				// Prepared statement, more efficient and secure
				MySqlCommand command = new MySqlCommand(sql, dbConnection);
				command.Parameters.AddWithValue("@integer", input);

				using (MySqlDataReader reader = command.ExecuteReader())
				{
					// Check query results
					while (reader.Read())
					{
						// If query result is "true"/prime (MySQL does not have a direct boolean type)
						bool value = (UInt64)reader.GetUInt64(0) != 0;
						if (value)
						{
							return true;
						}

						else
						{
							return false;
						}
					}
				}
			}

			return false;
		}
	}
}