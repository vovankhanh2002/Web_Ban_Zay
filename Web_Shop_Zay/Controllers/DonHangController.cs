using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Controllers
{
    public class DonHangController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: ChiTietHd

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HoaDon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HoaDon(Hoa_Don hoa_Don)
        {
            db.Hoa_Don.Add(hoa_Don);
            db.SaveChanges();
            Session["mahd"] = hoa_Don.MaHD;
            return RedirectToAction("ChiTietHD");
        }
 
        public ActionResult ChiTietHD()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChiTietHD(Chi_Tiet_HD chi_Tiet_HD)
        {
            Carts carts = (Carts)Session["Carts"];
            Session["tt"] = "tt";
            db.Chi_Tiet_HD.Add(chi_Tiet_HD);
            db.SaveChanges();
            return View();
        }
    }
}