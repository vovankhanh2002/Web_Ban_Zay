using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using PagedList;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (Session["data"] != null)
            {
                if (page == null) page = 1;
                var HoaDon = (from u in db.Hoa_Don select u).OrderBy(u => u.MaHD);
                int pageSize = 10;
                int pageNumber = page ?? 1;
                return View(HoaDon.ToPagedList(pageNumber, pageSize));
            }
            return Redirect("~/Admin/TaiKhoang/Login");
        }
       
        public ActionResult DeleteHoaDon(int id)
        {
            var ds = db.Hoa_Don.SingleOrDefault(m => m.MaHD == id);
            db.Hoa_Don.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}