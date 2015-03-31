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
using System.IO;
using System.Web.Security;

namespace Resume.Controllers
{
    public class BlogArticleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Admin Index
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex(int? page)
        {
            var posts = db.Article.AsQueryable();
            posts = posts.OrderByDescending(p => p.CreateDate);
            return View(posts.ToPagedList(page ?? 1, 10));
        }

        // Index
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
        public ActionResult Details(string Slug)
        {
            if(String.IsNullOrWhiteSpace(Slug))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogArticle post = db.Article.FirstOrDefault(p => p.Slug == Slug);
            if (post == null) 
            {
                return HttpNotFound();
            }
            
            return View(post);
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
        public ActionResult Create([Bind(Include = "Id,Title,Body,MediaUrl,Published")] BlogArticle blogArticle, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blogArticle.Title);
                if(String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(blogArticle);
                }
                if(db.Article.Any(p=>p.Slug == Slug))
                {
                    ModelState.AddModelError("Title", "The title must be unique.");
                    return View(blogArticle);
                }
                if (image != null)
                {
                    if (image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        if ((fileExtension == ".jpg") || (fileExtension == ".gif") || (fileExtension == ".png"))
                        {
                            var path = Server.MapPath("~/img/Blog/");
                            // if directory doesnt exist, create it
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            else
                                // if directory exists, check if file exists in directory
                            {
                                if(Directory.Exists(fileName))
                                {
                                    blogArticle.MediaUrl = "/img/Blog/" + fileName;
                                }
                                else
                                {
                                    blogArticle.MediaUrl = "/img/Blog/" + fileName;
                                    image.SaveAs(Path.Combine(path, fileName));
                                }
                            }
                            
                        }
                        else
                        {
                            ModelState.AddModelError("image", "Invalid image extension. Only .gif, .jpg, and .png are allowed");
                            return View(blogArticle);
                        }
                    }
                }
                blogArticle.CreateDate = DateTimeOffset.Now;
                blogArticle.UpdateDate = DateTimeOffset.Now;
                blogArticle.Slug = Slug;                                                                                                                                                      
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
        public ActionResult Edit([Bind(Include = "Id,CreateDate,Title,Body,MediaUrl,Published")] BlogArticle blogArticle, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                var Slug = StringUtilities.URLFriendly(blogArticle.Title);
                if (String.IsNullOrWhiteSpace(Slug))
                {
                    ModelState.AddModelError("Title", "Invalid title.");
                    return View(blogArticle);
                }
                if (blogArticle.Slug != Slug)
                {
                    if (db.Article.Any(p => p.Slug == Slug && p.Id != blogArticle.Id))
                    {
                        ModelState.AddModelError("Title", "The title must be unique.");
                        return View(blogArticle);
                    }
                }
                
                if (image != null)
                {
                    if (image.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(image.FileName);
                        var fileExtension = Path.GetExtension(fileName);
                        if ((fileExtension == ".jpg") || (fileExtension == ".gif") || (fileExtension == ".png"))
                        {
                            var path = Server.MapPath("~/img/Blog/");
                            // if directory doesnt exist, create it
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }
                            else
                            // if directory exists, check if file exists in directory
                            {
                                if (Directory.Exists(fileName))
                                {
                                    blogArticle.MediaUrl = "/img/Blog/" + fileName;
                                }
                                else
                                {
                                    blogArticle.MediaUrl = "/img/Blog/" + fileName;
                                    image.SaveAs(Path.Combine(path, fileName));
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("image", "Invalid image extension. Only .gif, .jpg, and .png are allowed");
                            return View(blogArticle);
                        }
                    }
                }  
                //should be testing field to see if modified to determine if updated with below
                //if(db.Entry(blogArticle).Property(p=> p.Body).IsModified)

                db.Entry(blogArticle).State = EntityState.Modified;
                blogArticle.UpdateDate = DateTimeOffset.Now;
                blogArticle.Slug = Slug;
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
        public ActionResult Comment([Bind(Include = "ArticleId, Message")] string Slug, BlogComment blogComment)
        {
            if (ModelState.IsValid)
            {
                if(blogComment.Message != null)
                {
                    blogComment.AuthorId = User.Identity.GetUserId();
                    blogComment.CreateDate = DateTimeOffset.Now;
                    blogComment.UpdateDate = DateTimeOffset.Now;
                    db.Comment.Add(blogComment);
                    db.SaveChanges();
                }                
            }
            return RedirectToAction("Details", new { Slug = Slug});
        }

        // GET: BlogArticles/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DltComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogComment blogComment = db.Comment.Find(id);
            if (blogComment == null)
            {
                return HttpNotFound();
            }
            return View(blogComment);
        }

        // delete a comment
        [HttpPost, ActionName("DltComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DltCommentConfirmed(int id)
        {
            BlogComment blogComment = db.Comment.Find(id);
            BlogArticle blogArticle = db.Article.Find(blogComment.ArticleId);

            db.Comment.Remove(blogComment);
            db.SaveChanges();
            return RedirectToAction("Details", new { Slug = blogArticle.Slug });
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
