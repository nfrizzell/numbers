using System;
using System.Numerics;

using Microsoft.Extensions.Configuration;

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

		public NumbersService(IConfiguration configuration)
		{
			this.validator = new NumbersInputValidator();
			this.factorialHandler = new RuntimeFactorialHandler();
			this.primeHandler = new RuntimePrimeHandler();
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
			return ":)";
		}
	}
}