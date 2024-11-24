using GastroLab.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JasperFx.CodeGeneration.Frames;
using Microsoft.AspNetCore.Authorization;
using GastroLab.Presentation.RequestModels;
using GastroLab.Application.ViewModels;
using System.Globalization;
using System.Security.Claims;

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

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateRegisteredTime([FromBody] UpdateRegisteredTimeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Nieprawidłowe dane.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Walidacja, czy StartDate jest przed FinishDate
            if (request.StartDate >= request.FinishDate)
            {
                return BadRequest("Czas rozpoczęcia musi być przed czasem zakończenia.");
            }

            var timeSlot = new TimeSlotVM()
            {
                Id = request.TimeslotId,
                DateFrom = request.StartDate,
                DateTo = request.FinishDate,
                Description = request.Description,
                UserId = userId
            };

            // Sprawdzenie, czy nie ma nakładających się czasów
            var overlappingTimes = _calendarService.CheckForOverlappingTimes(userId, timeSlot);

            if (overlappingTimes)
            {
                return BadRequest("Zaktualizowany czas nakłada się na inny zarejestrowany czas.");
            }

            _calendarService.UpdateRegisteredTime(timeSlot);

            return Json(new { success = true });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRegisteredTime([FromBody] DeleteRegisteredTimeRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var registeredTime = _calendarService.GetRegisteredTimeById(request.TimeslotId);

            if (registeredTime == null)
            {
                return NotFound("Zarejestrowany czas nie został znaleziony.");
            }

            if (registeredTime.UserId != userId)
            {
                return Forbid("Nie masz uprawnień do usunięcia tego zarejestrowanego czasu.");
            }

            _calendarService.DeleteRegisteredTime(registeredTime.Id);

            return Json(new { success = true });
        }




        [Authorize(Roles = "Admin,Director,Manager")]
        public IActionResult ManageRegisteredTimesListView(int? year, int? month)
        {
            var model = new ManageRegisteredTimesListVM()
            {
                SelectedYear = year ?? DateTime.Now.Year,
                SelectedMonth = month ?? DateTime.Now.Month
            };

            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Director,Manager")]
        public IActionResult GetRegisteredTimesSummary(int year, int month)
        {
            // Retrieve all users
            var users = _userManager.Users.ToList();

            // Prepare the data
            var data = new List<object>();

            foreach (var user in users)
            {
                // Sum of registered hours for the user in the specified month and year
                var totalHours = _calendarService.GetRegisteredTimesByUserId(user.Id)
                    .Where(rt => rt.DateFrom.Year == year && rt.DateFrom.Month == month)
                    .Sum(rt => (rt.DateTo - rt.DateFrom).TotalHours);

                data.Add(new
                {
                    name = user.Name,
                    surname = user.Surname,
                    email = user.Email,
                    sumOfRegisteredHours = totalHours
                });
            }

            return Json(data);
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Director,Manager")]
        public IActionResult ManageRegisteredTimes(DateTime? date, string userId)
        {
            var model = new WeeklyCalendarVM
            {
                BeginningOfTheWeek = FindBeginningOfTheWeek(date ?? DateTime.Now),
                DaysOfWeek = new List<DateTime>()
            };

            // Retrieve the list of users
            var users = _userManager.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "UserName", userId);

            // Set the default user if none is selected
            if (string.IsNullOrEmpty(userId) && users.Any())
            {
                userId = users.First().Id;
            }

            if (userId != null)
            {
                ViewBag.UserRegisteredTimes = _calendarService.GetRegisteredTimesByUserId(userId);
            }

            for (int i = 0; i < 7; i++)
            {
                model.DaysOfWeek.Add(model.BeginningOfTheWeek.AddDays(i));
            }

            return View(model);
        }


        [HttpGet]
        [Authorize]
        public IActionResult RegisterTime(DateTime? date)
        {
            var model = new WeeklyCalendarVM
            {
                BeginningOfTheWeek = FindBeginningOfTheWeek(date ?? DateTime.Now),
                DaysOfWeek = new List<DateTime>()
            };

            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            ViewBag.UserRegisteredTimes = _calendarService.GetRegisteredTimesByUserId(currentUserId);
            ViewBag.CurrentUserId = currentUserId;

            for (int i = 0; i < 7; i++)
            {
                model.DaysOfWeek.Add(model.BeginningOfTheWeek.AddDays(i));
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RegisterTime([FromBody] RegisterWorkingTimeRequest request)
        {
            // Combine startDate and startTime
            DateTime startDateTime;
            if (!DateTime.TryParseExact(
                $"{request.StartDate.Value:yyyy-MM-dd} {request.StartTime}",
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out startDateTime))
            {
                return BadRequest("Invalid start time format.");
            }

            // Combine finishDate and finishTime
            DateTime finishDateTime;
            if (!DateTime.TryParseExact(
                $"{request.FinishDate.Value:yyyy-MM-dd} {request.FinishTime}",
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out finishDateTime))
            {
                return BadRequest("Invalid finish time format.");
            }

            // Validate that startDateTime is before finishDateTime
            if (startDateTime >= finishDateTime)
            {
                return BadRequest("Start time must be before finish time.");
            }

            var timeSlot = new TimeSlotVM()
            {
                DateFrom = startDateTime,
                DateTo = finishDateTime,
                UserId = request.UserId,
                Description = request.Description
            };

            _calendarService.AddRegisteredTime(timeSlot);
            return Json(new { success = true, message = "Time registered successfully." });
        }


        [Authorize]
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

        [Authorize(Roles = "Admin,Director,Manager")]
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

        [HttpPost]
        [Authorize(Roles = "Admin,Director,Manager")]
        public IActionResult RegisterWorkingTime([FromBody] RegisterWorkingTimeRequest request)
        {
            // Combine startDate and startTime
            DateTime startDateTime;
            if (!DateTime.TryParseExact(
                $"{request.StartDate.Value:yyyy-MM-dd} {request.StartTime}",
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out startDateTime))
            {
                return BadRequest("Invalid start time format.");
            }

            // Combine finishDate and finishTime
            DateTime finishDateTime;
            if (!DateTime.TryParseExact(
                $"{request.FinishDate.Value:yyyy-MM-dd} {request.FinishTime}",
                "yyyy-MM-dd HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out finishDateTime))
            {
                return BadRequest("Invalid finish time format.");
            }

            // Validate that startDateTime is before finishDateTime
            if (startDateTime >= finishDateTime)
            {
                return BadRequest("Start time must be before finish time.");
            }

            var timeSlot = new TimeSlotVM()
            {
                DateFrom = startDateTime,
                DateTo = finishDateTime,
                UserId = request.UserId
            };

            _calendarService.AddWorkingTime(timeSlot);
            return Json(new { success = true, message = "Working time registered successfully." });
        }

        private DateTime FindBeginningOfTheWeek(DateTime date)
        {
            int dayOfWeek = (int)date.DayOfWeek;
            int substractDays = dayOfWeek == 0 ? 6 : dayOfWeek - 1;
            return date.AddDays(-substractDays);
        }

        public class WeeklyCalendarVM
        {
            public DateTime BeginningOfTheWeek { get; set; }
            public List<DateTime> DaysOfWeek{ get; set; }
        }
    }
}
