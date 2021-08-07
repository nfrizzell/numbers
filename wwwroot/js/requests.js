document.getElementById("Trigger").onclick = CheckInput;

function CheckInput()
{
    let numStr = document.getElementById("Input").value;
    // Check if the field string has any non-numeric characters
    let isNum = /^\d+$/.test(numStr);

    document.getElementById("isPrime").hidden = true;
    document.getElementById("notPrime").hidden = true;

    if (isNum)
    {
        const http = new XMLHttpRequest();
        const url = "https://localhost:5001/Number/" + numStr;
        http.open("GET", url);
        http.send();

        if (parseInt(numStr) % 2)
        {
            document.getElementById("notPrime").hidden = false;
        }
        else
        {
            document.getElementById("isPrime").hidden = false;
        }

        http.onreadystatechange=(e)=>
        {
            console.log(http.responseText);
        }
    }

}