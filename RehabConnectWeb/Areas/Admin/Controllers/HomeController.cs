using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.Models;
using RehabConnect.Utility;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = SD.Role_Admin)]
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
}
