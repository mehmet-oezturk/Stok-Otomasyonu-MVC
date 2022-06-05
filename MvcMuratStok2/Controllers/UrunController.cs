using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMuratStok2.Models.Entity;//bunu ekliyoruz

namespace MvcMuratStok2.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStok2Entities1 db = new MvcDbStok2Entities1();
        public ActionResult Index()
        {
            var degerler = db.TBLURUN.ToList();
            return View(degerler);
        }
        public ActionResult UrunEkle()
        { List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList() select new SelectListItem
        {
            Text=i.KATEGORIAD,
            Value=i.KATEGORIID.ToString()
        }).ToList();
            ViewBag.dg = degerler;//combo box için oluşturduğumuz degerleri diğer sayfaya taşımak için kullnılan kalıp
            return View();
        }
        [HttpPost]// herhangi bir işlem gerçekleştiği zaman orn kaydet butununa bastıgım zaman buradaki işlemi gerçekleştir
        public ActionResult UrunEkle(TBLURUN p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBLKATEGORILER= ktg;//kategoriden gelen değeri atar

            db.TBLURUN.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");//işlem bittikten sonra index sayfasına yönlendir
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUN.Find(id);
            db.TBLURUN.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUN.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList()//ürün eklede kategori isimlerini getiriyoruz
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dg = degerler;
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUN p)
        {
            var urun = db.TBLURUN.Find(p.URUNID);
            urun.URUNAD = p.URUNAD;
            urun.URUNMARKA = p.URUNMARKA;
            urun.STOK = p.STOK;
            urun.FİYAT = p.FİYAT;
            //urun.URUNKATEGORI = p.URUNKATEGORI;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p.TBLKATEGORILER.KATEGORIID).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;// burası başka tablodan bilgi çekme yani urun kategorisinin ıdsinden kategori adını getirtiyoruz inner join gibi yapıyoruz
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}