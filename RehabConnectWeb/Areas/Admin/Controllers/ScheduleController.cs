using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RehabConnect.DataAccess.Repository;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
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
    [HttpGet]
    public IActionResult GetAll(int draw, int start, int length)
    {
      try
      {
        var allSchedules = _unitOfWork.Schedule.GetAll();
        int recordsTotal = allSchedules.Count();
        var data = allSchedules.Skip(start).Take(length).ToList();

        var response = new
        {
          draw = draw,
          recordsTotal = recordsTotal,
          recordsFiltered = recordsTotal,
          data = data
        };

        return Json(response);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error in GetAll: {ex.Message}");
        return StatusCode(500, "Internal server error");
      }
    }


    #endregion
  }

}
