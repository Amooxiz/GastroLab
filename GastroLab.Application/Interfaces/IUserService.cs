using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GastroLab.Application.Interfaces
{
    public interface IUserService
    {
        List<LeaveHoursByUserVM> GetLeaveHoursByUsers(DateTime dateFrom, DateTime dateTo);
        List<WorkingHoursByUserVM> GetWorkingHoursByUsers(DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<UserVM>> GetAllUsersAsync();
        Task<UserVM> GetUserByIdAsync(string userId);
        Task<IdentityResult> CreateUserAsync(CreateUserVM userVM);
        Task<IdentityResult> UpdateUserAsync(EditUserVM userVM);
        Task<IdentityResult> DeleteUserAsync(string userId);
        Task<IEnumerable<string>> GetAllRolesAsync();
        Task<IList<string>> GetUserRolesAsync(string userId);
        Task<IdentityResult> AddUserToRolesAsync(string userId, IEnumerable<string> roles);
        Task<IdentityResult> RemoveUserFromRolesAsync(string userId, IEnumerable<string> roles);
    }
}
