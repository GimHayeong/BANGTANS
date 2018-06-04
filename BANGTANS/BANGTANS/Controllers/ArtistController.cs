using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BANGTANS.Models;

namespace BANGTANS.Controllers
{
    public class ArtistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artist
        public ActionResult Index()
        {
            return View(db.ArtistViewModels.ToList());
        }

        // GET: Artist/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
            if (artistViewModel == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModel);
        }

        // GET: Artist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DebutDate,Description")] ArtistViewModel artistViewModel)
        {
            if (ModelState.IsValid)
            {
                db.ArtistViewModels.Add(artistViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artistViewModel);
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
            if (artistViewModel == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModel);
        }

        // POST: Artist/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DebutDate,Description")] ArtistViewModel artistViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artistViewModel);
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
            if (artistViewModel == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModel);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
            db.ArtistViewModels.Remove(artistViewModel);
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
