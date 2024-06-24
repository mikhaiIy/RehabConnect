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

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
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

    public IActionResult Index(int? id)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var student = _unitOfWork.Student
          .Find(s => s.StudentID == id)
          .FirstOrDefault();
      var programs = _db.Programs.ToList();
      var programid = _unitOfWork.StudentProgram.Get(s => s.Status == StudentStatus.Ongoing);
      var stepsId = _unitOfWork.Program.Find(z => z.ProgramID == programid.ProgramID).Select(s => s.StepId).ToList();
      var steps = _unitOfWork.Step.Get(u => u.StepId == programid.Program.StepId);

      var schedules = _unitOfWork.Schedule.GetAll()
          .Select(s => new
          {
            Date = s.Date.ToString("yyyy-MM-dd"),
            StartTime = s.StartTime.ToString(@"hh\:mm"),
            ScheduleID = s.ScheduleID
          })
          .ToList();

      EnrollProgramVM enrollProgramVM = new EnrollProgramVM()
      {
        StudentProgram = _unitOfWork.StudentProgram.Get(i => i.Student.UserId == student.UserId),
        Schedule = _unitOfWork.Schedule.GetAll(),
        ProgramList = _unitOfWork.Program.Find(p => stepsId.Contains(p.StepId)),
        Steps = steps,
        ScheduleDataJson = JsonConvert.SerializeObject(schedules)
      };

      return View(enrollProgramVM);
    }

    [HttpPost]
    public IActionResult BookSession(int studentProgramId, int scheduleId)
    {
      Console.WriteLine("Received StudentProgramId: " + studentProgramId);
      Console.WriteLine("Received ScheduleId: " + scheduleId);

      if (ModelState.IsValid)
      {
        // Verify the existence of related entities if needed
        var studentProgram = _unitOfWork.StudentProgram.Find(sp => sp.StudentProgramId == studentProgramId).FirstOrDefault();
        var schedule = _unitOfWork.Schedule.Find(s => s.ScheduleID == scheduleId).FirstOrDefault();

        if (studentProgram == null || schedule == null)
        {
          // Handle the case where related entities are not found
          ModelState.AddModelError("", "Invalid student program or schedule.");
          return View("Index");
        }

        var session = new Session
        {
          StudentProgramId = studentProgramId,
          ScheduleID = scheduleId,
          StudentProgram = _unitOfWork.StudentProgram.Get(s => s.StudentProgramId == studentProgramId), // Optionally set navigation properties
          Schedule = _unitOfWork.Schedule.Get(z => z.ScheduleID == scheduleId) // Optionally set navigation properties
        };

        _unitOfWork.Session.Add(session);
        _unitOfWork.Save();

        return RedirectToAction("Index"); // Redirect to an appropriate action or view
      }

      return View("Index");
    }

  }
}
