using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.CustomerSupport.Controllers;

[Area("CustomerSupport")]
public class CsDashboardsController : Controller
{

  public IActionResult CsDashboard() => View();
}
