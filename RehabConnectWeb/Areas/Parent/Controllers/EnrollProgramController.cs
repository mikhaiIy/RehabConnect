using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

      EnrollProgramVM enrollProgramVM = new EnrollProgramVM()
      {
        StudentProgram = _unitOfWork.StudentProgram.Get(i => i.Student.UserId == student.UserId),
        Schedule = _unitOfWork.Schedule.GetAll(),
        ProgramList = _unitOfWork.Program.Find(p => stepsId.Contains(p.StepId)),
        Steps = steps

      };

      return View(enrollProgramVM);
    }
  }
}
