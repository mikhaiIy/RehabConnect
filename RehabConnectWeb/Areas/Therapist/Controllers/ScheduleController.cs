using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Therapist.Controllers;


[Area("Therapist")]
[Authorize(Roles = SD.Role_Therapist)]
public class ScheduleController : Controller
{
  private readonly IUnitOfWork _unitOfWork;
  public ScheduleController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public IActionResult Index()
  {
    return View();
  }

  #region API CALLS

  [HttpGet]
  public IActionResult GetSchedule()
      {
        // Want find Specific Student of this Therapist
        // We only TherapistId, from this we can get StudentIds
        // StudentId -> StudentProgram -> Sessions -> Schedules

        // therapisId
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        // StudentIds
        var studentList = _unitOfWork.Student
          .Find(u => u.Therapist.TherapistEmail == userEmail);

        // Calendar List to be Pass to API
        var calendars = new List<CalendarVM>();
        foreach (var obj in studentList)
        {
          // Each student -> has Many Sessions
          var sessions = _unitOfWork.Session.Find(u => u.StudentProgram.StudentID == obj.StudentID);

          // Each Student -> has One StudentProgram
          var studentPrograms =
            _unitOfWork.StudentProgram.Get(u => u.StudentID == obj.StudentID && u.Status == StudentStatus.Ongoing, includeProperties:"Program");
          // We can get Its Program Name here
          string category = "";

          if (studentPrograms.Program.ProgramName == "Consultation")
          {
            category = "Consultation";
          }
          if (studentPrograms.Program.ProgramName == "Assessment")
          {
            category = "Assessment";
          }
          if (studentPrograms.Program.ProgramName == "Full Development Report")
          {
            category = "Report";
          }
          if (studentPrograms.Program.ProgramName.Contains("Program"))
          {
            category = "Program";
          }
          if (studentPrograms.Program.ProgramName.Contains("Ready to School"))
          {
            category = "School";
          }

          // Each Student -> could have Many Schedules, since Session many.
          var schedules = new List<Schedule>();

          //Second Loop Based on Those Many Session
          foreach (var session in sessions)
          {
            // Iterate through every Session, and get its Respective Schedule.
            var schedule = _unitOfWork.Schedule.Get(u => u.ScheduleID == session.ScheduleID);

            var report = _unitOfWork.Report.Find(u => u.SessionID == session.SessionID).FirstOrDefault();

            var reportStatus = "";

            if (report == null)
            {
              reportStatus = "Not Created";
            }
            else
            {
              var reportConfirmed = _unitOfWork.Report
                .Find(u => u.SessionID == session.SessionID && u.CustomerSupportConfirmation == true).FirstOrDefault();

              if (reportConfirmed == null)
              {
                reportStatus = "Created, but not Confirmed";
              }
              else
              {
                reportStatus = "Report Confirmed";
              }
            }

            // schedules.Add(schedule);
            var calendar = new CalendarVM
            {
              id = session.SessionID,
              title = _unitOfWork.Program.Find(p=>p.ProgramID==studentPrograms.ProgramID).Select(p=>p.ProgramName).FirstOrDefault(),
              start = schedule.Date.ToDateTime(schedule.StartTime),
              end = schedule.Date.ToDateTime(schedule.EndDTime),
              ExtendedProps = new ExtendedProps
              {
                Calendar = category,
                Capacity = schedule.Capacity,
                Registered = schedule.Registered,
                Students = obj.ChildName,
                ReportStatus = reportStatus
              }
            };
            calendars.Add(calendar);
          }
        }
        return Json(new {events = calendars});
      }
  #endregion
}


