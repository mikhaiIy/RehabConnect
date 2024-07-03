using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.CustomerSupport.Controllers
{
  [Area("CustomerSupport")]
  [Authorize(Roles = SD.Role_CustomerSupport)]
  public class StudentProgramController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public StudentProgramController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      var studentProgram = _unitOfWork.StudentProgram.GetAll();

      foreach (var item in studentProgram)
      {
        var stuId = item.StudentID;
        var proId = item.ProgramID;

        item.Student = _unitOfWork.Student.Get(u => u.StudentID == stuId);
        item.Program = _unitOfWork.Program.Get(i => i.ProgramID == proId);
      }


      return View(studentProgram);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
      if (id == 0)
      {
        return NotFound();
      }

      var stuProgram = _unitOfWork.StudentProgram.Find(i => i.StudentProgramId == id, includeProperties: "Student").FirstOrDefault();

      if (stuProgram == null)
      {
        return NotFound();
      }

      var programs = _unitOfWork.Program.GetAll();
      var programSelectList = programs.Select(p => new SelectListItem
      {
        Value = p.ProgramID.ToString(),
        Text = p.ProgramName
      });

      var vm = new EditProgramVM()
      {
        studentProgram = stuProgram,
        ProgramSelectList = programSelectList,
        StatusList = GetStatusList()
      };

      return View(vm);
    }

    public IEnumerable<SelectListItem> GetStatusList()
    {
      return Enum.GetValues(typeof(StudentStatus))
        .Cast<StudentStatus>()
        .Select(status => new SelectListItem
        {
          Value = status.ToString(),
          Text = status.ToString()
        });
    }

    [HttpPost]

    public IActionResult Edit(int stuId, int progid, int studProg, StudentStatus status  )
    {
      if (ModelState.IsValid)
      {

        var studentProgram = _unitOfWork.StudentProgram.Get(i => i.StudentProgramId==studProg);

        if (progid == studentProgram.ProgramID && status!=StudentStatus.Completed)
        {
          studentProgram.Status = status;

          _unitOfWork.Save();
        }
        else
        {
          studentProgram.Status = StudentStatus.Completed;

          _unitOfWork.Save();

          var newStudentProgram = new StudentProgram
          {
            StudentID = stuId,
            ProgramID = progid,
            Status = StudentStatus.Ongoing
          };

          _unitOfWork.StudentProgram.Add(newStudentProgram);
          _unitOfWork.Save();
          return RedirectToAction(nameof(Index));
        }

      }
      return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
      StudentProgram studentProgram = _unitOfWork.StudentProgram.Get(u => u.StudentProgramId == id, includeProperties:"Student,Program");

      return View(studentProgram);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int studentProgramId)
    {
      var studentProgram = _unitOfWork.StudentProgram.Get(u => u.StudentProgramId == studentProgramId);
      if (studentProgram == null)
      {
        return NotFound();
      }
      _unitOfWork.StudentProgram.Remove(studentProgram);
      _unitOfWork.Save();
      return RedirectToAction("Index");
    }
  }
}
