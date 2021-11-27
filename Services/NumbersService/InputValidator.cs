using System;

namespace numbers
{
	public class NumbersInputValidator 
	{
		public NumbersInputValidator()
		{
			// Add as needed
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

			// Make sure numeric value of input does not exceed desired max
			if (input.Length == 0 || UInt64.Parse(input) > max)
			{
				return false;
			}

			return true;
		}

		public UInt64 ConvertStringToInt(string input)
		{
			return UInt64.Parse(input);
		}
	}
}	