using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.Parent.Controllers;

public class PDashboardsController : Controller
{
  [Area("Parent")]
  public IActionResult PDashboard() => View();
}
