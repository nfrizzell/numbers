namespace numbers.Models
{
	public class FormResult
	{
		public string input {get; set;}
		public string result {get; set;}

		public FormResult(string input, string result)
		{
			this.input = input;
			this.result = result;
		}
	}
}