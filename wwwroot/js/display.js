function hide_sidebar()
{
	var grid = document.getElementById("grid-container");
	var sidebar = document.getElementById("sidebar-container");
	var show_button = document.getElementById("show-button");

	grid.style.gridTemplateColumns = "0px auto auto";
	sidebar.style.visibility = "hidden";
	show_button.style.visibility = "visible";
}

function show_sidebar()
{
	var grid = document.getElementById("grid-container");
	var sidebar = document.getElementById("sidebar-container");
	var show_button = document.getElementById("show-button");

	grid.style.gridTemplateColumns = "200px auto auto";
	sidebar.style.visibility = "visible";
	show_button.style.visibility = "hidden";
}