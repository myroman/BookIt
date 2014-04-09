using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

using BookIt.Api.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BookIt.Api.Controllers
{
    [System.Web.Http.Authorize]
    public class AccountController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserManagerExtensions
                var user = new ApplicationUser() { UserName = model.UserName };
                new UserManager<ApplicationUser, string>().CreateAsync(user, model.Password);
                //var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}