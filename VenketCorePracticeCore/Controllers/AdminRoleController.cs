using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenketCorePracticeCore.Models;
using VenketCorePracticeCore.Models.ViewModels;

namespace VenketCorePracticeCore.Controllers
{

    // ye 2sra projec hai Venket ka ye wala theek run ho rha hai 

        //[Authorize(Roles = "Admin")]
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this._roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("CreateRole");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }


        public IActionResult ShowAllRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }


        [HttpGet]

        public async Task
            <IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return RedirectToAction("ShowAllRoles");
            }

            var model = new EditRoleViewModel
            {
                RoleName = role.Name,
                Id = role.Id

            };



            return View(model);
        }


        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if(role==null)
            {
                return View();

            }
            else
            {
                role.Name=model.RoleName;
              IdentityResult result =  await  _roleManager.UpdateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("ShowAllRoles");
                }

                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }


                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRole(string RoleId)
        {
            ViewBag.RoleID = RoleId;

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Id {RoleId} is not here";

                return View("NotFound");
            }
            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserID = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, RoleId))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRole(List<UserRoleViewModel>model,string RoleId)
        {
            ViewBag.RoleID = RoleId;

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Id {RoleId} is not here";

                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserID);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if(!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (i < (model.Count - 1))
                {
                    continue;
                }
                else
                {
                    return RedirectToAction("EditRole", new { id = RoleId });
                }
            }
            return RedirectToAction("EditRole", new { id = RoleId });
        }
    }
}
