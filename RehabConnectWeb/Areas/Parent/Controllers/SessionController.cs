using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;

namespace RehabConnectWeb.Areas.Parent.Controllers;

[Area("Parent")]
public class SessionController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public SessionController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index()
  {

    return View();
  }

  public IActionResult SessionEdit(int? scheduleId)
  {
    ViewBag.scheduleId = scheduleId;
    return View();
  }

  [HttpPost]
  public IActionResult SessionEdit()
  {
    return View();
  }

  #region API CALLS
    // -- Get Schedule for Parent Calendar --
      [HttpGet]
      public IActionResult GetSchedule()
      {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


        // getting all students related to the userId
        var studentList = _unitOfWork.Student.Find(u => u.UserId == userId).Select(u => u.StudentID);

        List<int> scheduleIds = new List<int>();
        foreach (var studentId in studentList)
        {
          var scheduleId = _unitOfWork.Session.Find(u => u.StudentProgram.StudentID == studentId, includeProperties:"StudentProgram").Select(u=>u.ScheduleID);

          scheduleIds.AddRange(scheduleId);
        }

        List<Schedule>? schedulesList = new List<Schedule>();
        foreach (var scheduleID in scheduleIds)
        {
          var schedule = _unitOfWork.Schedule.Get(u => u.ScheduleID == scheduleID, includeProperties: "Program");

          schedulesList.Add(schedule);
        }

        // var schedulesList = _unitOfWork.Schedule.GetAll(includeProperties:"Program").ToList();

        var schedules = new List<CalendarVM>();

        foreach (var obj in schedulesList)
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

      [HttpGet]
      public IActionResult GetStudentSchedule()
      {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // getting all students related to the userId -> Many
        var studentList = _unitOfWork.Student.Find(u => u.UserId == userId);

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
            // schedules.Add(schedule);
            var calendar = new CalendarVM
            {
              id = schedule.ScheduleID,
              title = _unitOfWork.Program.Find(p=>p.ProgramID==studentPrograms.ProgramID).Select(p=>p.ProgramName).FirstOrDefault(),
              start = schedule.Date.ToDateTime(schedule.StartTime),
              end = schedule.Date.ToDateTime(schedule.EndDTime),
              ExtendedProps = new ExtendedProps
              {
                Calendar = category,
                Capacity = schedule.Capacity,
                Registered = schedule.Registered,
                Students = obj.ChildName
              }
            };
            calendars.Add(calendar);
          }
        }
        return Json(new {events = calendars});
      }
  #endregion
}


