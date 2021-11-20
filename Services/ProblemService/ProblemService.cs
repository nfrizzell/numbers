using System;
using System.IO;
using System.Numerics;
using System.Text.Json;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;

using MySql.Data;
using MySql.Data.MySqlClient;

using numbers.Models;

namespace numbers
{
	public class ProblemService
	{
		IConfiguration configuration;
		IWebHostEnvironment env;

		public ProblemService(IConfiguration configuration, IWebHostEnvironment env)
		{
			this.configuration = configuration;
			this.env = env;
		}

		public ProblemPost Load(string id)
		{
			ProblemPost post = new ProblemPost();

			int _;
			if (id.Length > 3 || !System.Int32.TryParse(id, out _))
			{
				return post;
			}

			//string json = JsonSerializer.Serialize(post);
			//Console.WriteLine(json);

			string path = this.env.ContentRootPath + "/Data/article/kattis/" + id + ".json";
			string json = File.ReadAllText(path);
			post = JsonSerializer.Deserialize<ProblemPost>(json);

			return post;
		}
	}
}