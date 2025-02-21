using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using GastroLab.Domain.DBO;
using GastroLab.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GastroLab.Application.Extensions;

namespace GastroLab.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GastroLabDbContext _context;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, GastroLabDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public List<LeaveHoursByUserVM> GetLeaveHoursByUsers(DateTime dateFrom, DateTime dateTo)
        {
            var leaveRequests = _context.LeaveRequests.Include(x => x.User).AsQueryable();

            leaveRequests = leaveRequests.Where(x => x.DateFrom < dateTo && x.DateTo > dateFrom);

            var result = leaveRequests
                .ToList()
                .GroupBy(x => x.User)
                .Select(x => new
                {
                    User = x.Key,
                    TotalLeaveHours = x.Sum(x => x.DateFrom >= dateFrom && x.DateTo <= dateTo // sumowanie jedynie tych godzin, które mieszczą się w zakresie dat
                        ? (x.DateTo - x.DateFrom).TotalHours
                        : (x.DateFrom.CompareTo(dateFrom) >= 0
                            ? (dateTo - x.DateFrom).TotalHours
                            : (x.DateTo - dateFrom).TotalHours))
                });

            return result.Select(x => new LeaveHoursByUserVM()
            {
                User = x.User.UserName ?? x.User.Email,
                TotalLeaveHours = x.TotalLeaveHours
            }).ToList(); ;

        }
        public List<WorkingHoursByUserVM> GetWorkingHoursByUsers(DateTime dateFrom, DateTime dateTo)
        {
            var workingHours = _context.WorkingTimes.Include(x => x.User).AsQueryable();
            var registeredHours = _context.RegisteredTimes.Include(x => x.User).AsQueryable();

            workingHours = workingHours.Where(x => x.DateFrom < dateTo && x.DateTo > dateFrom);
            registeredHours = registeredHours.Where(x => x.DateFrom < dateTo && x.DateTo > dateFrom);

            var result = workingHours
                .ToList()
                .GroupBy(x => x.User)
                .Select(x => new
                {
                    User = x.Key,
                    TotalWorkingHours = x.Sum(x => x.DateFrom >= dateFrom && x.DateTo <= dateTo // sumowanie jedynie tych godzin, które mieszczą się w zakresie dat
                        ? (x.DateTo - x.DateFrom).TotalHours
                        : (x.DateFrom.CompareTo(dateFrom) >= 0
                            ? (dateTo - x.DateFrom).TotalHours
                            : (x.DateTo - dateFrom).TotalHours)),

                    TotalRegisteredHours = registeredHours
                    .Where(y => y.User == x.Key)
                    .ToList()
                        .Sum(y => y.DateFrom >= dateFrom && y.DateTo <= dateTo // sumowanie jedynie tych godzin, które mieszczą się w zakresie dat
                            ? (y.DateTo - y.DateFrom).TotalHours
                            : (y.DateFrom.CompareTo(dateFrom) >= 0
                                ? (dateTo - y.DateFrom).TotalHours
                                : (y.DateTo - dateFrom).TotalHours))
                });

            return result.Select(x => new WorkingHoursByUserVM()
            {
                User = x.User.UserName ?? x.User.Email,
                TotalRegisteredHours = x.TotalRegisteredHours,
                TotalWorkingHours = x.TotalWorkingHours
            }).ToList();
        }

        public async Task<IEnumerable<UserVM>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userVMs = new List<UserVM>();

            foreach (var user in users)
            {
                var userVM = user.ToVM();
                userVM.Roles = (await _userManager.GetRolesAsync(user)).ToList();
                userVMs.Add(userVM);
            }

            return userVMs;
        }

        public async Task<UserVM> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var user1 = (User)user;
            var userVM = user1.ToVM();
            userVM.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return userVM;
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserVM userVM)
        {
            var user = userVM.ToModel();
            var result = await _userManager.CreateAsync(user, userVM.Password);

            if (result.Succeeded)
            {
                if (userVM.SelectedRoles != null && userVM.SelectedRoles.Any())
                {
                    await _userManager.AddToRolesAsync(user, userVM.SelectedRoles);
                }
            }

            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(EditUserVM userVM)
        {
            var user = await _userManager.FindByIdAsync(userVM.Id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            user.UserName = userVM.UserName;
            user.Email = userVM.Email;
            user.Name = userVM.Name;
            user.Surname = userVM.Surname;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var rolesToAdd = userVM.SelectedRoles.Except(userRoles).ToList();
                var rolesToRemove = userRoles.Except(userVM.SelectedRoles).ToList();

                if (rolesToAdd.Any())
                {
                    await _userManager.AddToRolesAsync(user, rolesToAdd);
                }

                if (rolesToRemove.Any())
                {
                    await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                }
            }

            return result;
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.Select(r => r.Name).ToListAsync();
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> AddUserToRolesAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, IEnumerable<string> roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.RemoveFromRolesAsync(user, roles);
        }
    }
}
