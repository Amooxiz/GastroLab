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

        // Constructor with dependency injection
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: User/Create
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

        // POST: User/Create
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

                // Add errors to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Reload roles if ModelState is invalid
            model.AllRoles = (await _userService.GetAllRolesAsync())
                .Select(role => new SelectListItem { Value = role, Text = role })
                .ToList();

            return View(model);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var userVM = await _userService.GetUserByIdAsync(id);
            if (userVM == null)
                return NotFound();
            var editUserVM = userVM.ToEditUserVM();
            // Populate AllRoles with selection
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

        // POST: User/Edit/5
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

                // Add errors to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Reload roles
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

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var userVM = await _userService.GetUserByIdAsync(id);
            if (userVM == null)
                return NotFound();

            return View(userVM);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            // Handle errors if needed
            ModelState.AddModelError(string.Empty, "An error occurred while deleting the user.");

            var userVM = await _userService.GetUserByIdAsync(id);
            return View("Delete", userVM);
        }
    }
}
