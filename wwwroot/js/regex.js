// Credit to https://www.rexegg.com/regex-quickstart.html for the very helpful guide

const tokens = 
{
	// Parse in the order presented
	characters: 
	{
		"\d" : "digit", 
		"\w" : "character",
		"\s" : "whitespace",
		"\D" : "non-digit",
		"\W" : "non-character",
		"\S" : "non-whitespace",
		"." : "any character except linebreak",
	},

	quantifiers: 
	{
		"+" : "1+",
		"*" : "any number of",
		"?" : "0 or 1"
	},

	logic:
	{
		"|" : "or",
		""
	}

}

function ParseRegex(expression)
{
	scIndices = [];
	

	for (let i = 0; i < expression.length; i++)
	{
		char = expression.charAt(i);
	}
}