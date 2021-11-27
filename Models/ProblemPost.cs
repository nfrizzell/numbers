namespace numbers.Models
{
	public class ProblemPost
	{
		public string title {get; set;}
		public string problem {get; set;}
		public string remarks {get; set;}
		public string solution {get; set;}
		public string code {get; set;}

		public ProblemPost()
		{
			this.title = "Page not found";
			this.problem = "";
			this.remarks = "";
			this.solution = "";
			this.code = "";
		}

		public ProblemPost(string title, string problem, string code)
		{
			this.title = title;
			this.problem = problem;
			this.code = code;
		}
	}
}