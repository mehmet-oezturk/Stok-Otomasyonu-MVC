using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMuratStok2.Models.Entity;//bunu ekliyoruz

namespace MvcMuratStok2.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStok2Entities1 db = new MvcDbStok2Entities1();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));//ARAMA
            }
            //var degerler = db.TBLMUSTERILER.ToList();
            return View(degerler.ToList());
        }
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]// herhangi bir işlem gerçekleştiği zaman orn kaydet butununa bastıgım zaman buradaki işlemi gerçekleştir
        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLMUSTERILER.Find(id);//ilk başta silinecek alanı burada buluyoruz
            db.TBLMUSTERILER.Remove(kategori);//burada bulunan alanı siliyoruz
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)//1. adım buraya view ekle güncelleme için açtık burayı
        {
            var kt = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", kt);
        }
        public ActionResult Guncelle(TBLMUSTERILER p2)
        {
            var muste = db.TBLMUSTERILER.Find(p2.MUSTERIID);
            muste.MUSTERIAD = p2.MUSTERIAD;
            muste.MUSTERISOYAD = p2.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}