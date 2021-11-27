using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;

using MySql.Data.MySqlClient;

namespace numbers
{
	public class CountHandler 
	{
		string dbConnString;

		public CountHandler(IConfiguration configuration)
		{
			this.dbConnString = configuration["DBConnectionString"];
		}

		public ulong Increment(ulong increment, string ipaddr)
		{
			DateTime dt = DateTime.Now;
			long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();

			string retrieve = "SELECT total FROM count WHERE id='count';";
			string update = "UPDATE count SET total=@integer WHERE id='count';";
			string timestamp = "INSERT INTO ratelimit VALUES(@string, false, @number) ON DUPLICATE KEY UPDATE last_request=@number;";
			ulong total = 0;

			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				MySqlCommand retrieveCommand = new MySqlCommand(retrieve, dbConnection);
				MySqlCommand updateCommand = new MySqlCommand(update, dbConnection);
				MySqlCommand timestampCommand = new MySqlCommand(timestamp, dbConnection);

				using (MySqlDataReader reader = retrieveCommand.ExecuteReader())
				{
					// Check query results
					while (reader.Read())
					{
						total = (UInt64)reader.GetUInt64(0);
					}
				}

				updateCommand.Parameters.AddWithValue("@integer", total + increment);
				updateCommand.ExecuteNonQuery();

				timestampCommand.Parameters.AddWithValue("@string", ipaddr);
				timestampCommand.Parameters.AddWithValue("@number", unixTime);
				timestampCommand.ExecuteNonQuery();
			}

			return total + increment;
		}

		public bool CheckSpam(string ipaddr)
		{
			string checkSpam = "SELECT blacklisted, last_request FROM ratelimit WHERE ipaddr=@string;";

			DateTime dt = DateTime.Now;
			long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();

			bool spam = false;
			long lastRequestTime = 0;

			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				MySqlCommand checkSpamCommand = new MySqlCommand(checkSpam, dbConnection);
				checkSpamCommand.Parameters.AddWithValue("@string", ipaddr);

				using (MySqlDataReader reader = checkSpamCommand.ExecuteReader())
				{
					// Check query results
					while (reader.Read())
					{
						spam = (bool)reader.GetBoolean(0);
						lastRequestTime = (long)reader.GetInt64(1);
					}
				}
			}

			return (spam || unixTime - lastRequestTime < 4);
		}

		public void Blacklist(string ipaddr)
		{
			DateTime dt = DateTime.Now;
			long unixTime = ((DateTimeOffset)dt).ToUnixTimeSeconds();

			string blacklist = "INSERT INTO ratelimit VALUES(@string, true, @number) ON DUPLICATE KEY UPDATE blacklisted=true, last_request=@number;";

			using (MySqlConnection dbConnection = new MySqlConnection(dbConnString))
			{
				dbConnection.Open();

				MySqlCommand blacklistCommand = new MySqlCommand(blacklist, dbConnection);
				blacklistCommand.Parameters.AddWithValue("@string", ipaddr);
				blacklistCommand.Parameters.AddWithValue("@number", unixTime);

				blacklistCommand.ExecuteNonQuery();
			}
		}
	}
}