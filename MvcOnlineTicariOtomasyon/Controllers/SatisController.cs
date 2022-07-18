using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            CascadingSatis cs = new CascadingSatis();

            //List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
            //                               orderby x.UrunAd ascending
            //                               select new SelectListItem
            //                               {
            //                                   Text = x.UrunAd,
            //                                   Value = x.UrunID.ToString()

            //                               }).ToList();
            cs.Kategoriler  = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunID", "UrunAd");
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           orderby x.CariAd ascending
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd+ " "+x.CariSoyad,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           orderby x.PersonelAd ascending
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd +" "+x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();
            List<SelectListItem> deger4 = (from x in c.Uruns.ToList()
                                           orderby x.Marka ascending
                                           select new SelectListItem
                                           {
                                               Text = x.Marka,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            //ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View(cs);
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket p)
        {
            p.Tarih = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Uruns.ToList()
                                           orderby x.UrunAd ascending
                                           select new SelectListItem
                                           {
                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()

                                           }).ToList();
            List<SelectListItem> deger2 = (from x in c.Carilers.ToList()
                                           orderby x.CariAd ascending
                                           select new SelectListItem
                                           {
                                               Text = x.CariAd + " " + x.CariSoyad,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in c.Personels.ToList()
                                           orderby x.PersonelAd ascending
                                           select new SelectListItem
                                           {
                                               Text = x.PersonelAd + " " + x.PersonelSoyad,
                                               Value = x.PersonelID.ToString()
                                           }).ToList();

            List<SelectListItem> deger4 = (from x in c.Uruns.ToList()
                                           orderby x.UrunAd ascending
                                           select new SelectListItem
                                           {

                                               Text = x.Marka,
                                               Value = x.UrunID.ToString()
                                           }).ToList();

            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            var degerler = c.SatisHarekets.Find(id);
            return View("SatisGetir", degerler);
        }
        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var deger = c.SatisHarekets.Find(p.SatisID);
            deger.Urunid = p.Urunid;
            deger.Cariid = p.Cariid;
            deger.Personelid = p.Personelid;
            deger.Adet = p.Adet;
            deger.Fiyat = p.Fiyat;
            deger.ToplamTutar = p.ToplamTutar;
            deger.Tarih = p.Tarih;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var deger = c.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(deger);
        }
        
        public ActionResult SatisCascading()
        {
            CascadingSatis cs = new CascadingSatis();
            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "UrunID", "UrunAd");
            return View(cs);
        }
       
        public JsonResult UrunGetir(int p)
        {
            var UrunListesi = (from x in c.Uruns
                               join y in c.Kategoris on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.UrunID.ToString()
                               }).ToList();
            return Json(UrunListesi, JsonRequestBehavior.AllowGet);
        }
    }
}