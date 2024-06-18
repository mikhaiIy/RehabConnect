using Microsoft.AspNetCore.Mvc;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class ScheduleController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }
  }
}
