using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  [Authorize(Roles = SD.Role_Parent)]
  public class EnrollProgramController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _db;

    public EnrollProgramController(IUnitOfWork unitOfWork, ApplicationDbContext db)
    {
      _unitOfWork = unitOfWork;
      _db = db;
    }

    public IActionResult SelectChild()
    {
      var alertType = TempData["AlertType"] as string;
      var alertMessage = TempData["AlertMessage"] as string;

      // Check if the alert has been shown
      var alertShown = TempData["AlertShown"] as bool? ?? false;

      if (alertShown)
      {
        // Display the alert
        ViewBag.AlertType = alertType;
        ViewBag.AlertMessage = alertMessage;

        // Clear the flag to prevent showing the alert again
        TempData["AlertShown"] = false;
      }

      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

      var student = _unitOfWork.Student
          .Find(s => s.UserId == userId)
          .ToList();

      SelectChildVM vm = new SelectChildVM()
      {
        Students = _unitOfWork.Student.Get(i => i.UserId == userId),
        StudentList = student
      };

      return View(vm);
    }

    [HttpPost]
    public IActionResult SelectChild(int Students)
    {
      if (Students == 0)
      {
        // Handle the case where no student was selected (optional)
        ModelState.AddModelError("Students", "Please select a child.");
        return RedirectToAction("SelectChild");
      }

      return RedirectToAction("Index", new { id = Students });
    }

    public IActionResult Index(int id)
    {
      // we have studentId, from here we can get programId from StudentProgram
      // but we need to find the programId(Select) for the student with the attribute Status == ongoing(&&)
      // okay now the problem is that we are getting IEnum<int> of ProgramId, since 1 student could have many ProgramId (FirstOrDefault)
      var programId = _unitOfWork.StudentProgram.Find(u => u.StudentID == id && u.Status == StudentStatus.Ongoing).Select(p => p.ProgramID).FirstOrDefault();
      // next from the ProgramId, we'll need to find the single(FirstOrDefault) StepId(Select)
      var stepId = _unitOfWork.Program.Find(u=> u.ProgramID == programId).Select(a=>a.StepId).FirstOrDefault();
      // then with the StepId, we find its corresponding RoadmapId
      var roadmapId = _unitOfWork.Step.Find(u=>u.StepId == stepId).Select(p=>p.RoadmapId).FirstOrDefault();

      // Other
      var student = _unitOfWork.Student
        .Find(s => s.StudentID == id)
        .FirstOrDefault();

      // Check if the Parent has already Register Session More than the required Amount in; e.g. Program.NumOfSession = 3, then Parent can only Register 3 Session.
      // Getting NumOfSession in Program
      var numOfSession = _unitOfWork.Program.Find(u => u.ProgramID == programId).Select(u => u.NumOfSession)
        .FirstOrDefault();
      // Getting Number of Session the Parent has already Register/Enroll/Book
      var studentProgramId = _unitOfWork.StudentProgram.Find(u => u.StudentID == id && u.Status == StudentStatus.Ongoing).Select(p => p.StudentProgramId).FirstOrDefault();
      var sessionBooked = _unitOfWork.Session.Find(u => u.StudentProgramId == studentProgramId)
        .Count();

      if (sessionBooked<=numOfSession)
      {
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

        EnrollProgramVM enrollProgramVm = new EnrollProgramVM()
        {
          StudentProgram = _unitOfWork.StudentProgram.Get(i => i.Student.StudentID == id),
          Schedule = _unitOfWork.Schedule.GetAll(),
          ProgramList = _unitOfWork.Program.Find(p => p.StepId == stepId && p.ProgramID == programId),
          ScheduleDataJson = JsonConvert.SerializeObject(schedules),

          // -- Header
          StepList = _unitOfWork.Step.Find(u=>u.RoadmapId==roadmapId),
          stepId = stepId
        };
        return View(enrollProgramVm);
      }
      // Set SweetAlert message and type
      TempData["AlertType"] = "warning";
      TempData["AlertMessage"] = "You have booked all required sessions. Please edit a session if you want to make changes.";
      TempData["AlertShown"] = true;

      return RedirectToAction("SelectChild");
    }

    [HttpPost]
    public IActionResult BookSession(int ProgramId, List<int> SessionTimes, int studentId)
    {
      // Get the ongoing program ID for the student
      var programId = _unitOfWork.StudentProgram.Find(u => u.StudentID == studentId && u.Status == StudentStatus.Ongoing)
                                                .Select(p => p.ProgramID)
                                                .FirstOrDefault();

      // Get the step ID from the program
      var stepId = _unitOfWork.Program.Find(u => u.ProgramID == programId)
                                      .Select(a => a.StepId)
                                      .FirstOrDefault();

      // Get the roadmap ID from the step
      var roadmapId = _unitOfWork.Step.Find(u => u.StepId == stepId)
                                      .Select(p => p.RoadmapId)
                                      .FirstOrDefault();

      // Fetch student
      var student = _unitOfWork.Student.Find(s => s.StudentID == studentId).FirstOrDefault();

      // Fetch schedules
      var schedules = _unitOfWork.Schedule.GetAll()
                                          .Select(s => new
                                          {
                                            Date = s.Date.ToString("yyyy-MM-dd"),
                                            StartTime = s.StartTime.ToString(@"hh\:mm"),
                                            ScheduleID = s.ScheduleID
                                          }).ToList();

      if (ModelState.IsValid)
      {
        // Verify the existence of related entities
        var studentProgramId = _unitOfWork.StudentProgram.Find(sp => sp.StudentID == studentId)
                                                         .Select(u => u.StudentProgramId)
                                                         .FirstOrDefault();

        if (studentProgramId == 0)
        {
          // Handle the case where related entities are not found
          ModelState.AddModelError("", "Invalid student program or schedule.");
          return View("Index", new EnrollProgramVM
          {
            StudentProgram = _unitOfWork.StudentProgram.Get(i => i.Student.UserId == student.UserId),
            Schedule = _unitOfWork.Schedule.GetAll(),
            ProgramList = _unitOfWork.Program.Find(p => p.StepId == stepId && p.ProgramID == programId),
            ScheduleDataJson = JsonConvert.SerializeObject(schedules),
            StepList = _unitOfWork.Step.Find(u => u.RoadmapId == roadmapId),
            stepId = stepId
          });
        }

        foreach (var scheduleId in SessionTimes)
        {
          var session = new Session
          {
            StudentProgramId = studentProgramId,
            ScheduleID = scheduleId,
          };

          // Handling Schedule

          var scheduleDb = _unitOfWork.Schedule.Get(u => u.ScheduleID == scheduleId);

          // Adding 1 into Registered attribute in Schedule
          var registeredDb = _unitOfWork.Schedule.Find(u => u.ScheduleID == scheduleId).Select(u => u.Registered)
            .FirstOrDefault();

          var registeredUpdated = registeredDb + 1;

          var schedule = new Schedule
          {
            ScheduleID = scheduleDb.ScheduleID,
            Date = scheduleDb.Date,
            StartTime = scheduleDb.StartTime,
            EndDTime = scheduleDb.EndDTime,
            Capacity = scheduleDb.Capacity,
            Registered = registeredUpdated,
            ProgramID = scheduleDb.ProgramID
          };

          _db.Entry(scheduleDb).State = EntityState.Detached;

          _unitOfWork.Schedule.Update(schedule);
          _unitOfWork.Session.Add(session);
        }

        _unitOfWork.Save();

        return RedirectToAction("Index", "Session", new{id=studentId});
      }

      return View("Index");
    }

  }
}
