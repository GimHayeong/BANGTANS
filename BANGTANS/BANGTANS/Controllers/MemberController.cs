using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BANGTANS.Models;
using System.IO;

namespace BANGTANS.Controllers
{
    public class MemberController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Member/
        // GET: Member/Index/1
        public ActionResult Index(int? id)
        {
            var memberViewModels = db.MemberViewModels.Include(m => m.Artist);
            if (id != null) {
                memberViewModels = memberViewModels.Where(a => a.ArtistId == id);
            }
            if (memberViewModels == null)
            {
                return HttpNotFound();
            }

            return View(memberViewModels.OrderByDescending(m => m.Id).ToList());
        }


        // GET: Member/GetList/1
        public ActionResult GetList(int id)
        {
            var memberViewModels = db.MemberViewModels.Where(a => a.ArtistId == id).Include(a => a.Artist);
            if(memberViewModels == null)
            {
                return HttpNotFound();
            }
            return PartialView("_MemberList", memberViewModels.ToList());
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel memberViewModel = db.MemberViewModels.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name");
            return View();
        }

        // POST: Member/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StageName,Name,Role,BirtyDay,Height,Weight,ArtistId,ImageUrl")] MemberViewModel memberViewModel, HttpPostedFileBase file)
        {
            bool isSavedFile = SaveAsFile(file);
            if (isSavedFile)
            {
                memberViewModel.ImageUrl = "~/Content/Images/Member/" + file.FileName;
                if (ModelState.IsValid)
                {
                    db.MemberViewModels.Add(memberViewModel);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name", memberViewModel.ArtistId);
            return View(memberViewModel);
        }

        private bool SaveAsFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && IsValidExtension(file))
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/Member/"), fileName);
                file.SaveAs(path);
                return true;
            }

            return false;
        }

        private Boolean IsValidExtension(HttpPostedFileBase file)
        {
            return Path.GetExtension(file.FileName).ToLower() == ".jpg"
                || Path.GetExtension(file.FileName).ToLower() == ".png"
                || Path.GetExtension(file.FileName).ToLower() == ".gif"
                || Path.GetExtension(file.FileName).ToLower() == ".jpeg";
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel memberViewModel = db.MemberViewModels.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name", memberViewModel.ArtistId);
            return View(memberViewModel);
        }

        // POST: Member/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StageName,Name,Role,BirtyDay,Height,Weight,ArtistId,ImageUrl")] MemberViewModel memberViewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                bool isFileChanged = SaveAsFile(file);

                db.Entry(memberViewModel).State = EntityState.Modified;
                if (isFileChanged) memberViewModel.ImageUrl = "~/Content/Images/Member/" + file.FileName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name", memberViewModel.ArtistId);
            return View(memberViewModel);
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberViewModel memberViewModel = db.MemberViewModels.Find(id);
            if (memberViewModel == null)
            {
                return HttpNotFound();
            }
            return View(memberViewModel);
        }

        // POST: Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberViewModel memberViewModel = db.MemberViewModels.Find(id);
            db.MemberViewModels.Remove(memberViewModel);
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
