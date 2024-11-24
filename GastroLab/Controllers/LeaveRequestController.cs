using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GastroLab.Presentation.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly UserManager<User> _userManager;

        public LeaveRequestController(ILeaveRequestService leaveRequestService, UserManager<User> userManager)
        {
            _leaveRequestService = leaveRequestService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Delete(LeaveRequestVM leaveRequestVM)
        {
            _leaveRequestService.DeleteLeaveRequest(leaveRequestVM.Id);
            return RedirectToAction("MyLeaveRequests");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(LeaveRequestVM leaveRequestVM)
        {
            _leaveRequestService.UpdateLeaveRequest(leaveRequestVM);
            return RedirectToAction("MyLeaveRequests");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Consider(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Consider(LeaveRequestVM leaveRequestVM)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                throw new Exception("Unknown user");
            }
            leaveRequestVM.UserId = userId;
            leaveRequestVM.ResolvedOn = DateTime.Now;
            _leaveRequestService.UpdateLeaveRequest(leaveRequestVM);
            return RedirectToAction("ManageLeaveRequests");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(LeaveRequestVM leaveRequestVM)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                throw new Exception("Unknown user");
            }
            leaveRequestVM.UserId = userId;
            leaveRequestVM.CreatedOn = DateTime.Now;
            _leaveRequestService.AddLeaveRequest(leaveRequestVM);
            return RedirectToAction("MyLeaveRequests");
        }

        [HttpGet]
        [Authorize]
        public IActionResult ManageLeaveRequests(string status = "Pending")
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                throw new Exception("Unknown user");
            }

            List<LeaveRequestVM> leaveRequests;
            if (status == "Resolved")
            {
                leaveRequests = _leaveRequestService.GetResolvedLeaveRequests().OrderByDescending(x => x.ResolvedOn).ToList();
            }
            else
            {
                leaveRequests = _leaveRequestService.GetPendingLeaveRequests().OrderByDescending(x => x.CreatedOn).ToList();
            }

            ViewBag.Status = status;
            return View(leaveRequests);
        }


        [HttpGet]
        [Authorize]
        public IActionResult MyLeaveRequests(string status = "Pending")
{
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                throw new Exception("Unknown user");
            }

            List<LeaveRequestVM> leaveRequests;
            if (status == "Resolved")
            {
                leaveRequests = _leaveRequestService.GetResolvedLeaveRequestsByUserId(userId);
            }
            else
            {
                leaveRequests = _leaveRequestService.GetPendingLeaveRequestsByUserId(userId);
            }

            ViewBag.Status = status;
            return View(leaveRequests);
        }
    }
}
