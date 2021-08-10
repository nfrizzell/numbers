function HTTPGetRequest(value)
{
	const url = "http://localhost:5000";
	const http = new XMLHttpRequest();

	http.open("GET", url);
	http.send();

	http.onreadystatechange=(e)=>
	{
		console.log(http.responseText);
	}
}

function retrieveFactorial()
{
	const numberField = document.getElementById("number-field");
	const resultField = document.getElementById("result-field");

	const max = 99999;
	const input = numberField.value;
	if (input != null && /^\d+$/.test(input) && 0 <= parseInt(input) <= max)
	{
		response = HTTPGetRequest(input);
	}

	else
	{
		resultField.value = "Invalid input";
	}
}

function retrievePrime()
{
	const numberField = document.getElementById("number-field");
	const resultField = document.getElementById("result-field");

	const max = 0xFFFFFFFF;
	const input = numberField.value;
	if (input != null && /^\d+$/.test(input) && 0 <= parseInt(input) <= max)
	{
		response = HTTPGetRequest(input);
	}

	else
	{
		resultField.value = "Invalid input";
	}
}