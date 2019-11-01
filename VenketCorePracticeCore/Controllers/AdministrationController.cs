using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenketCorePracticeCore.Models;
using VenketCorePracticeCore.Models.DataContext;
using VenketCorePracticeCore.Models.ViewModels;

namespace VenketCorePracticeCore.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DemoContext _context;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, DemoContext _context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._context = _context;
        }

        [HttpGet]

        public IActionResult ShowAllUsers()
        {


            List<RegisterUsers> users = _context.Users.Select(s => new RegisterUsers
            {
                ID = s.Id,
                UserNam = s.UserName,
                Email = s.Email
            }).ToList();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> editUser(string ID)
        {
            var user = await userManager.FindByIdAsync(ID);
            if(user==null)
            {
                ViewBag.ErrorMessage = $"USer With Id {ID} can not be found";

                return View("Not Found");
            }

            var userClaims = await userManager.GetClaimsAsync(user);
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                userName = user.UserName,
                Email = user.Email,
                Claims = userClaims.Select(s => s.Value).ToList(),
                Roles = userRoles,
                City=user.FirstNameofUser
            };

            return View(model);


        }

        [HttpPost]
        public async Task<IActionResult> editUser(EditUserViewModel userModel)
        {
            var user = await userManager.FindByIdAsync(userModel.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"USer With Id {userModel.Id} can not be found";

                return View("Not Found");
            }
            else
            {
                user.Email = userModel.Email;
                user.UserName = userModel.userName;
                user.FirstNameofUser = userModel.City;        

                var result = await userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("ShowAllUsers", "Administration");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return View(userModel);
            }

        }



        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userID)
        {
            ViewBag.UserID = userID;
            var user = await userManager.FindByIdAsync(userID);

            if(user==null)
            {
                ViewBag.ErrorMessage = $"User Id {user.Id} is not here";

                return View("NotFound");
            }

            var model = new List<RolesUserViewModel>();


            foreach (var role in roleManager.Roles)
            {
                var userRolesviewmodel = new RolesUserViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if(await userManager.IsInRoleAsync(user,role.Name))
                {
                    userRolesviewmodel.IsSelected = true;
                }
                else
                {
                    userRolesviewmodel.IsSelected = false;
                }

                model.Add(userRolesviewmodel);
            }

            return View(model);
        }


        //this Function is Use to Manage the User Role. (Assign the Role to the User).
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<RolesUserViewModel> model, string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User Id {user.Id} is not here";

                return View("NotFound");
            }

            var roles = await userManager.GetRolesAsync(user);

            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if(result.Succeeded==false)
            {
                ViewBag.ErrorMessage = $"Can Not existing User Roles";
                return View("NotFound");
            }


            result = await userManager.AddToRolesAsync(user, model.Where(s => s.IsSelected).Select(y => y.RoleName));


            if (result.Succeeded == false)
            {
                ViewBag.ErrorMessage = $"Can Not Add Selected Roles to user";
                return View("NotFound");
            }


            return RedirectToAction("EditUser", new { ID = userId });

        }
        public async Task<IActionResult> DeleteUsers(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User Id {user.Id} is not here";

                return View("NotFound");
            }

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
                return RedirectToAction("ShowAllRoles","AdminRole");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return View();
        }


        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role Id {role.Id} is not here";

                return View("NotFound");
            }

            var result = await roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return RedirectToAction("ShowAllUsers");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }


            return View();
        }



    }
}
