function Convert()
{
	let input = document.getElementById("inputstr").value;
	let output = "";

	let conversions = {
		0: "\\0", 
		7: "\\a", 
		9: "\\b", 
		9: "\\t", 
		10: "\\n",
		11: "\\v",
		12: "\\f",
		13: "\\r",
		27: "\\e"
	};

	for (let i = 0; i < input.length; i++)
	{
		let char = input[i];
		let charCode = input.charCodeAt(i);

		console.log(charCode)
		if (charCode in conversions)
		{
			output += conversions[charCode];
		}

		else
		{
			output += char;
		}
	}

	let outputElement = document.getElementById("outputstr");
	outputElement.value = output;
}