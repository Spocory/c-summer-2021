using Learning.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Learning.Business;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace Learning.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager userManager;
        private readonly IEnrollManager enrollManager;
        public HomeController(IClassManager classManager,
                              IUserManager userManager,
                              IEnrollManager enrollManager)
        {
            this.classManager = classManager;
            this.userManager = userManager;
            this.enrollManager = enrollManager;
        }

        [Authorize]
        public ActionResult studentclasses(int id)
        {
            if (JsonConvert.DeserializeObject<Models.UserModel>(HttpContext.Session?.GetString("User")) == null)
            {
                return RedirectToAction("LogIn");
            }

            var user = JsonConvert.DeserializeObject<Models.UserModel>(HttpContext.Session.GetString("User"));
    

            if (id != 0)
            {
                var item = enrollManager.Add(user.Id, id);
            }

            var items = enrollManager.GetAll(user.Id)
                    .Select(t => new Learning.WebSite.Models.EnrollModel
                    {
                        UserId = t.UserId,
                        ClassId = t.ClassId,
                        Name = t.Name,
                        Description = t.Description,
                        Price = t.Price
                        
                  
                    })
                    .ToArray();
            return View(items);
        }

        [Authorize]
        public ActionResult enrollinclass()
        {
            //if ((JsonConvert.DeserializeObject<Models.UserModel>(HttpContext.Session.GetString("User")) == null))
            //{
            //    return RedirectToAction("LogIn");
            //}

            var user = JsonConvert.DeserializeObject<Models.UserModel>(HttpContext.Session.GetString("User"));

            var classes = classManager.Classes
                                  .Select(t => new Learning.WebSite.Models.ClassModel(t.Id, t.Name, t.Price, t.Description))
                                  .ToArray();



            var model = new ClassListModel { Classes = classes };
 
             return View(model);
        }

        public ActionResult ClassList()
        {
            var classes = classManager.Classes
                                            .Select(t => new Learning.WebSite.Models.ClassModel(t.Id, t.Name, t.Price, t.Description))
                                            .ToArray();


            var model = new ClassListModel { Classes = classes };
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public ActionResult LogIn()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new Learning.WebSite.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    });

                    HttpContext.Session.SetString("User", json);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User"),
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1440),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("~/");
        }







        public ActionResult Register()
        {
            ViewData["ReturnUrl"] = Request.Query["returnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.UserName, registerModel.Password, registerModel.ConfirmPassword);

                if (user == null)
                {
                   ModelState.AddModelError("", "User name and password do not match or Account already exists..");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new Learning.WebSite.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    });

                    HttpContext.Session.SetString("User", json);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User"),
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return Redirect(returnUrl ?? "~/");
                }
            }

            ViewData["ReturnUrl"] = returnUrl;

            return View(registerModel);
        }
    }
}
