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
    public class AlbumController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Album
        public ActionResult Index()
        {
            var albumViewModels = db.AlbumViewModels.Include(a => a.Artist);
            return View(albumViewModels.OrderByDescending(m => m.Id).ToList());
        }

        public ActionResult GetList(int id)
        {
            var albumViewModels = db.AlbumViewModels.Where(a => a.ArtistId == id).Include(a => a.Artist);
            return PartialView("_AlbumList", albumViewModels.OrderByDescending(m => m.Id).ToList());
        }

        // GET: Album/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumViewModel albumViewModel = db.AlbumViewModels.Find(id);
            if (albumViewModel == null)
            {
                return HttpNotFound();
            }
            return View(albumViewModel);
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name");
            return View();
        }

        // POST: Album/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Subtitle,ReleasedDate,ImageUrl,Description,ArtistId")] AlbumViewModel albumViewModel, HttpPostedFileBase file)
        {
            bool isSavedFile = SaveAsFile(file);
            if (isSavedFile)
            {
                albumViewModel.ImageUrl = "~/Content/Images/Album/" + file.FileName;
                
                if (ModelState.IsValid)
                {
                    db.AlbumViewModels.Add(albumViewModel);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("ImageUrl", "Please, upload file.");
            }           

            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name", albumViewModel.ArtistId);
            return View(albumViewModel);
        }

        private bool SaveAsFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0 && IsValidExtension(file))
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/Album/"), fileName);
                file.SaveAs(path);
                return  true;
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

        // GET: Album/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumViewModel albumViewModel = db.AlbumViewModels.Find(id);
            if (albumViewModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name", albumViewModel.ArtistId);
            return View(albumViewModel);
        }

        // POST: Album/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하십시오. 
        // 자세한 내용은 http://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하십시오.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Subtitle,ReleasedDate,ImageUrl,Description,ArtistId")] AlbumViewModel albumViewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                bool isFileChanged = SaveAsFile(file);

                db.Entry(albumViewModel).State = EntityState.Modified;
                if(isFileChanged) albumViewModel.ImageUrl = "~/Content/Images/Album/" + file.FileName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.ArtistViewModels, "Id", "Name", albumViewModel.ArtistId);
            return View(albumViewModel);
        }

        // GET: Album/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumViewModel albumViewModel = db.AlbumViewModels.Find(id);
            if (albumViewModel == null)
            {
                return HttpNotFound();
            }
            return View(albumViewModel);
        }

        // POST: Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlbumViewModel albumViewModel = db.AlbumViewModels.Find(id);
            db.AlbumViewModels.Remove(albumViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Video()
        {
            ViewBag.VideoSources = new List<string>()
            {
                "https://youtu.be/MBdVXkSdhwU", "https://youtu.be/ALj5MKjy2BU", "https://youtu.be/BVwAVbKYYeM"
                , "https://youtu.be/hmE9f-TEutc", "https://youtu.be/NMdTd9e-LEI", "https://youtu.be/9DwzBICPhdM"
                , "https://youtu.be/GZjt_sA2eso", "https://youtu.be/bagj78IQ3l0", "https://youtu.be/m8MfJg68oCs"
            };

            ViewBag.VideoTitles = new List<string>()
            {
                "DNA", "불타오르네", "DOPE: 쩔어", "피 땀 눈물", "I NEED YOU", "Not Today", "Save ME", "Danger", "상남자"
            };

            return View();
        }

        [ActionName("aspnet-mvc-helper")]
        public ActionResult VideoList()
        {
            ViewBag.VideoSources = new List<string>()
            {
                "https://youtu.be/MBdVXkSdhwU", "https://youtu.be/ALj5MKjy2BU", "https://youtu.be/BVwAVbKYYeM"
                , "https://youtu.be/hmE9f-TEutc", "https://youtu.be/NMdTd9e-LEI", "https://youtu.be/9DwzBICPhdM"
                , "https://youtu.be/GZjt_sA2eso", "https://youtu.be/bagj78IQ3l0", "https://youtu.be/m8MfJg68oCs"
            };

            ViewBag.VideoTitles = new List<string>()
            {
                "DNA", "불타오르네", "DOPE: 쩔어", "피 땀 눈물", "I NEED YOU", "Not Today", "Save ME", "Danger", "상남자"
            }; 

            return View("aspnet-mvc-helper");
        }

        [ActionName("fallback-video")]
        public ActionResult FallbackVideo()
        {
            ViewBag.VideoSources = new List<string>()
            {
                "https://youtu.be/MBdVXkSdhwU", "https://youtu.be/ALj5MKjy2BU", "https://youtu.be/BVwAVbKYYeM"
                , "https://youtu.be/hmE9f-TEutc", "https://youtu.be/NMdTd9e-LEI", "https://youtu.be/9DwzBICPhdM"
                , "https://youtu.be/GZjt_sA2eso", "https://youtu.be/bagj78IQ3l0", "https://youtu.be/m8MfJg68oCs"
            };

            ViewBag.VideoTitles = new List<string>()
            {
                "DNA", "불타오르네", "DOPE: 쩔어", "피 땀 눈물", "I NEED YOU", "Not Today", "Save ME", "Danger", "상남자"
            };

            return View("fallback-video");
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
