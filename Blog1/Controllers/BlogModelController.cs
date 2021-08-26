using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.DB.DBOperations;
using Blog.Model;

namespace Blog1.Controllers
{
    public class BlogModelController : Controller
    {
        private BlogRepository db = null;
        UserRepository user = new UserRepository();

        public BlogModelController()
        {
            db = new BlogRepository();
        }

        // GET: BlogModel
        [AllowAnonymous]
        public ActionResult Index()
        {
            var result = db.GetAllBlogs();
            return View(result);
        }

        // GET: BlogModel/Details/5
        public ActionResult Details(String author)
        {
            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blogModel = db.GetBlog(author);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // GET: BlogModel/Create
        public ActionResult Create()
        {
            //ViewBag.DOP = DateTime.Now.ToString("dd MMMM yyyy hh:mm:ss tt");
            //ViewBag.Author = User.Identity.Name;
            
            return View();
        }

        // POST: BlogModel/Create
        // [Bind(Include = "Id,DOP,Title,Description,Author,Likes,Report")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BlogModel blogModel)
        {
            var repo = new UserRepository();
            var datetime = DateTime.Now;
            blogModel.DOP = datetime.ToString("dd MMMM yyyy hh:mm:ss tt");
            //blogModel.Author = User.Identity.Name;

            var users = repo.GetAllUsers().FirstOrDefault(x => x.UserName == User.Identity.Name);

            blogModel.UserID = users.Id;
            blogModel.Author = users.UserName;

            if (ModelState.IsValid)
            {
                int id = db.AddBlog(blogModel);
                if (id > 0)
                {
                    ModelState.Clear();
                    ViewBag.UserAdded = "Blog Added Successfully";
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            return View(blogModel);
        }

        // GET: BlogModel/Edit/5
        public ActionResult Edit(string author)
        {
            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.GetBlog(author);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // POST: BlogModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DOP,Title,Description,Author,Likes,Report")] BlogModel blogModel)
        {
            if (ModelState.IsValid)
            {
                db.EditBlog(blogModel.Id, blogModel);
                return RedirectToAction("Index");
            }
            return View(blogModel);
        }

        // GET: BlogModel/Delete/5
        public ActionResult Delete(string author)
        {
            if (author == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogModel blogModel = db.GetBlog(author);
            if (blogModel == null)
            {
                return HttpNotFound();
            }
            return View(blogModel);
        }

        // POST: BlogModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteBlog(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
