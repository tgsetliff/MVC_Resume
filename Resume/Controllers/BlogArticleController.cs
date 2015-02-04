using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Resume.Models;

namespace Resume.Controllers
{
    public class BlogArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BlogArticles
        public ActionResult Index()
        {
            return View(db.Article.ToList());
        }

        // GET: BlogArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogArticle blogArticle = db.Article.Find(id);
            if (blogArticle == null)
            {
                return HttpNotFound();
            }
            return View(blogArticle);
        }

        // GET: BlogArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogArticles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreateDate,UpdateDate,Title,Body,MediaUrl,Published,Slug")] BlogArticle blogArticle)
        {
            if (ModelState.IsValid)
            {
                db.Article.Add(blogArticle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blogArticle);
        }

        // GET: BlogArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogArticle blogArticle = db.Article.Find(id);
            if (blogArticle == null)
            {
                return HttpNotFound();
            }
            return View(blogArticle);
        }

        // POST: BlogArticles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreateDate,UpdateDate,Title,Body,MediaUrl,Published,Slug")] BlogArticle blogArticle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blogArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blogArticle);
        }

        // GET: BlogArticles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogArticle blogArticle = db.Article.Find(id);
            if (blogArticle == null)
            {
                return HttpNotFound();
            }
            return View(blogArticle);
        }

        // POST: BlogArticles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogArticle blogArticle = db.Article.Find(id);
            db.Article.Remove(blogArticle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
