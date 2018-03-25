using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PressfordConsultingNews.Models;


namespace PressfordConsultingNews.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
             // this is just some quick coding to get the login working
             // enter the worked admin in email to login as a publisher or use the word user to login as both publishers(editor)/user(view)
             // e.g. admin@admim.com or user@user.com
             // password does not matter

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = SignInManager.UserManager.FindByEmail(model.Email);

            if (user == null)
            {
                if (model.Email.Contains("admin"))
                {
                    var createdUser = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var createdResult = await UserManager.CreateAsync(createdUser, "PressfordAdm1n!");

                    if (createdResult.Succeeded)
                    {
                        var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));

                        if (RoleManager.RoleExists("Admin"))
                        {
                            await UserManager.AddToRoleAsync(createdUser.Id, "Admin");
                        }
                        else
                        {
                            await RoleManager.CreateAsync(new IdentityRole("Admin"));
                            await UserManager.AddToRoleAsync(createdUser.Id, "Admin");
                        }

                        if (RoleManager.RoleExists("User"))
                        {
                            await UserManager.AddToRoleAsync(createdUser.Id, "User");
                        }
                        else
                        {
                            await RoleManager.CreateAsync(new IdentityRole("User"));
                            await UserManager.AddToRoleAsync(createdUser.Id, "User");
                        }

                        await SignInManager.SignInAsync(createdUser, isPersistent: false, rememberBrowser: false);
                    }
                }
                else
                {
                    var createdUser = new ApplicationUser { UserName = model.Email, Email = model.Email };
                    var createdResult = await UserManager.CreateAsync(createdUser, "PressfordUs3r!");

                    if (createdResult.Succeeded)
                    {
                        var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(ApplicationDbContext.Create()));

                        if (RoleManager.RoleExists("User"))
                        {
                            await UserManager.AddToRoleAsync(createdUser.Id, "User");
                        }
                        else
                        {
                            await RoleManager.CreateAsync(new IdentityRole("User"));
                            await UserManager.AddToRoleAsync(createdUser.Id, "User");
                        }

                        await SignInManager.SignInAsync(createdUser, isPersistent: false, rememberBrowser: false);
                    }
                }
            }
            else
            {
                if (model.Email.Contains("admin"))
                {
                    model.Password = "PressfordAdm1n!";
                }
                else
                {

                    model.Password = "PressfordUs3r!";
                }

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            }

            return RedirectToLocal(returnUrl);
        }

        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }



        #endregion
    }
}