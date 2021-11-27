const endpoint = "/api/count/";
const totalDisplay = document.getElementById("count");

let increment = 0;
let total = parseInt(totalDisplay.textContent);

function count()
{
	increment++;
	total++;
	totalDisplay.textContent = total;
}

async function retrieveTotal()
{
	let query = endpoint + increment;
	increment = 0;
	let response = await fetch(query);
	total = parseInt(await response.text());
	totalDisplay.textContent = total;
}
retrieveTotal();
let heartbeat = setInterval(retrieveTotal, 5000);