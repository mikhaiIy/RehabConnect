using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

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
    public async Task<IActionResult> Create(string dt)
    {
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
              Duration = TimeSpan.FromHours(1)
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

    #region API CALLS

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var allSchedules =  _unitOfWork.Schedule.GetAll();
      return Json(new { data = allSchedules });
    }

    #endregion
  }
}
