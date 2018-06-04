using BANGTANS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BANGTANS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Picture picture)
        {
            foreach (var file in picture.Files)
            {
                SaveAsFile(file);
            }
            
            return RedirectToAction("Index");
        }

        private void SaveAsFile(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images/Album/"), fileName);
                file.SaveAs(path);
            }
        }
        

        public ActionResult About()
        {
            ViewBag.Message = "Description";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}