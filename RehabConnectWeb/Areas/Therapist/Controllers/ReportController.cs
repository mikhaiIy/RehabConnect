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
      List<Report> objReportList = _unitOfWork.Report.GetAll(includeProperties: "Student").ToList();
      return View(objReportList);
    }
    
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(Report obj)
    {
      //if (ModelState.IsValid)
      //{
      //  _unitOfWork.Report.Add(obj);
      //  _unitOfWork.Save();
      //  TempData["success"] = "Report created successfully";
      //  return RedirectToAction("Index");
      //}
      //else
      //{
      //  return View(obj);
      //}
      _unitOfWork.Report.Add(obj);
      _unitOfWork.Save();
      TempData["success"] = "Report created successfully";
      return RedirectToAction("Index");
    }

    public IActionResult Edit(int? reportid)
    {
      if (reportid == null || reportid == 0)
      {
        return NotFound();
      }
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == reportid);
      //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
      //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

      if (reportFromDb == null)
      {
        return NotFound();
      }
      return View(reportFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Report obj)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Report.Update(obj);
        _unitOfWork.Save();
        TempData["success"] = "Report updated successfully";
        return RedirectToAction("Index");
      }
      return View();
    }
  }
}
