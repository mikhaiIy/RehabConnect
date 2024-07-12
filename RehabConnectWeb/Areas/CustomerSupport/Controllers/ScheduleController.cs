using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;


namespace RehabConnectWeb.Areas.CustomerSupport.Controllers;

[Area("CustomerSupport")]
[Authorize(Roles = SD.Role_CustomerSupport)]
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

  #region APICALLS
[HttpGet]
      public IActionResult GetStudentSchedule()
      {
        // getting all students related to the userId -> Many
        var studentList = _unitOfWork.Student.GetAll(includeProperties:"Therapist");

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
                Students = obj.ChildName,
                Therapist = obj.Therapist.TherapistName
              }
            };
            calendars.Add(calendar);
          }
        }
        return Json(new {events = calendars});
      }
  #endregion
}
