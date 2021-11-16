class DisplayBox
{
	containerDiv = document.getElementById("displaybox");

	backgroundColorHeader = "lightsteelblue";
	backgroundColor1 = "#F0FFF0";
	backgroundColor2 = "white";

	constructor()
	{
		this.Reset();
	}

	AddItem(line)
	{
		let newDiv = document.createElement("div");

		newDiv.innerHTML = line;
		newDiv.classList.add("display-box-row")

		// Color operators
		if (line == "AND" || line == "OR")
		{
			newDiv.style.color = "darkorange";
		}

		// Color italicized lines
		if (line.length > 0 && line[0] === "<")
		{
			if (line.includes("group"))
			{
				newDiv.style.color = "red";
			}

			else if (line.includes("set"))
			{
				newDiv.style.color = "magenta";
			}

			else if (line.includes("interval"))
			{
				newDiv.style.color = "magenta";
			}
		}

		if (this.containerDiv.childElementCount == 0)
		{
			newDiv.style.backgroundColor = this.backgroundColorHeader;
		}

		else if (this.containerDiv.childElementCount % 2)
		{
			newDiv.style.backgroundColor = this.backgroundColor1;
		}

		else
		{
			newDiv.style.backgroundColor = this.backgroundColor2;
		}

		this.containerDiv.appendChild(newDiv);
	}

	ShowError()
	{
		let newDiv = document.createElement("div");
		newDiv.innerHTML = "Invalid input";
		newDiv.classList.add("display-box-row");
		newDiv.style.backgroundColor = "tomato";
		newDiv.style.borderColor = "red";
		this.containerDiv.appendChild(newDiv);
	}	

	Reset()
	{
		this.containerDiv.innerHTML = "";
	}

}

displayBox = new DisplayBox();