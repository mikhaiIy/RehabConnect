using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.CustomerSupport.Controllers
{
  [Area("CustomerSupport")]
  [Authorize(Roles = SD.Role_CustomerSupport)]
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

    public IActionResult Print(int? id)
    {
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);
      return View(reportFromDb);
    }

    public IActionResult Info(int? id)
    {
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);
      return View(reportFromDb);
    }

    public IActionResult Confirm(int? id)
    {
      ReportVM reportVM = new()
      {
        StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
        {
          Text = u.ChildName,
          Value = u.StudentID.ToString()
        }),
        Report = new Report()
      };
      if (id == null || id == 0)
      {
        //create
        return View(reportVM);
      }
      else
      {
        //update
        reportVM.Report = _unitOfWork.Report.Get(u => u.ReportID == id);
        return View(reportVM);
      }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirm(ReportVM reportVM)
    {

      if (ModelState.IsValid)
      {
        if (reportVM.Report.ReportID == 0)
        {
          _unitOfWork.Report.Add(reportVM.Report);
        }
        else
        {
          _unitOfWork.Report.Update(reportVM.Report);
        }
        _unitOfWork.Save();
        TempData["success"] = "Report Confirm successfully";
        return RedirectToAction("Index");
      }
      else
      {
        reportVM.StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
        {
          Text = u.ChildName,
          Value = u.StudentID.ToString()
        });
        return View(reportVM);
      }
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
      List<RehabConnect.Models.Report> objReportList = _unitOfWork.Report.GetAll(includeProperties: "Student.ChildName").ToList();
      return Json(new { data = objReportList });
    }

    #endregion

  }
}
