// TODO: rewrite using state machine

class RegexParser
{
	metacharacters = ["(", ")", "[", "]", "{", "}", ".", "^", "$", "*", "\\"];

	groupPairs = 
	{
		"(":")", 
		"[":"]", 
		"{":"}"
	};

	// Tokens that do not require any special parsing methods
	semantics = 
	{
		// Operators/metacharacters
		"$" : "Previous pattern is at end of the string",
		"." : "Any character",
		"*" : "Previous pattern occurs zero or more times",
		"]" : "<i>--- end set ---</i>",
		"}" : "<i>--- end interval ---</i>",
	};

	whitespaceCharacters = [" ", "\t"];

	tokenBuffer = [];
	tokens = [];
	groupStack = [];
	groupCounter = 1;

	constructor()
	{

	}

	LexRegex()
	{
		let expression = document.getElementById("regex-input").value;
		expression.replace(/\s/g, "");

		displayBox.Reset();
		this.Reset();
		
		if (!this.CheckGroupBalance(expression))
		{
			displayBox.ShowError();
			return;
		}

		for (let i = 0; i < expression.length; i++)
		{
			let char = expression[i];

			if (!this.CheckGroupValidity(char))	
			{
				displayBox.ShowError();
				return;
			}

			if (this.metacharacters.includes(char))
			{
				// Backslash need special handling to grab the next char
				if (char == "\\")
				{
					this.tokenBuffer.push(char);
				}

				else
				{
					this.ClearBufferAndPush(char);
				}
			}

			else
			{
				this.tokenBuffer.push(char);
			}
		}

		this.ClearBufferAndPush();
		this.ParseRegex()
	}

	ParseRegex()
	{
		console.log(this.tokens);
		let translated = ["Match strings which follow:"];

		for (let i = 0; i < this.tokens.length; i++)
		{
			let token = this.tokens[i];

			// Normal metacharacter add
			if (Object.keys(this.semantics).includes(token))
			{
				translated.push(this.semantics[token]);
			}

			else if (this.ParseSpecialTokens(token, translated, i)) {}

			// String literal
			else
			{
				translated.push("\"" + token + "\"");

				if (i != this.tokens.length - 1 && !Object.values(this.groupPairs).includes(this.tokens[i+1]))
				{
					translated.push("AND");
				}
			}

			if (i != (this.tokens.length - 1) && Object.values(this.groupPairs).includes(token)
				&& Object.keys(this.groupPairs).includes(this.tokens[i+1]))
			{
				translated.push("AND");
			}
		}

		for (let i = 0; i < translated.length; i++)
		{
			let translatedLine = translated[i];
			displayBox.AddItem(translatedLine);
		}
	}

	ParseSpecialTokens(token, translated, index)
	{
		// Handle the ^ character
		if (token == "^")
		{
			if (index > 0 && this.tokens[index-1] == "[")
			{
				translated.pop(); translated.pop();
				translated.push("At least a single character NOT in the set");
				translated.push("<i>--- begin set ---</i>");
			}
			else
			{
				translated.push("Following expression at beginning of line");
			}
			return true;
		}

		else if (token == "(")
		{
			translated.push("<i>--- begin group #" + this.groupCounter + " ---</i>")
			return true;
		}

		else if (token == ")")
		{
			translated.push("<i>--- end group #" + this.groupCounter + " ---</i>")
			this.groupCounter++;
			return true;
		}

		else if (token == "{")
		{
			translated.push("Previous pattern must appear a number of times falling on the interval:");
			translated.push("<i>--- begin interval ---</n>")
			return true;
		}

		else if (token == "[")
		{
			translated.push("At least a single character in the set:");
			translated.push("<i>--- begin set ---</i>")
			return true;
		}

		else if (token == "\\")			
		{
			if (index != this.tokens.length - 1)
			{
				let next = this.tokens[index + 1];
				// Number
				if (next.match(/[0-9]/) && next < this.groupCounter)
				{
					translated.push()
				}

				else if (this.tokens[index + 1].match(/[0-9]/))
				{

				}
			}

			return true;
		}

		return false;
	}

	Reset()
	{
		this.tokenBuffer = [];
		this.tokens = [];
		this.groupStack = [];
		this.groupCounter = 1;
	}

	CheckGroupValidity(char)
	{
		if (Object.keys(this.groupPairs).includes(char))
		{
			this.groupStack.push(char);
		}

		else if (Object.values(this.groupPairs).includes(char))
		{
			if (this.groupPairs[this.groupStack.pop()] != char)
			{
				return false;
			}
		}

		return true;
	}

	CheckGroupBalance(expression)
	{
		let balance = 0;
		for (let i = 0; i < expression.length; i++)
		{
			let char = expression[i];

			if (["(", "[", "{"].includes(char))
			{
				balance++;
			}

			else if ([")", "]", "}"].includes(char))
			{
				balance--;
			}
		}

		return balance == 0;
	}

	ClearBufferAndPush(char)
	{
		if (this.tokenBuffer.length > 0)
		{
			let token = this.tokenBuffer.join("");
			this.tokens.push(token);
			this.tokenBuffer = [];
		}

		if (char != null)
		{
			this.tokens.push(char);
		}
	}
}

class POSIXRegexParser extends RegexParser
{
	constructor()
	{
		super();
	}
}

class ExtendedPOSIXRegexParser extends POSIXRegexParser
{
	constructor()
	{
		super();

		this.metacharacters.push("|")
		this.semantics["|"] = "OR";

		this.metacharacters.push("?")
		this.semantics["?"] = "Previous pattern occurs zero or one times (optional)";

		this.metacharacters.push("+")
		this.semantics["+"] = "Previous pattern occurs one or more times";
	}
}

class PCRERegexParser extends RegexParser
{
	constructor()
	{
		super();
	}
}

posix = new POSIXRegexParser();
posixExtended = new ExtendedPOSIXRegexParser();
pcre = new PCRERegexParser();

function Translate()
{
	let posixSelected = document.getElementById("posix").checked;
	let extendedPosixSelected = document.getElementById("eposix").checked;
	let PCRESelected = document.getElementById("pcre").checked;

	if (posixSelected)
	{
		posix.LexRegex();
	}

	else if (extendedPosixSelected)
	{
		posixExtended.LexRegex();
	}

	else
	{
		pcre.LexRegex();
	}
}