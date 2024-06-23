using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public IActionResult Edit(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);

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

    public IActionResult Upsert(int? id)
    {
      if (id == null || id == 0)
      {
        PopulateSelectedStudent();
        return View();
      }
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);

      if (reportFromDb == null)
      {
        return NotFound();
      }
      PopulateSelectedStudent(id);
      return View(reportFromDb);
    }

    [HttpPost]
    public IActionResult Upsert(Report obj)
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

    private void PopulateSelectedStudent(int? id = null)
    {
      var studentQuery = _unitOfWork.Student.GetAll().OrderBy(t => t.ChildName);
      ViewBag.Student = new SelectList(studentQuery, "StudentID", "ChildName", id);
    }
  }
}
