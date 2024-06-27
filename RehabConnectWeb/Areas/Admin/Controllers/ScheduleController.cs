using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RehabConnect.DataAccess.Repository;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
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

    public IActionResult Create()
    {
      ScheduleViewModel vm = new ScheduleViewModel()
      {
        ProgramList = _unitOfWork.Program.GetAll().ToList()
      };

      return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string dt, int idprog)
    {

      var program = _unitOfWork.Program.Get(u => u.ProgramID == idprog);

      if (!string.IsNullOrEmpty(dt))
      {
        var dateTimes = dt.Split(',').Select(d => d.Trim()).ToList();

        foreach (var dtString in dateTimes)
        {
          if (DateTime.TryParse(dtString, out DateTime parsedDateTime))
          {
            var schedule = new Schedule
            {
              Date = DateOnly.FromDateTime(parsedDateTime),
              StartTime = parsedDateTime.TimeOfDay,
              EndTime = parsedDateTime.TimeOfDay + TimeSpan.FromHours(1),
              Duration = TimeSpan.FromHours(1),
              ProgramID = idprog,
              Program = program
            };

            _unitOfWork.Schedule.Add(schedule);
          }
          else
          {
            return View();
          }
        }

        _unitOfWork.Save();
        return RedirectToAction("Create");
      }
      else
      {
        return View();
      }
    }

    public IActionResult Edit(int? id)
    {
      if (id == null || id == 0)
      {
        return View(new Schedule());
      }
      else
      {
        Schedule scheduleObj = _unitOfWork.Schedule.Get(u => u.ScheduleID == id);
        return View(scheduleObj);
      }
    }

    [HttpPost]
    public IActionResult Edit(Schedule scheduleObj)
    {
      if (ModelState.IsValid)
      {
          _unitOfWork.Schedule.Update(scheduleObj);


        _unitOfWork.Save();
        TempData["success"] = "Schedule Updated";
        return RedirectToAction("Index");
      }
      else
      {
        return View(scheduleObj);
      }
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
      var allSchedules = _unitOfWork.Schedule.GetAll();
      return Json(new { data = allSchedules });
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
      var ScheduleToBeDeleted = _unitOfWork.Schedule.Get(u => u.ScheduleID == id);
      if (ScheduleToBeDeleted == null)
      {
        return Json(new { success = false, message = "Error while deleting" });
      }
      else
      {
        _unitOfWork.Schedule.Remove(ScheduleToBeDeleted);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
      }
    }

    #endregion
  }

}
