using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Utility;
using System.Security.Claims;
using RehabConnect.Models.ViewModel;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  [Authorize(Roles = SD.Role_Parent)]
  public class ReportController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public ReportController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    //public IActionResult Index()
    //{
    //  List<Report> objReportList = _unitOfWork.Report.GetAll(includeProperties: "Student").ToList();
    //  return View(objReportList);
    //}

    public IActionResult Index()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var studentList = _unitOfWork.Student.GetAll(u=>u.UserId==userId);

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
            var report = _unitOfWork.Report.Get(u => u.SessionID == sessionObj.SessionID && u.CustomerSupportConfirmation==true);
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

  }
}
