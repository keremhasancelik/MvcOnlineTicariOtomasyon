using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System.Net;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alan == mail).ToList();
            ViewBag.m = mail;
            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var tsatis = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.topsatis = tsatis;
            var tutar = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y=>y.ToplamTutar);
            ViewBag.avtutari = tutar;
            var urunadet = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet);
            ViewBag.turunadet = urunadet;
            var adsoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.isim = adsoyad;
            var posta = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariMail).FirstOrDefault();
            ViewBag.mail = posta;
            var resim = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariGorsel).FirstOrDefault();
            ViewBag.gorsel = resim;
            var meslek = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariMeslek).FirstOrDefault();
            ViewBag.msk = meslek;
            var sehir = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.shr = sehir;
            var telefon = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariTelefon).FirstOrDefault();
            ViewBag.tlf = telefon;
            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y=>y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x=>x.Alan==mail).OrderByDescending(x => x.MesajID).ToList();
            var gelenmesaj = c.Mesajlars.Count(x => x.Alan == mail);
            ViewBag.d1 = gelenmesaj;
            var gidenmesaj = c.Mesajlars.Count(x => x.Gonderen == mail);
            ViewBag.d2 = gidenmesaj;
            return View(degerler);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Gonderen == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gidenmesaj = c.Mesajlars.Count(x => x.Gonderen == mail);
            ViewBag.d2 = gidenmesaj;
            var gelenmesaj = c.Mesajlars.Count(x => x.Alan == mail);
            ViewBag.d1 = gelenmesaj;
            return View(degerler);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var deger = c.Mesajlars.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Gonderen == mail).ToList();
            var gidenmesaj = c.Mesajlars.Count(x => x.Gonderen == mail);
            ViewBag.d2 = gidenmesaj;
            var gelenmesaj = c.Mesajlars.Count(x => x.Alan == mail);
            ViewBag.d1 = gelenmesaj;
            return View(deger);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Gonderen == mail).ToList();
            var gidenmesaj = c.Mesajlars.Count(x => x.Gonderen == mail);
            ViewBag.d2 = gidenmesaj;
            var gelenmesaj = c.Mesajlars.Count(x => x.Alan == mail);
            ViewBag.d1 = gelenmesaj;

            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            var mail = (string)Session["CariMail"];
            p.Gonderen = mail;
            c.Mesajlars.Add(p);
            c.SaveChanges();
            return View();

        }
        [Authorize]
        public ActionResult KargoTakip(string id)
        {
            var kargo = from x in c.kargoDetays select x;
            kargo = kargo.Where(y => y.TakipKodu.Contains(id));
            return View(kargo.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.kargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
       
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var caribul = c.Carilers.Find(id);
            return PartialView("Partial1",caribul);
        }

        public ActionResult BilgiGuncelle(Cariler p)
        {
            var cari = c.Carilers.Find(p.CariID);
            cari.CariAd = p.CariAd;
            cari.CariSoyad = p.CariSoyad;
            cari.CariMail = p.CariMail;
            cari.CariSehir = p.CariSehir;
            cari.CariMeslek = p.CariMeslek;
            cari.CariTelefon = p.CariTelefon;
            cari.CariSifre = p.CariSifre;
            cari.CariGorsel = p.CariGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult Partial2()
        {
            var duyuru = c.Mesajlars.Where(x => x.Gonderen == "admin").ToList();
            return PartialView(duyuru);
        }
    }
}