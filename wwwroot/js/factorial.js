const numberField = document.getElementById("number-field");
const resultField = document.getElementById("result-field");

function factorial()
{
	num = parseInt(numberField.value);

	let result = 1;
	for (i = 1; i <= num; i++)
	{
		result *= i;
	}

	resultField.value = result.toString();
}