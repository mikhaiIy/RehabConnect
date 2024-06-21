using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.CustomerSupport.Controllers
{
  [Area("Therapist")]
  public class ReportController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
