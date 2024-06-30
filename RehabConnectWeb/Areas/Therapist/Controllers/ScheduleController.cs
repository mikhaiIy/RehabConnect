using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Packaging;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;

namespace RehabConnectWeb.Areas.Therapist.Controllers;


[Area("Therapist")]
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
        var studentIds = _unitOfWork.Student
          .Find(u => u.Therapist.TherapistEmail == userEmail, includeProperties: "Therapist")
          .Select(u => u.StudentID);

        // StudentProgram
        var studentPrograms = new List<StudentProgram>();

        foreach (var studentId in studentIds)
        {
          var studentProgram = new StudentProgram();
          studentProgram = _unitOfWork.StudentProgram.Get(u => u.StudentID == studentId && u.Status==StudentStatus.Ongoing);

          studentPrograms.Add(studentProgram);
        }

        var sessions  = new List<int>();
        foreach (var obj in studentPrograms)
        {
          var session = new List<int>();
          session = _unitOfWork.Session.Find(u => u.StudentProgramId == obj.StudentProgramId).Select(u=>u.ScheduleID).ToList();

          sessions.AddRange(session);
        }

        var scheduleList = new List<Schedule>();
        foreach (var ids in sessions)
        {
          var schedule = new List<Schedule>();
          schedule = _unitOfWork.Schedule.Find(u => u.ScheduleID == ids, includeProperties:"Program").ToList();

          scheduleList.AddRange(schedule);
        }


        // var studentPrograms1 = _unitOfWork.StudentProgram.Find(u=>u.StudentID==studentIds)
        //
        // IEnumerable<Schedule> schedulesList = _unitOfWork.Schedule.Find(includeProperties:"Program").ToList();

        var schedules = new List<CalendarVM>();

        foreach (var obj in scheduleList)
        {
          // Checking StepId
          string category = "";

          if (obj.Program.ProgramName == "Consultation")
          {
            category = "Consultation";
          }
          if (obj.Program.ProgramName == "Assessment")
          {
            category = "Assessment";
          }
          if (obj.Program.ProgramName == "Full Development Report")
          {
            category = "Report";
          }
          if (obj.Program.ProgramName.Contains("Program"))
          {
            category = "Program";
          }
          if (obj.Program.ProgramName.Contains("Ready to School"))
          {
            category = "School";
          }

          var schedule = new CalendarVM
          {
            id = obj.ScheduleID,
            title = _unitOfWork.Program.Find(p=>p.ProgramID==obj.ProgramID).Select(p=>p.ProgramName).FirstOrDefault(),
            start = obj.Date.ToDateTime(obj.StartTime),
            end = obj.Date.ToDateTime(obj.EndDTime),
            ExtendedProps = new ExtendedProps
            {
              Calendar = category,
              Capacity = obj.Capacity,
              Registered = obj.Registered
            }
          };
          schedules.Add(schedule);
        }
        return Json(new {events = schedules});
      }
  #endregion
}


