using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ADashboardsController : Controller
{
  public IActionResult ADashboard() => View();
}
