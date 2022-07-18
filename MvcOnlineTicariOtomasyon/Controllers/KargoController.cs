using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.kargoDetays.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakter = { "A", "B", "C", "D", "F", "G", "H", "J", "K", "L", "M", "N", "X", "Z", "W", "Q", "T", "Y", "R", "O", "U", "P", "S" };
            int k1, k2,k3,k4,k5;
            k1 = rnd.Next(0, karakter.Length);
            k2 = rnd.Next(0, karakter.Length);
            k3 = rnd.Next(0, karakter.Length);
            k4 = rnd.Next(0, karakter.Length);
            k5 = rnd.Next(0, karakter.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(1, 9);
            s3 = rnd.Next(1, 9);
            string kod = karakter[k1] + karakter[k2] + s1.ToString() + karakter[k3] + karakter[k4] + s2 + s3 + karakter[k5];
            ViewBag.TakipKodu = kod;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKargo(KargoDetay p)
        {
            c.kargoDetays.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoTakip(string id)
        {
            var degerler = c.kargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

    }
}