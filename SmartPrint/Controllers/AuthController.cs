using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartPrint.Models;
using System.Security.Claims;
using SmartPrint.CustomLibraries;

namespace SmartPrint.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {

        /*

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        */

        [HttpGet]
        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(Users model)
        {
            if (!ModelState.IsValid) //Checks if input fields have the correct format
            {
                return
                    View(model); //Returns the view with the input values so that the user doesn't have to retype again
            }

            using (var db = new MainDbContext())
            {

                var emailCheck = db.Users.FirstOrDefault(u => u.UserEmail== model.UserEmail);
                var getPassword = db.Users.Where(u => u.UserEmail == model.UserEmail).Select(u => u.UserPass);
                var materializePassword = getPassword.ToList();
                var password = materializePassword[0];
                var decryptedPassword = CustomDecrypt.Decrypt(password);

                if (model.UserEmail != null && model.UserPass == decryptedPassword)
                //Checks whether the input is the same as those literals. Note: Never ever do this! This is just to demo the validation while we're not yet doing any database interaction
               // if (model.UserEmail == "admin@admin.com" & model.UserPass == "123456")
                {
                    var getName = db.Users.Where(u => u.UserEmail == model.UserEmail).Select(u =>u.FName);
                    var materializeName = getName.ToList();
                    var name = materializeName[0];

                    var getUserId = db.Users.Where(u => u.UserEmail == model.UserEmail).Select(u => u.UserId);
                    var materializeUserId = getUserId.ToList();
                    var userId=  materializeUserId[0];
                    //var getCountry = db.Users.Where(u = &amp; gt; u.Email == model.Email).Select(u = &amp; gt; u.Country);
                    //var materializeCountry = getCountry.ToList();
                    //var country = materializeCountry[0];

                    var getEmail = db.Users.Where(u =>u.UserEmail== model.UserEmail).Select(u =>u.UserEmail);
                    var materializeEmail = getEmail.ToList();
                    var email = materializeEmail[0];

                    var identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Email, email),
                        //new Claim(ClaimTypes., userId.ToString())

                    }, "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;
                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
        }

        ModelState.AddModelError("", "Invalid email or password");


            return View(model); //Should always be declared on the end of an action method
           
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        public ActionResult Registration()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult Registration(Users model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MainDbContext())
                {
                    var encryptedPassword = CustomEnrypt.Encrypt(model.UserPass);
                    var user = db.Users.Create();
                    user.FName= model.FName;
                    user.LName = model.LName;
                      user.UserTypeId= model.UserTypeId;
                   user.UserCode = model.UserCode;
                    user.UserEmail = model.UserEmail;
                    user.UserPass= encryptedPassword;
                   user.IsActive= model.IsActive;
                    user.UserPhone= model.UserPhone;
                    user.AddedOn = DateTime.Now;
                    user.EditedOn= DateTime.Now;
                    user.RowStatus= model.RowStatus;
                    db.Users.Add(user);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been");
            }
            return View();
        }

    }
}