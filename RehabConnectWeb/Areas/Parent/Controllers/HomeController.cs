using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  public class HomeController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public HomeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
      _unitOfWork = unitOfWork;
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

    public IActionResult Add() => View();
    public IActionResult Edit() => View();
    public IActionResult Preview() => View();
    public IActionResult Print() => View();

  }
}


