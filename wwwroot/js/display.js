function toggleSidebar()
{
	var grid = document.getElementById("grid-container");
	var sidebar = document.getElementById("sidebar-container");

	if (sidebar.style.visibility === "hidden")
	{
		sidebar.style.visibility = "visible";
		grid.style.gridTemplateColumns = "200px auto auto";
	}

	else
	{
		sidebar.style.visibility = "hidden";
		grid.style.gridTemplateColumns = "0px auto auto";
	}
}

var width = window.innerWidth
|| document.documentElement.clientWidth
|| document.body.clientWidth;

//var height = window.innerHeight
//|| document.documentElement.clientHeight
//|| document.body.clientHeight;

if (width < 768)
{
	toggleSidebar();
}