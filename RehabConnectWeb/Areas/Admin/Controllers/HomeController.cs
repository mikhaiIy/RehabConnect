using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
  public IActionResult Index() => View();
}

