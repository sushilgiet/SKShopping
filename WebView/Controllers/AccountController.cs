using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using WebView.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WebView.Repositories;

namespace WebView.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: AccountController
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            AppUser user =UserRepository.GetUser(model.UserName,model.Password);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,user.Roles[0].RoleName)
            };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                new AuthenticationProperties {IsPersistent=false }).GetAwaiter().GetResult();
            //redirection attack 
           // return   RedirectToAction("Index", "Weather");
            return Redirect(model.ReturnUrl);
           // return Redirect("~/Weather");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = "~/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(returnUrl);
            }
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("~/");
        }

      

       
    }
}
