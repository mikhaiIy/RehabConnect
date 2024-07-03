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
        ProgramSelectList = programSelectList
      };

      return View(vm);
    }

    [HttpPost]
    public IActionResult Edit(int stuId, int progid)
    {
      if (ModelState.IsValid)
      {
        StudentProgram obj = new StudentProgram()
        {
          StudentID = stuId,
          ProgramID = progid,
          Status = 0
        };

        _unitOfWork.StudentProgram.Add(obj);
        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
      }

      return View("Index");
    }

  }
}
