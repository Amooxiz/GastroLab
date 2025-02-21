using GastroLab.Application.Extensions;
using GastroLab.Application.Interfaces;
using GastroLab.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace GastroLab.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            var model = new CreateUserVM
            {
                AllRoles = (await _userService.GetAllRolesAsync())
                    .Select(role => new SelectListItem { Value = role, Text = role })
                    .ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.AllRoles = (await _userService.GetAllRolesAsync())
                .Select(role => new SelectListItem { Value = role, Text = role })
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var userVM = await _userService.GetUserByIdAsync(id);
            if (userVM == null)
                return NotFound();
            var editUserVM = userVM.ToEditUserVM();
            editUserVM.AllRoles = (await _userService.GetAllRolesAsync())
                .Select(role => new SelectListItem
                {
                    Value = role,
                    Text = role,
                    Selected = userVM.Roles.Contains(role)
                })
                .ToList();

            return View(editUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUserAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            model.AllRoles = (await _userService.GetAllRolesAsync())
                .Select(role => new SelectListItem
                {
                    Value = role,
                    Text = role,
                    Selected = model.Roles.Contains(role)
                })
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var userVM = await _userService.GetUserByIdAsync(id);
            if (userVM == null)
                return NotFound();

            return View(userVM);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "An error occurred while deleting the user.");

            var userVM = await _userService.GetUserByIdAsync(id);
            return View("Delete", userVM);
        }
    }
}
