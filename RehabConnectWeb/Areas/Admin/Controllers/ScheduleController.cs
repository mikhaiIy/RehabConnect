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
    public IActionResult GetAll(int draw, int start, int length,
            [FromQuery(Name = "order[0][column]")] int orderColumnIndex,
            [FromQuery(Name = "order[0][dir]")] string orderDir,
            [FromQuery(Name = "columns[0][search][value]")] string dateFilter,
            [FromQuery(Name = "columns[1][search][value]")] string startTimeFilter,
            [FromQuery(Name = "columns[2][search][value]")] string endTimeFilter,
            [FromQuery(Name = "search[value]")] string searchValue)
    {
      try
      {
        var allSchedules = _unitOfWork.Schedule.GetAll();

        // Filtering by Date
        if (!string.IsNullOrEmpty(dateFilter))
        {
          DateOnly filterDateOnly;
          if (DateOnly.TryParse(dateFilter, out filterDateOnly))
          {
            allSchedules = allSchedules.Where(s => s.Date == filterDateOnly);
          }
          else
          {
            // Handle invalid date filter scenario if needed
          }
        }

        //Search
        if (!string.IsNullOrEmpty(searchValue))
        {
          allSchedules = allSchedules.Where(s =>
              // Add conditions based on your table columns where you want to apply search
              s.Date.ToString().Contains(searchValue) ||
              s.StartTime.ToString().Contains(searchValue) ||
              s.EndTime.ToString().Contains(searchValue)
                // Add more columns as needed
                // Example: s.Duration.ToString().Contains(searchValue)
          );
        }

        // Sorting
        switch (orderColumnIndex)
        {
          case 0:
            if (orderDir == "asc")
              allSchedules = allSchedules.OrderBy(s => s.Date);
            else
              allSchedules = allSchedules.OrderByDescending(s => s.Date);
            break;
          // Add more cases for other columns if needed
          default:
            allSchedules = allSchedules.OrderBy(s => s.Date);
            break;
        }

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

    [HttpDelete]
    public IActionResult Delete(int? id)
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
