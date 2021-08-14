function HTTPGetRequest(value, controller)
{
	const url = "https://localhost:5001/" + controller + "/" + value;
	const http = new XMLHttpRequest();

	http.open("GET", url, false);
	http.send();
	return http.responseText;
}

function retrieveFactorial()
{
	const numberField = document.getElementById("number-field");
	const resultField = document.getElementById("result-field");

	const max = 99999;
	const input = numberField.value;
	if (input != null && /^\d+$/.test(input) && 0 <= parseInt(input) <= max)
	{
		let response = HTTPGetRequest(input, "Factorial");
		resultField.value = response;
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
		let response = HTTPGetRequest(input, "Prime");
		resultField.value = response;
	}

	else
	{
		resultField.value = "Invalid input";
	}
}