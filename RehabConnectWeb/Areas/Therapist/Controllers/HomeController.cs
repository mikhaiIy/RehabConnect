using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.Models;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Therapist.Controllers;

[Area("Therapist")]
[Authorize(Roles = SD.Role_Therapist)]
public class HomeController : Controller
{
  private readonly IWebHostEnvironment _webHostEnvironment;

  public HomeController(IWebHostEnvironment webHostEnvironment)
  {
    _webHostEnvironment = webHostEnvironment;
  }

  public IActionResult Index()
  {
    string wwwRootPath = _webHostEnvironment.WebRootPath;
    string imagesPath = Path.Combine(wwwRootPath, "img", "announcements");

    var imageFiles = Directory.GetFiles(imagesPath).Select(Path.GetFileName).ToList();

    var announcements = imageFiles.Select(fileName => new Announcement
    {
      ImageUrl = Path.Combine("/img/announcements", fileName), // Adjust as per your folder structure
      Title = Path.GetFileNameWithoutExtension(fileName), // Example title from file name
      Description = "Description for " + Path.GetFileNameWithoutExtension(fileName) // Example description
    }).ToList();

    return View(announcements);
  }
}
