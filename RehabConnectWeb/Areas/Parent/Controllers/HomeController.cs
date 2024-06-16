using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.Parent.Controllers;

public class HomeController : Controller
{
  [Area("Parent")]
  public IActionResult Index() => View();
}
