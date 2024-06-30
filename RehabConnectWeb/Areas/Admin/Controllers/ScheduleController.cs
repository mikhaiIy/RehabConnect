using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string startDt, TimeOnly startTime,TimeOnly endTime , int programId, int capacity)
    {
      if (!string.IsNullOrEmpty(startDt))
      {
        var dates = startDt
          .Split(',').Select(DateTime.Parse)
          .ToList();

        var schedules = new List<Schedule>();

        foreach (var date in dates)
        {
          var schedule = new Schedule
          {
            Date = DateOnly.FromDateTime(date),
            StartTime = startTime,
            EndDTime = endTime,
            Capacity = capacity,
            ProgramID = programId,
            Registered = 0
          };
          schedules.Add(schedule);
        }

        _unitOfWork.Schedule.AddRange(schedules);
        _unitOfWork.Save();
        return RedirectToAction("Create");
      }

      return View();
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

    // -- Get Schedule for Admin Calendar --
    [HttpGet]
    public IActionResult GetSchedule()
    {
      var schedulesList = _unitOfWork.Schedule.GetAll(includeProperties:"Program").ToList();

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




    // -- Get Roadmap, Step & Program/Session List --
    [HttpGet]
    public IActionResult GetProgram()
    {

      ScheduleViewModel scheduleViewModel = new ScheduleViewModel()
      {
        RoadmapList = _unitOfWork.Roadmap.GetAll().ToList(),
        StepList = _unitOfWork.Step.GetAll().ToList(),
        ProgramList = _unitOfWork.Program.GetAll().ToList()
      };

      return Json(new { data = scheduleViewModel });
    }

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
