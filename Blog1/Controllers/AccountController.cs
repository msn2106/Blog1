using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Model;
using Blog.DB.DBOperations;
using System.Web.Security;

namespace Blog1.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        UserRepository repository = null;

        public AccountController()
        {
            repository = new UserRepository();
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Login(Blog.Model.Membership model) //Because of confusion between Membership in Model and Membership in Web.Security
        {
            if (ModelState.IsValid)
            {
                bool isValid = repository.Login(model);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false); // the second parameter gives a checkbox with option of stay signed in which by false is unticked by default
                    return RedirectToAction("Index","BlogModel");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username or Password");
                    return View();
                }
            }
            else return View();
        }
        
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Signup(UserModel model)
        {

            if (ModelState.IsValid)
            {
                int id = repository.AddUser(model);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.UserAdded = "User Added Successfully";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
    }
}