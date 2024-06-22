using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

namespace RehabConnectWeb.Areas.Therapist.Controllers
{
  [Area("Therapist")]
  public class ReportController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public ReportController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      return View();
    }
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Report obj)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Report.Add(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
      }
      return View();
    }
  }
}
