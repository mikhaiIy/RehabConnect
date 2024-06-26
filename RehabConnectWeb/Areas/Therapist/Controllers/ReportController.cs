using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;
using System.Security.Claims;

namespace RehabConnectWeb.Areas.Therapist.Controllers
{
  [Area("Therapist")]
  [Authorize(Roles = SD.Role_Therapist)]
  public class ReportController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public ReportController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      // Get the logged-in user's ID
      string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

      // Find the ApplicationUser associated with the logged-in user
      var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

      if (applicationUser == null)
      {
        return NotFound("User not found");
      }

      // Find the therapist associated with the ApplicationUser
      var therapist = _unitOfWork.Therapist.Get(t => t.TherapistID == applicationUser.TherapistID);

      if (therapist == null)
      {
        return NotFound("Therapist not found");
      }

      // Fetch reports for the therapist's students
      List<Report> objReportList = _unitOfWork.Report
          .GetAll(includeProperties: "Student")
          .Where(r => r.Student.TherapistID == therapist.TherapistID)
          .ToList();

      return View(objReportList);
    }

    //public IActionResult Index()
    //{
    //  List<Report> objReportList = _unitOfWork.Report.GetAll(includeProperties: "Student").ToList();
    //  return View(objReportList);
    //}

    public IActionResult Upsert(int? id)
    {
      // Get the logged-in user's ID
      string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

      // Find the ApplicationUser associated with the logged-in user
      var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);

      if (applicationUser == null)
      {
        return NotFound("User not found");
      }

      // Find the therapist associated with the ApplicationUser
      var therapist = _unitOfWork.Therapist.Get(t => t.TherapistID == applicationUser.TherapistID);

      if (therapist == null)
      {
        return NotFound("Therapist not found");
      }

      // Prepare the ReportVM
      ReportVM reportVM = new ReportVM
      {
        StudentList = _unitOfWork.Student
              .GetAll(s => s.TherapistID == therapist.TherapistID)
              .Select(s => new SelectListItem
              {
                Text = s.ChildName,
                Value = s.StudentID.ToString()
              })
              .ToList(),
        Report = id == null || id == 0 ?
              new Report { DateReport = DateTime.Now } : // Create new report with current date if id is null or 0
              _unitOfWork.Report.Get(u => u.ReportID == id) // Fetch existing report for update
      };

      // Remove seconds from DateReport to ensure consistency
      reportVM.Report.DateReport = reportVM.Report.DateReport.AddSeconds(-reportVM.Report.DateReport.Second);

      return View(reportVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ReportVM reportVM)
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
        TempData["success"] = "Report saved successfully";
        return RedirectToAction("Index");
      }
      else
      {
        // Reload student list in case of validation errors
        reportVM.StudentList = _unitOfWork.Student
            .GetAll(s => s.TherapistID == reportVM.Report.Student.TherapistID) // Assuming Student.TherapistID is set correctly
            .Select(s => new SelectListItem
            {
              Text = s.ChildName,
              Value = s.StudentID.ToString()
            })
            .ToList();
        return View(reportVM);
      }
    }

    //public IActionResult Upsert(int? id)
    //{
    //  ReportVM reportVM = new()
    //  {
    //    StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
    //    {
    //      Text = u.ChildName,
    //      Value = u.StudentID.ToString()
    //    }),
    //    Report = new Report()
    //  };
    //  if (id == null || id == 0)
    //  {
    //    //create
    //    reportVM.Report.DateReport = DateTime.Now;
    //    reportVM.Report.DateReport = reportVM.Report.DateReport.AddSeconds(-reportVM.Report.DateReport.Second); // Remove seconds
    //    return View(reportVM);
    //  }
    //  else
    //  {
    //    //update
    //    reportVM.Report = _unitOfWork.Report.Get(u => u.ReportID == id);
    //    return View(reportVM);
    //  }

    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Upsert(ReportVM reportVM)
    //{

    //  if (ModelState.IsValid)
    //  {
    //    if (reportVM.Report.ReportID == 0)
    //    {
    //      _unitOfWork.Report.Add(reportVM.Report);
    //    }
    //    else
    //    {
    //      _unitOfWork.Report.Update(reportVM.Report);
    //    }
    //    _unitOfWork.Save();
    //    TempData["success"] = "Report created successfully";
    //    return RedirectToAction("Index");
    //  }
    //  else
    //  {
    //    reportVM.StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
    //    {
    //      Text = u.ChildName,
    //      Value = u.StudentID.ToString()
    //    });
    //    return View(reportVM);
    //  }
    //}

    public IActionResult Delete(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }

      var report = _unitOfWork.Report.Get(u => u.ReportID == id);
      if (report == null)
      {
        return NotFound();
      }

      ReportVM reportVM = new()
      {
        Report = report,
        StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
        {
          Text = u.ChildName,
          Value = u.StudentID.ToString()
        })
      };

      return View(reportVM);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
      var report = _unitOfWork.Report.Get(u => u.ReportID == id);
      if (report == null)
      {
        return NotFound();
      }

      _unitOfWork.Report.Remove(report);
      _unitOfWork.Save();
      TempData["success"] = "Report deleted successfully";
      return RedirectToAction("Index");
    }

    //public IActionResult Upsert(int? id)
    //{
    //  if (id == null || id == 0)
    //  {
    //    PopulateSelectedStudent();
    //    return View();
    //  }
    //  Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);

    //  if (reportFromDb == null)
    //  {
    //    return NotFound();
    //  }
    //  PopulateSelectedStudent(id);
    //  return View(reportFromDb);
    //}

    //[HttpPost]
    //public IActionResult Upsert(Report obj)
    //{
    //  if (ModelState.IsValid)
    //  {
    //    _unitOfWork.Report.Update(obj);
    //    _unitOfWork.Save();
    //    TempData["success"] = "Report updated successfully";
    //    return RedirectToAction("Index");
    //  }
    //  return View();
    //}

    //private void PopulateSelectedStudent(int? id = null)
    //{
    //  var studentQuery = _unitOfWork.Student.GetAll().OrderBy(t => t.ChildName);
    //  ViewBag.Student = new SelectList(studentQuery, "StudentID", "ChildName", id);
    //}
  }
}
