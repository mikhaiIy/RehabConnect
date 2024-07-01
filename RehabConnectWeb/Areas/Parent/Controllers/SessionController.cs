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
using Microsoft.AspNetCore.Authorization;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Parent.Controllers;

[Area("Parent")]
[Authorize(Roles = SD.Role_Parent)]
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

  public IActionResult SessionEdit(int? sessionId)
  {

    // We will get sessionId, it is 1:1
    var session = _unitOfWork.Session.Get(u => u.SessionID == sessionId, includeProperties:"StudentProgram");

    //
    var programId = _unitOfWork.StudentProgram.Find(u => u.StudentID == session.StudentProgram.StudentID && u.Status == StudentStatus.Ongoing).Select(p => p.ProgramID).FirstOrDefault();
    // next from the ProgramId, we'll need to find the single(FirstOrDefault) StepId(Select)
    var stepId = _unitOfWork.Program.Find(u=> u.ProgramID == programId).Select(a=>a.StepId).FirstOrDefault();
    // then with the StepId, we find its corresponding RoadmapId
    var roadmapId = _unitOfWork.Step.Find(u=>u.StepId == stepId).Select(p=>p.RoadmapId).FirstOrDefault();

    // Filtering only Available Schedule slot &&
    var schedules = _unitOfWork.Schedule.Find(u=>u.ProgramID==programId && u.Registered<u.Capacity)
      .Select(s => new
      {
        Date = s.Date.ToString("yyyy-MM-dd"),
        StartTime = s.StartTime.ToString(@"hh\:mm"),
        ScheduleID = s.ScheduleID
      })
      .ToList();
    if (!schedules.Any())
    {
      ViewBag.AlertType = "warning";
      ViewBag.AlertMessage = "There is no Slot Available. Please Contact Admin at 01X-XXX XXXX";
    }


    // We want to populate this vm
    EnrollProgramVM enrollProgramVm = new EnrollProgramVM()
    {
      StudentProgram = _unitOfWork.StudentProgram.Get(i => i.StudentID==session.StudentProgram.StudentID && i.Status==StudentStatus.Ongoing, includeProperties:"Student"),
      Schedule = _unitOfWork.Schedule.GetAll(),
      ProgramList = _unitOfWork.Program.Find(p => p.StepId == stepId && p.ProgramID == programId),
      ScheduleDataJson = JsonConvert.SerializeObject(schedules),

      // -- Header
      StepList = _unitOfWork.Step.Find(u=>u.RoadmapId==roadmapId),
      stepId = stepId
    };

    ViewBag.sessionId = sessionId;
    ViewBag.studentProgramId = _unitOfWork.Session.Find(u => u.SessionID == sessionId)
      .Select(u => u.StudentProgramId)
      .FirstOrDefault();
    return View(enrollProgramVm);
  }

  [HttpPost]
  public IActionResult SessionEdit(int studentProgramId, List<int> SessionTimes, int sessionId)
  {
    if (ModelState.IsValid)
    {

      foreach (var scheduleId in SessionTimes)
      {
        var session = new Session
        {
          SessionID = sessionId,
          StudentProgramId = studentProgramId,
          ScheduleID = scheduleId,
        };
        _unitOfWork.Session.Update(session);
      }
      _unitOfWork.Save();
    }
    return RedirectToAction("Index", "Session");
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
              id = session.SessionID,
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


