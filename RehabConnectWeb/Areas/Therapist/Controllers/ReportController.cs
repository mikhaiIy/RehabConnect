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
      // therapisId
      var userEmail = User.FindFirstValue(ClaimTypes.Email);

      // StudentIds
      var studentList = _unitOfWork.Student
        .Find(u => u.Therapist.TherapistEmail == userEmail);
      // for Scoreboards
      var studentCount = studentList.ToList().Count;
      var sessionCount = _unitOfWork.Session.GetAll().ToList().Count;
      var confirmedReports = _unitOfWork.Report.Find(u => u.CustomerSupportConfirmation == true).ToList().Count();
      var reportCount = _unitOfWork.Report.GetAll().Count();

      var reportCsVm = new ReportCSVM
      {
        TableDatas = new List<TableData>(),
      };

      // populating the Scoreboard
      reportCsVm.SessionCount = sessionCount;
      reportCsVm.StudentCount = studentCount;
      reportCsVm.ConfirmedReport = confirmedReports;
      reportCsVm.ReportCount = reportCount;

      foreach (var student in studentList)
      {
        // Create a new TableData for this student
        var tableData = new TableData
        {
          ReportList = new List<ReportList>(),
          StudentDetail = new List<StudentDetail>()
        };
        // Find each Report for each Student
        var studentPrograms = _unitOfWork.StudentProgram.Find(u => u.StudentID == student.StudentID).ToList();

        var sessions = new List<Session>();
        foreach (var studentProgram in studentPrograms)
        {
          var session = _unitOfWork.Session.Find(u => u.StudentProgramId == studentProgram.StudentProgramId, includeProperties:"StudentProgram");
          sessions.AddRange(session);

          var reports = new List<Report>();
          foreach (var sessionObj in sessions)
          {
            var report = _unitOfWork.Report.Get(u => u.SessionID == sessionObj.SessionID);
            reports.Add(report);
          }

          // Find this Student Details at this studentProgram
          var studentDetails = _unitOfWork.Student.Get(u => u.StudentID == studentProgram.StudentID);
          var studentProgramInfo = _unitOfWork.StudentProgram
            .Get(u => u.StudentID == student.StudentID && u.Status == StudentStatus.Ongoing);
          var studentProgramDetail = _unitOfWork.Program.Get(u=>u.ProgramID==studentProgramInfo.ProgramID, includeProperties:"Step");
          var studentStepDetail = _unitOfWork.Step.Get(u => u.StepId == studentProgramDetail.StepId, includeProperties:"Roadmap");
          var studentRoadmapDetail = _unitOfWork.Roadmap.Get(u => u.RoadmapId == studentStepDetail.RoadmapId);

          tableData.ReportList.Add(new ReportList
          {
            Report=reports

          });
          tableData.StudentDetail.Add(new StudentDetail{
            Student = studentDetails,
            Program = studentProgramDetail,
            Step = studentStepDetail,
            Roadmap = studentRoadmapDetail,
            Status = studentProgramInfo.Status
          });
        }
        // Add tableData to reportCsVm
        reportCsVm.TableDatas.Add(tableData);
      }

      return View(reportCsVm);
    }


    // public IActionResult Upsert(int? sessionId)
    // {
    //   return View(); }

    //public IActionResult Index()
    //{
    //  List<Report> objReportList = _unitOfWork.Report.GetAll(includeProperties: "Student").ToList();
    //  return View(objReportList);
    //}

    public IActionResult Upsert(int? sessionId)
    {
      if (sessionId.HasValue)
      {
        var report = _unitOfWork.Report.Find(u => u.SessionID == sessionId).FirstOrDefault();

        //Finding
        var studentId = _unitOfWork.Session
          .Find(u => u.SessionID == sessionId, includeProperties: "StudentProgram")
          .Select(u => u.StudentProgram.StudentID).FirstOrDefault();

        var program = _unitOfWork.Session
          .Find(u => u.SessionID == sessionId, includeProperties: "StudentProgram")
          .Select(u => u.StudentProgram.ProgramID).FirstOrDefault();

        var step = _unitOfWork.Program.Find(u => u.ProgramID == program, includeProperties: "Step").Select(u=>u.StepId).FirstOrDefault();

        var roadmap = _unitOfWork.Step.Find(u => u.RoadmapId == step).Select(u=>u.RoadmapId).FirstOrDefault();

        ReportVM reportVm = new ReportVM
        {
          Student = _unitOfWork.Student.Get(u => u.StudentID == studentId),
          Program = _unitOfWork.Program.Get(u => u.ProgramID == program),
          Step = _unitOfWork.Step.Get(u => u.StepId == step),
          Roadmap = _unitOfWork.Roadmap.Get(u => u.RoadmapId == roadmap),
          Report = new Report()
        };

        if (report == null)
        {
          // Create New Report
          reportVm.Report.SessionID = sessionId;
          reportVm.Report.DateReport = DateTime.Now;
          return View(reportVm);
        }

        reportVm.Report = _unitOfWork.Report.Get(u => u.SessionID==sessionId);
        return View(reportVm);

      }

      return RedirectToAction("Index", "Report", new {Areas="Therapist"});
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
        return View(reportVM);
      }
    }
    //
    // //public IActionResult Upsert(int? id)
    // //{
    // //  ReportVM reportVM = new()
    // //  {
    // //    StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
    // //    {
    // //      Text = u.ChildName,
    // //      Value = u.StudentID.ToString()
    // //    }),
    // //    Report = new Report()
    // //  };
    // //  if (id == null || id == 0)
    // //  {
    // //    //create
    // //    reportVM.Report.DateReport = DateTime.Now;
    // //    reportVM.Report.DateReport = reportVM.Report.DateReport.AddSeconds(-reportVM.Report.DateReport.Second); // Remove seconds
    // //    return View(reportVM);
    // //  }
    // //  else
    // //  {
    // //    //update
    // //    reportVM.Report = _unitOfWork.Report.Get(u => u.ReportID == id);
    // //    return View(reportVM);
    // //  }
    //
    // //}
    //
    // //[HttpPost]
    // //[ValidateAntiForgeryToken]
    // //public IActionResult Upsert(ReportVM reportVM)
    // //{
    //
    // //  if (ModelState.IsValid)
    // //  {
    // //    if (reportVM.Report.ReportID == 0)
    // //    {
    // //      _unitOfWork.Report.Add(reportVM.Report);
    // //    }
    // //    else
    // //    {
    // //      _unitOfWork.Report.Update(reportVM.Report);
    // //    }
    // //    _unitOfWork.Save();
    // //    TempData["success"] = "Report created successfully";
    // //    return RedirectToAction("Index");
    // //  }
    // //  else
    // //  {
    // //    reportVM.StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
    // //    {
    // //      Text = u.ChildName,
    // //      Value = u.StudentID.ToString()
    // //    });
    // //    return View(reportVM);
    // //  }
    // //}
    //
    // public IActionResult Delete(int? id)
    // {
    //   if (id == null || id == 0)
    //   {
    //     return NotFound();
    //   }
    //
    //   var report = _unitOfWork.Report.Get(u => u.ReportID == id);
    //   if (report == null)
    //   {
    //     return NotFound();
    //   }
    //
    //   ReportVM reportVM = new()
    //   {
    //     Report = report,
    //     StudentList = _unitOfWork.Student.GetAll().Select(u => new SelectListItem
    //     {
    //       Text = u.ChildName,
    //       Value = u.StudentID.ToString()
    //     })
    //   };
    //
    //   return View(reportVM);
    // }
    //
    // [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    // public IActionResult DeletePOST(int? id)
    // {
    //   var report = _unitOfWork.Report.Get(u => u.ReportID == id);
    //   if (report == null)
    //   {
    //     return NotFound();
    //   }
    //
    //   _unitOfWork.Report.Remove(report);
    //   _unitOfWork.Save();
    //   TempData["success"] = "Report deleted successfully";
    //   return RedirectToAction("Index");
    // }
    //
    // //public IActionResult Upsert(int? id)
    // //{
    // //  if (id == null || id == 0)
    // //  {
    // //    PopulateSelectedStudent();
    // //    return View();
    // //  }
    // //  Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);
    //
    // //  if (reportFromDb == null)
    // //  {
    // //    return NotFound();
    // //  }
    // //  PopulateSelectedStudent(id);
    // //  return View(reportFromDb);
    // //}
    //
    // //[HttpPost]
    // //public IActionResult Upsert(Report obj)
    // //{
    // //  if (ModelState.IsValid)
    // //  {
    // //    _unitOfWork.Report.Update(obj);
    // //    _unitOfWork.Save();
    // //    TempData["success"] = "Report updated successfully";
    // //    return RedirectToAction("Index");
    // //  }
    // //  return View();
    // //}
    //
    // //private void PopulateSelectedStudent(int? id = null)
    // //{
    // //  var studentQuery = _unitOfWork.Student.GetAll().OrderBy(t => t.ChildName);
    // //  ViewBag.Student = new SelectList(studentQuery, "StudentID", "ChildName", id);
    // //}
  }
}
