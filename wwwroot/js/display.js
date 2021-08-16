function toggleSidebar()
{
	const grid = document.getElementById("grid-container");
	const sidebar = document.getElementById("sidebar-container");

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

let width = window.innerWidth
|| document.documentElement.clientWidth
|| document.body.clientWidth;

if (width < 768)
{
	toggleSidebar();
}