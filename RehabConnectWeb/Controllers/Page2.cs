using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcStarter.Controllers;

public class Page2 : Controller
{
  public IActionResult Index() => View();
}
