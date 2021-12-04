using System;
using System.Numerics;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Hosting;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace numbers
{

	// Provides interface for classes to implement methods of calculating/retrieving
	// relevant values
	public class NumbersService
	{
		private NumbersInputValidator validator;
		private FactorialHandler factorialHandler;
		private PrimeHandler primeHandler;
		private PermutationHandler permutationHandler;
		private CountHandler countHandler;

		public NumbersService(IConfiguration configuration, IWebHostEnvironment env)
		{
			this.validator = new NumbersInputValidator();
			this.factorialHandler = new RuntimeFactorialHandler();
			this.primeHandler = new RuntimePrimeHandler();
			this.permutationHandler = new PermutationHandler(env);
			this.countHandler = new CountHandler(configuration);
		}

		public string CalculateFactorial(string strInput) 
		{ 
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, factorialHandler.maxInput))
			{
				return FactorialHandler.ERROR;
			}

			UInt64 input = validator.ConvertStringToInt(strInput);
			BigInteger factorial = factorialHandler.CalculateFactorial(input);
			return factorial.ToString();
		}

		public string CheckPrime(string strInput) 
		{
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, primeHandler.maxInput))
			{
				return PrimeHandler.ERROR;
			}

			UInt64 input = validator.ConvertStringToInt(strInput);
			bool prime = primeHandler.CheckPrime(input);
			if (prime)
			{
				return PrimeHandler.PRIME;
			}

			else
			{
				return PrimeHandler.COMPOSITE;
			}
		}

		public string ListPermutations(string strInput)
		{
			if (String.IsNullOrEmpty(strInput))
			{
				return "";
			}

			if (!validator.ValidateNumberInput(strInput, permutationHandler.maxInput))
			{
				return PermutationHandler.ERROR;
			}

			int input = (int)validator.ConvertStringToInt(strInput);
			return permutationHandler.ListPermutations(input);
		}

		public ulong Count(ulong increment, string ipaddr)
		{
			const int limit = 1000;
			if (increment > limit)
			{
				countHandler.Blacklist(ipaddr);
			}

			if (!countHandler.CheckSpam(ipaddr))
			{
				return countHandler.Increment(increment, ipaddr);
			}

			return 0;
		}
	}
}
