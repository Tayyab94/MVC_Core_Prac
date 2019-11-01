using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VenketCorePracticeCore.Models;
using VenketCorePracticeCore.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VenketCorePracticeCore.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccessController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("ShowAllEmployee", "Home");
        }

        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult>IsEmailExist(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if(user==null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstNameofUser=model.FirstNameofUser };
              var result=await  userManager.CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("ShowAllEmployee", "Home");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> login(string returnUrl)
        {
            loginViewModel model = new loginViewModel
            {
                ReturnURL = returnUrl,
                ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> login(loginViewModel model, string returnUrl)
        {

                
            //if (ModelState.IsValid)
            //{
            //    var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Rememberme, false);
            //    if (result.Succeeded)
            //    {
            //        if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            //        {
            //            return Redirect(returnUrl);
            //        }
            //        return RedirectToAction("ShowAllEmployee", "Home");
            //    }

            //        ModelState.AddModelError(string.Empty,"Invalid login Attempt");  
            //}


            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Rememberme, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("ShowAllEmployee", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login Attempt");
            }
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogins(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Access", new { returnURL = returnUrl });

            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }


        //[AllowAnonymous]
        //public async Task
        //    <IActionResult> ExternalLoginCallback(string returnURL=null, string remoteError=null)
        //{

        //    returnURL = returnURL ?? Url.Content("~/");

        //    loginViewModel loginViewModel = new loginViewModel
        //    {
        //        ReturnURL = returnURL,
        //        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };

        //    if(remoteError!=null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from the External provider :{remoteError}");

        //        return View("Login", loginViewModel);
        //    }

        //    var info = await signInManager.GetExternalLoginInfoAsync();
        //    if(info==null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error loading External login Informtion provider :{remoteError}");

        //        return View("Login", loginViewModel);
        //    }

        //    var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        //    if(signInResult.Succeeded)
        //    {
        //        return LocalRedirect(returnURL);
        //    }else
        //    {
        //      //  var email=info.Principal.FindFirstValue()
        //    }
        //}

    }
}
