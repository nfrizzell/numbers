using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;

using MySql.Data.MySqlClient;

namespace numbers
{
	public class PermutationHandler 
	{

		public const string ERROR = "Too large or invalid input";

		public readonly string precomputedPermPath;

		public readonly UInt64 maxInput = 8;

		public string[] precomputedPerms;

		public PermutationHandler(IWebHostEnvironment env)
		{
			this.precomputedPermPath = env.ContentRootPath + "/Data/permutations";
			this.precomputedPerms = new string[10];
			LoadPermutations();
		}

		public string ListPermutations(int input) { return precomputedPerms[input]; }

		private void LoadPermutations()
		{
			// Empty set
			precomputedPerms[0] = "[]";

			using (var sr = new StreamReader(precomputedPermPath))
			{
				string line = sr.ReadLine();
				for (int i = 1; line != null; i++)
				{
					precomputedPerms[i] = line;
					line = sr.ReadLine();
				}
			}
		}
	}
}