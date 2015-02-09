using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Resume.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace Resume.Controllers
{
    public class BlogArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Get
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex(int? page)
        {
            var posts = db.Article.AsQueryable();
            posts = posts.OrderByDescending(p => p.CreateDate);
            return View(posts.ToPagedList(page ?? 1, 10));
        }


        public ActionResult Index(int? page, string query)
        {
            var posts = db.Article.AsQueryable();
            if (!String.IsNullOrWhiteSpace(query))
            {
                posts = posts.Where(p => p.Title.Contains(query) || p.Body.Contains(query));
                ViewBag.Query = query;
            }
            posts = posts.OrderByDescending(p => p.CreateDate);
            return View(posts.ToPagedList(page ?? 1, 3));
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
        [Authorize(Roles="Admin")]
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
            }

            return RedirectToAction("Index");
        }

        // GET: BlogArticles/Edit/5
        [Authorize(Roles="Admin")]
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
        [Authorize(Roles="Admin")]
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

        // add a comment
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Comment([Bind(Include = "ArticleId, Message")] BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                blogComment.AuthorId = User.Identity.GetUserId();
                blogComment.CreateDate = DateTimeOffset.Now;
                db.Comment.Add(blogComment);
                db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = blogComment.ArticleId });
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
