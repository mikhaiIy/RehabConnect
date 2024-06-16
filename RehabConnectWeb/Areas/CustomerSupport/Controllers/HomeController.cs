using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.CustomerSupport.Controllers;

[Area("CustomerSupport")]
public class HomeController : Controller
{

  public IActionResult Index() => View();
}
