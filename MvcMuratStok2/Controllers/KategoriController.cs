using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMuratStok2.Models.Entity;//bunu ekliyoruz
using PagedList;
using PagedList.Mvc;//pagedlist i yükledikten sonra bu ikisini kütüphanelere ekliyoruz

namespace MvcMuratStok2.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        MvcDbStok2Entities1 db = new MvcDbStok2Entities1();

        public ActionResult Index(int sayfa=1)
        {
            /* var degerler = db.TBLKATEGORILER.ToList();*///LİSTELEME
            var degerler = db.TBLKATEGORILER.ToList().ToPagedList(sayfa, 4);//bu işlemi listenenleri sayfalara ayırmak için yaparızToPagedList da 1.parametre kaçıncı saydadan başlayacagı 2.parametre sayfada kaç adet bilgi olacağı
            //bunları yaptıktan sonra İNDEX SAYFASINDAKİ MODELİ DEĞİSTİRECEZ
            return View(degerler);
        }
        [HttpGet] //herhabgi bir işlem yapmazsam sadece sayfayı geri döndür 
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]// herhangi bir işlem gerçekleştiği zaman orn kaydet butununa bastıgım zaman buradaki işlemi gerçekleştir
        public ActionResult YeniKategori(TBLKATEGORILER p1)
        {   //if deki sorgu eger modelin dogrulanma işlemi yapılmadıysa
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORILER.Add(p1);
            db.SaveChanges();
            return View();
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.TBLKATEGORILER.Find(id);//ilk başta silinecek alanı burada buluyoruz
            db.TBLKATEGORILER.Remove(kategori);//burada bulunan alanı siliyoruz
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)//1. adım buraya view ekle güncelleme için açtık burayı
        {
            var kt = db.TBLKATEGORILER.Find(id);
            return View("KategoriGetir", kt);
        }
        public ActionResult Guncelle(TBLKATEGORILER p1)
        {
            var kt = db.TBLKATEGORILER.Find(p1.KATEGORIID);
            kt.KATEGORIAD = p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}