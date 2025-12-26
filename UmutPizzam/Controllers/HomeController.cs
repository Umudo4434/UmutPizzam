using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UmutPizzam.DataModel;
using UmutPizzam.Models;

namespace UmutPizzam.Controllers
{
    public class HomeController : Controller
    {
        private UmutPizzamEntities1 _umutpizzam;
        UmutPizzamEntities1 db = new UmutPizzamEntities1();
        public HomeController() 
        {
            _umutpizzam = new UmutPizzamEntities1();
        }
       public ActionResult AdminSayfa2()
       {
            if (Session["KullaniciAdi"] == null)
            {
                HttpCookie cookie = Request.Cookies["AdminLogin"];
                if (cookie == null)
                {
                    return RedirectToAction("Giris", "Admin");
                }
                else
                {
                    Session["KullaniciAdi"] = cookie.Value;
                }
            }

            Class1 kmenum = new Class1();
            var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
            var yorumm = (from Yorumlar in _umutpizzam.Yorumlars select Yorumlar).ToList();
            kmenum.Yorumlars = yorumm;
            kmenum.Menulers = menum;
            return View(kmenum);
        }
        [HttpPost]
        public ActionResult AdminSayfa2(FormCollection fc)
        {
            try
            {
                Class1 kmenum = new Class1();

                if (fc["MenuSil"] == "Sil")
                {
                    var arananmenu = fc["menuID"];
                    var menuu = db.Yorumlars.FirstOrDefault(k => k.Id.ToString() == arananmenu);
                    if (menuu != null)
                    {
                        menuu.Aktif = false;
                        db.SaveChanges();
                        return RedirectToAction("AdminSayfa2");
                    }
                }

                var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
                var yorumm = (from Yorumlar in _umutpizzam.Yorumlars select Yorumlar).ToList();
                kmenum.Yorumlars = yorumm;
                kmenum.Menulers = menum;
                return View(kmenum);
            }
            catch (Exception ex)
            {
                ViewBag.HataMesaji = "Hatalı giriş! Lütfen bilgileri kontrol ediniz." + ex;

                Class1 kmenum = new Class1();
                var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
                var yorumm = (from Yorumlar in _umutpizzam.Yorumlars select Yorumlar).ToList();
                kmenum.Yorumlars = yorumm;
                kmenum.Menulers = menum;

                return View(kmenum);
            }
        }
        public ActionResult AdminSayfa1()
        {
            if (Session["KullaniciAdi"] == null)
            {
                HttpCookie cookie = Request.Cookies["AdminLogin"];
                if (cookie == null)
                {
                    return RedirectToAction("Giris", "Admin");
                }
                else
                {
                    Session["KullaniciAdi"] = cookie.Value;
                }
            }

            Class1 kmenum = new Class1();
            var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
            var kateg = (from Kategoriler in _umutpizzam.Kategorilers select Kategoriler).ToList();
            kmenum.Menulers = menum;
            kmenum.Kategorilers = kateg;
            return View(kmenum);
        }
        [HttpPost]
        public ActionResult AdminSayfa1(FormCollection fc)
        {
            try
            {
                Menuler menuler = new Menuler();

                if (fc["MenuEkle"] == "Ekle")
                {
                    menuler.Ad = fc["MenuAd"];
                    menuler.Aciklama = fc["AciklaMenu"];
                    menuler.Fiyat = Convert.ToInt32(fc["Fiyat"]);
                    menuler.ResimUrl = fc["ResimL"];
                    var kat = fc["KategAdi"];
                    var katt = db.Kategorilers.FirstOrDefault(k => k.Ad == kat);
                    if (katt != null)
                    {
                        int KatID = katt.Id;
                        menuler.KategoriId = KatID;
                    }
                    menuler.AktifMi = true;
                    menuler.OlusturulmaTarihi = DateTime.Now;

                    db.Menulers.Add(menuler);
                    db.SaveChanges();
                    return RedirectToAction("AdminSayfa1");
                }
                if (fc["MenuSil"] == "Sil")
                {
                    var arananmenu = fc["menuID"];
                    var menuu = db.Menulers.FirstOrDefault(k => k.Id.ToString() == arananmenu);
                    if (menuu != null)
                    {
                        menuu.AktifMi = false;
                        db.SaveChanges();
                        return RedirectToAction("AdminSayfa1");
                    }
                }
                Class1 kmenum = new Class1();
                var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
                var kateg = (from Kategoriler in _umutpizzam.Kategorilers select Kategoriler).ToList();
                kmenum.Menulers = menum;
                kmenum.Kategorilers = kateg;
                return View(kmenum);
            }
            catch (Exception)
            {
                ViewBag.HataMesaji = "Hatalı giriş! Lütfen bilgileri kontrol ediniz.";


                Class1 kmenum = new Class1();
                var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
                var kateg = (from Kategoriler in _umutpizzam.Kategorilers select Kategoriler).ToList();
                kmenum.Kategorilers = kateg;
                kmenum.Menulers = menum;

                return View(kmenum);
            }
        }
       public ActionResult AdminSayfa()
        {
            if (Session["KullaniciAdi"] == null)
            {
                HttpCookie cookie = Request.Cookies["AdminLogin"];
                if (cookie == null)
                {
                    return RedirectToAction("Giris", "Admin");
                }
                else
                {
                    Session["KullaniciAdi"] = cookie.Value;
                }
            }

            var kategor = (from Kategoriler in _umutpizzam.Kategorilers select Kategoriler).ToList();
            return View(kategor);
        }
        [HttpPost]
        public ActionResult AdminSayfa(FormCollection fc)
        {
            try {
                Kategoriler kategoriler = new Kategoriler();

                if (fc["kateEkle"] == "Ekle")
                {
                    kategoriler.Ad = fc["KategoriAd"];
                    kategoriler.Aktif = true;
                    db.Kategorilers.Add(kategoriler);
                    db.SaveChanges();
                    return RedirectToAction("AdminSayfa");
                    }
                if (fc["KategoriSil"] == "Sil")
                {
                    var arananmenu = fc["KategoriSilId"];
                    var menuu = db.Kategorilers.FirstOrDefault(k => k.Id.ToString() == arananmenu);
                    if (menuu != null)
                    {
                        menuu.Aktif = false;
                        db.SaveChanges();
                        return RedirectToAction("AdminSayfa");
                    }
                }
                var kategor = (from Kategoriler in _umutpizzam.Kategorilers select Kategoriler).ToList();

                return View(kategor);
            }
            catch (Exception)
            {
                ViewBag.HataMesaji = "Hatalı giriş! Lütfen bilgileri kontrol ediniz.";
                var kategor = (from Kategoriler in _umutpizzam.Kategorilers select Kategoriler).ToList();

                return View(kategor);
            }
        }
        public ActionResult Anasayfa()
        {
            var menuler = (from a in _umutpizzam.Menulers select a).ToList();
            return View(menuler);
        }
        public ActionResult Menu()
        {
            Class1 kmenum = new Class1();
            var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
            kmenum.Menulers = menum;
            return View(kmenum);
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            Class1 kmenum = new Class1();
            var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
            kmenum.Menulers = menum;
            return View(kmenum);
        }
        [HttpPost]
        public ActionResult Iletisim(FormCollection fc)
        {
            Yorumlar yor = new Yorumlar();

            Class1 kmenum = new Class1();         

            if (fc["YorumYap"] == "Gonder")
            {
                var secilenMenuAdi = fc["menu"];
                var menu = db.Menulers.FirstOrDefault(m => m.Ad == secilenMenuAdi);
                if (menu != null)
                {
                    yor.MenuId = menu.Id;
                    yor.KullaniciAdi = fc["kAd"];
                    yor.Email = fc["kEmail"];
                    yor.Icerik = fc["kYorum"];
                    yor.YorumTarihi = DateTime.Now;
                    yor.Aktif = true;

                    db.Yorumlars.Add(yor);
                    db.SaveChanges();
                    return RedirectToAction("Iletisim");
                }
            }
            var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
            var yorumm = (from Yorumlar in _umutpizzam.Yorumlars select Yorumlar).ToList();
            kmenum.Yorumlars = yorumm;
            kmenum.Menulers = menum;
            return View(kmenum);
        }
        public ActionResult Yorumlar()
        {
            Class1 kmenum = new Class1();
            var menum = (from Menuler in _umutpizzam.Menulers select Menuler).ToList();
            var yorumm = (from Yorumlar in _umutpizzam.Yorumlars select Yorumlar).ToList();
            kmenum.Yorumlars = yorumm;
            kmenum.Menulers = menum;
            return View(kmenum);
        }
    }
}