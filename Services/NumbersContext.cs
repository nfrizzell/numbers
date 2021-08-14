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

		private bool ValidateInput(string input, int maxLen)
		{
			if (input.Length > maxLen || input.Length == 0)
			{
				return false;
			}

			// Check for non-digit characters
			foreach (char c in input)	
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
			}

			return true;
		}

		public string CheckNumberPrime(string input)
		{
			if (!ValidateInput(input, 100))
			{
				return "Invalid input";
			}

			string sql = "SELECT EXISTS (SELECT * FROM prime WHERE VALUE=@integer);";
			MySqlCommand command = new MySqlCommand(sql, dbConnection);
			command.Parameters.AddWithValue("@integer", input);

			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					bool value = (Int64)reader.GetValue(0) != 0;
					if (value)
					{
						return "Number is prime";
					}

					else
					{
						return "Number is not prime";
					}
				}
			}

			return "Error";
		}
	}
}