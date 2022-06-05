using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMuratStok2.Models.Entity;

namespace MvcMuratStok2.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        MvcDbStok2Entities1 db = new MvcDbStok2Entities1();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TBLSATISLAR p)
        {
            db.TBLSATISLAR.Add(p);
            db.SaveChanges();
            return View("Index");
        }
    }
}