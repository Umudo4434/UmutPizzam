using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UmutPizzam.Models;

namespace UmutPizzam.Controllers
{
    public class AdminController : Controller
    {
        private UmutPizzamEntities1 db = new UmutPizzamEntities1();

        [HttpGet]
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(string KullaniciAdi, string Sifre)
        {
            var admin = db.Admins.FirstOrDefault(a => a.KullaniciAdi == KullaniciAdi && a.Sifre == Sifre);

            if (admin != null)
            {
                Session["KullaniciAdi"] = admin.KullaniciAdi;

                HttpCookie cookie = new HttpCookie("AdminLogin");
                cookie.Value = admin.KullaniciAdi;
                cookie.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(cookie);

                return RedirectToAction("AdminSayfa", "Home");
            }
            else
            {
                ViewBag.Hata = "Hatalı Giriş";
                return View();
            }
        }
        public ActionResult Cikis()
        {
            Session.Clear();

            if (Request.Cookies["AdminLogin"] != null)
            {
                var cookie = new HttpCookie("AdminLogin");
                cookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Giris", "Admin");
        }
    }
}