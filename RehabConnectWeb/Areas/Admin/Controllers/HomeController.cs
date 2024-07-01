using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;
using System;
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
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IWebHostEnvironment webHostEnvironment, IUnitOfWork unitOfWork)
    {
      _webHostEnvironment = webHostEnvironment;
      _unitOfWork = unitOfWork;
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

      var viewModel = new HomeVM
      {
        Announcements = announcements,
        ParentDetails = GetParentDetails(), // Call method to get ParentDetails
        Students = GetStudents(), // Call method to get Students
        Therapists = GetTherapists(),
        CustomerSupports = GetCustomerSupports()
      };

      return View(viewModel);
    }

    // Example method to retrieve ParentDetails (replace with your actual implementation)
    private List<ParentDetail> GetParentDetails()
    {
      // Example code to fetch ParentDetails from repository
      return _unitOfWork.ParentDetail.GetAll().ToList();
    }

    // Example method to retrieve Students (replace with your actual implementation)
    private List<Student> GetStudents()
    {
      // Example code to fetch Students from repository
      return _unitOfWork.Student.GetAll().ToList();
    }
    private List<RehabConnect.Models.CustomerSupport> GetCustomerSupports()
    {
      // Example code to fetch Students from repository
      return _unitOfWork.CustomerSupport.GetAll().ToList();
    }

    private List<RehabConnect.Models.Therapist> GetTherapists()
    {
      // Example code to fetch Students from repository
      return _unitOfWork.Therapist.GetAll().ToList();
    }

    public IActionResult Faq()
    {
      return View();
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
      try
      {
        int parentDetailCount = _unitOfWork.ParentDetail.GetAll().Count(); // Get total count of ParentDetails
        int studentCount = _unitOfWork.Student.GetAll().Count(); // Get total count of Students
        int therapistCount = _unitOfWork.Therapist.GetAll().Count();
        int customerSupportCount = _unitOfWork.CustomerSupport.GetAll().Count();

        return Json(new { parentDetailCount, studentCount, therapistCount, customerSupportCount });
      }
      catch (Exception ex)
      {
        return BadRequest(new { error = "Failed to fetch counts.", message = ex.Message });
      }
    }

    #endregion
  }
}
