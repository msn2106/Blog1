using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Model;
using Blog.DB.DBOperations;
using System.Net;

namespace Blog1.Controllers
{
    public class HomeController : Controller
    {
        UserRepository repo = null;

        public HomeController()
        {
            repo = new UserRepository();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "BlogModel");
        }

        [Authorize]
        public ActionResult GetAllUsers()
        {
            var result = repo.GetAllUsers();
            return View(result);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = repo.GetUser((int)id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var user = repo.GetUser(id);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                repo.EditDetails(model.Id, model);
                return RedirectToAction("GetAllUsers");
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = repo.GetUser((int)id);
            if(user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteUser(id);
            return RedirectToAction("GetAllUsers");
        }

        [AllowAnonymous]
        public ActionResult AboutUs()
        {
            return View();
        }
    }
}