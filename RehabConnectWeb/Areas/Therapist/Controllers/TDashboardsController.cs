using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.Therapist.Controllers;

[Area("Therapist")]
public class HomeController : Controller
{

  public IActionResult Index() => View();
}
