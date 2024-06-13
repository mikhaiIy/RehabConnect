

using Microsoft.AspNetCore.Mvc;
using RehabConnect.Utility;
using RehabConnect.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RehabConnect.ViewModels;
using RehabConnect.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class SessionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SessionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch the schedule data
            var sessions = await _db.Sessions
                .Include(s => s.Student)
                .Include(s => s.Therapist)
                .Include(s => s.Program)
                .Include(s => s.Schedule)
                .ToListAsync();

            // Organize sessions by day and period
            var scheduleData = sessions
                .GroupBy(s => s.Schedule.Date.DayOfWeek)
                .Select(g => new ScheduleViewModel
                {
                    Day = g.Key.ToString(),
                    Periods = g.Select(s => new PeriodViewModel
                    {
                        PeriodTime = s.Schedule.Time,
                        Subject = s.Program.ProgramName
                    }).ToList()
                })
                .ToList();

            return View(scheduleData);
        }

        public IActionResult ManageSession()
        {
            var sessions = _db.Sessions
                .Include(s => s.Student)
                .Include(s => s.Therapist)
                .Include(s => s.Program)
                .Include(s => s.Schedule)
                .Select(s => new SessionViewModel
                {
                    SessionID = s.SessionID,
                    StudentName = s.Student.ChildName,
                    TherapistID = s.Therapist.TherapistID,
                    ProgramName = s.Program.ProgramName,
                    ScheduleDate = s.Schedule.Date,
                    Status = s.Status
                }).ToList();

            return View(sessions);
        }
    }
}

