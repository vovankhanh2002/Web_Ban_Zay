using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using PagedList;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class ChiTietHDController : Controller
    {
        // GET: Admin/ChiTietHD
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var ChiTiet = (from u in db.Chi_Tiet_HD select u).OrderBy(u => u.MaCT);
            int pageSize = 10;
            int pageNumber = page ?? 1;
            return View(ChiTiet.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult EditChiTietHD(int id)
        {
            var ds = db.Chi_Tiet_HD.SingleOrDefault(m => m.MaCT == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditChiTietHD(Chi_Tiet_HD chi_Tiet_HD)
        {
            var ds = db.Chi_Tiet_HD.SingleOrDefault(m => m.MaCT == chi_Tiet_HD.MaCT);
                db.SaveChanges();
                TempData["ThongBao"] = "Đã sửa thành công!";
                return RedirectToAction("Index");
            }
        public ActionResult DeleteChiTietHD(int id)
        {
            var ds = db.Chi_Tiet_HD.SingleOrDefault(m => m.MaCT == id);
            db.Chi_Tiet_HD.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}