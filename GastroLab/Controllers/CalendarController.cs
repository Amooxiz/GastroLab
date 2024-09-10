using GastroLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JasperFx.CodeGeneration.Frames;

namespace GastroLab.Presentation.Controllers
{
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;
        private UserManager<User> _userManager;

        public CalendarController(ICalendarService calendarService, UserManager<User> userManager)
        {
            _calendarService = calendarService;
            _userManager = userManager;
        }
        public IActionResult GetCalendar(DateTime? date)
        {
            var model = new WeeklyCalendarVM
            {
                BeginningOfTheWeek = FindBeginningOfTheWeek(date ?? DateTime.Now),
                DaysOfWeek = new List<DateTime>()
            };

            for (int i = 0; i < 7; i++)
            {
                model.DaysOfWeek.Add(model.BeginningOfTheWeek.AddDays(i));
            }
            var user = _userManager.GetUserAsync(HttpContext.User).Result;

            if (user != null)
            {
                ViewBag.UserWorkingTimes = _calendarService.GetWorkingTimesByUserId(user.Id);
            }


            return View(model);
        }

        public IActionResult ManageWorkingTimes(DateTime? date, string userId)
        {
            var model = new WeeklyCalendarVM
            {
                BeginningOfTheWeek = FindBeginningOfTheWeek(date ?? DateTime.Now),
                DaysOfWeek = new List<DateTime>()
            };
            // Retrieve the list of users
            var users = _userManager.Users.ToList();

            // Set the default user if none is selected
            if (string.IsNullOrEmpty(userId) && users.Any())
            {
                userId = users.First().Id;
            }
            if (userId != null)
            {
                ViewBag.UserWorkingTimes = _calendarService.GetWorkingTimesByUserId(userId);
            }
            ViewBag.Users = new SelectList(users, "Id", "UserName", userId);
            for (int i = 0; i < 7; i++)
            {
                model.DaysOfWeek.Add(model.BeginningOfTheWeek.AddDays(i));
            }

            return View(model);
        }

        private DateTime FindBeginningOfTheWeek(DateTime date)
        {
            int dayOfWeek = (int)date.DayOfWeek;
            int substractDays = dayOfWeek == 0 ? 6 : dayOfWeek - 1;
            return date.AddDays(-substractDays);
        }
        public IActionResult Index()
        {
            return View();
        }

        public class WeeklyCalendarVM
        {
            public DateTime BeginningOfTheWeek { get; set; }
            public List<DateTime> DaysOfWeek{ get; set; }
        }
    }
}
