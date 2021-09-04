using System;
using System.Numerics;

using Microsoft.Extensions.Configuration;

using MySql.Data.MySqlClient;

namespace numbers
{
	public abstract class FactorialHandler
	{
		public const string ERROR = "Too large or invalid input";
		public readonly UInt64 maxInput = 0x10000;

		public abstract BigInteger CalculateFactorial(UInt64 input);
	}

	public class RuntimeFactorialHandler : FactorialHandler
	{
		public RuntimeFactorialHandler()
		{
		}

		public override BigInteger CalculateFactorial(UInt64 input)
		{
			var factorial = new System.Numerics.BigInteger(input);
			for (UInt64 i = input-1; i > 0; i--)
			{
				factorial *= i;
			}

			return factorial;
		}
	}

	public class DBFactorialHandler : FactorialHandler
	{
		private readonly string dbConnString;

		DBFactorialHandler(IConfiguration configuration)
		{
			this.dbConnString = configuration["DBConnectionString"];
		}

		public override BigInteger CalculateFactorial(UInt64 input)
		{
			string sql;

			// Factorial code is segmented into two different tables:
			// a small table storing values < 3000 in the form of a VARCHAR
			// and a BLOB table that stores very large factorials on the disk
			if (input < 3000)
			{
				sql = "SELECT value FROM factorial WHERE input=@integer;";
			}
			else
			{
				sql = "SELECT value FROM big_factorial WHERE input=@integer;";
			}

			BigInteger factorial = 0;
			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				MySqlCommand command = new MySqlCommand(sql, dbConnection);
				command.Parameters.AddWithValue("@integer", input);

				using (MySqlDataReader reader = command.ExecuteReader())
				{
					// Check query results
					while (reader.Read())
					{
						// Retrieve the factorial value
						factorial = BigInteger.Parse(reader.GetString(0));
					}
				}
			}

			return factorial;
		}
	}
}