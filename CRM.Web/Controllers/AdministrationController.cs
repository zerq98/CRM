using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Application.Dto.User;
using CRM.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdministrationController(IUserService userService,
                                        IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            await _userService.RemoveUser(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userService.AddUserAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ShowUserData(string id)
        {
            var user = await _userService.GetApplicationUser(id);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _roleService.CreateRoleAsync(model);

            return RedirectToAction("ShowAllRoles");
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllRolesAsync()
        {
            var roles = await _roleService.GetRolesAsync();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string Id)
        {
            var user = await _userService.GetApplicationUser(Id);
            var roles = await _roleService.GetRolesAsync();
            foreach(var role in roles)
            {
                if (user.Roles.Contains(role.RoleName))
                {
                    role.IsSelected = true;
                }
            }

            ViewBag.UserId = Id;
            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesVM> userRoles, string Id)
        {
            await _userService.AssignUserToRoleAsync(Id, userRoles);
            return RedirectToAction("ShowUserData", new { Id=Id});
        }
    }
}
