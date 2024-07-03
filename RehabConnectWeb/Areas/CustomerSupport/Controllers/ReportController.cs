using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
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
      var studentList = _unitOfWork.Student.GetAll();

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

    public IActionResult Print(int? id)
    {
      var sessionId = _unitOfWork.Report.Find(u => u.ReportID == id).Select(u => u.SessionID).FirstOrDefault();
      var studentId = _unitOfWork.Session
        .Find(u => u.SessionID == sessionId, includeProperties: "StudentProgram")
        .Select(u => u.StudentProgram.StudentID).FirstOrDefault();

      var reportvm = new ReportVM()
      {
        Report = _unitOfWork.Report.Get(u=>u.ReportID==id),
        Student = _unitOfWork.Student.Get(u=>u.StudentID==studentId, includeProperties:"Therapist")
      };
      return View(reportvm);
    }
    //
    public IActionResult Info(int? id)
    {
      var sessionId = _unitOfWork.Report.Find(u => u.ReportID == id).Select(u => u.SessionID).FirstOrDefault();
      var studentId = _unitOfWork.Session
        .Find(u => u.SessionID == sessionId, includeProperties: "StudentProgram")
        .Select(u => u.StudentProgram.StudentID).FirstOrDefault();

      var reportvm = new ReportVM()
      {
        Report = _unitOfWork.Report.Get(u=>u.ReportID==id),
        Student = _unitOfWork.Student.Get(u=>u.StudentID==studentId, includeProperties:"Therapist")
      };
      return View(reportvm);
    }

    public IActionResult Confirm(int? id)
    {
      var sessionId = _unitOfWork.Report.Find(u => u.ReportID == id).Select(u => u.SessionID).FirstOrDefault();
      var studentId = _unitOfWork.Session
        .Find(u => u.SessionID == sessionId, includeProperties: "StudentProgram")
        .Select(u => u.StudentProgram.StudentID).FirstOrDefault();

      var program = _unitOfWork.Session
        .Find(u => u.SessionID == sessionId, includeProperties: "StudentProgram")
        .Select(u => u.StudentProgram.ProgramID).FirstOrDefault();

      var step = _unitOfWork.Program.Find(u => u.ProgramID == program, includeProperties: "Step").Select(u=>u.StepId).FirstOrDefault();

      var roadmap = _unitOfWork.Step.Find(u => u.RoadmapId == step).Select(u=>u.RoadmapId).FirstOrDefault();

      var reportvm = new ReportVM()
      {
        Report = _unitOfWork.Report.Get(u=>u.ReportID==id),
        Student = _unitOfWork.Student.Get(u=>u.StudentID==studentId, includeProperties:"Therapist"),
        Program = _unitOfWork.Program.Get(u => u.ProgramID == program),
        Step = _unitOfWork.Step.Get(u => u.StepId == step),
        Roadmap = _unitOfWork.Roadmap.Get(u => u.RoadmapId == roadmap)
      };

      if (id == null || id == 0)
      {
        //create
        return View(reportvm);
      }
      else
      {
        //update
        reportvm.Report = _unitOfWork.Report.Get(u => u.ReportID == id);
        return View(reportvm);
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
