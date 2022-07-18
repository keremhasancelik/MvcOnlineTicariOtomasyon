using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacaklarController : Controller
    {
        // GET: Yapilacaklar
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3 = (from x in c.Carilers select x.CariSehir).Distinct().Count();
            ViewBag.d3 = deger3;
            var deger4 = c.Uruns.Sum(x => x.Stok);
            ViewBag.d4 = deger4;
            var deger = (from x in c.Yapilacaklars orderby x.YapilacaklarID descending select x).ToList();
            return View(deger);
        }
        [HttpGet]
        public ActionResult YapilacakEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YapilacakEkle(Yapilacaklar p)
        {
            c.Yapilacaklars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YapilacakGetir(int id)
        {
            var dpt = c.Yapilacaklars.Find(id);
            return View("YapilacakGetir", dpt);
        }
        public ActionResult YapilacaklarGuncelle(Yapilacaklar p)
        {
            var deger = c.Yapilacaklars.Find(p.YapilacaklarID);
            deger.Baslik = p.Baslik;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}