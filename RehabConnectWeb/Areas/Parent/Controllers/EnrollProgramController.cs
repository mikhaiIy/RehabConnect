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



      EnrollProgramVM enrollProgramVm = new EnrollProgramVM()
      {
        StudentProgram = _unitOfWork.StudentProgram.Get(i => i.Student.UserId == student.UserId),
        Schedule = _unitOfWork.Schedule.GetAll(),
        ProgramList = _unitOfWork.Program.Find(p => p.StepId == stepId && p.ProgramID == programId),


        // -- Header
        StepList = _unitOfWork.Step.Find(u=>u.RoadmapId==roadmapId),
        stepId = stepId
      };


      return View(enrollProgramVm);
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

          _unitOfWork.Session.Add(session);
        }

        _unitOfWork.Save();

        return RedirectToAction("Index", new { id = studentId });
      }

      return View("Index");
    }

  }
}
