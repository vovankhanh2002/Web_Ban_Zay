using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;

namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class ChiTietSPController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var ChiTietSP = (from u in db.ChiTietSps select u).OrderBy(u => u.MaChiTietSP);
            int pageSize = 10;
            int pageNumber = page ?? 1;
            return View(ChiTietSP.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult AddChiTietSP(int masp)
        {
            ViewBag.masp = masp;
            return View();
        }
        [HttpPost]
        public ActionResult AddChiTietSP(ChiTietSp ct)
        {
            var ds = db.ChiTietSps.SingleOrDefault(m => m.MaSP == ct.MaSP);
            if (ds == null | ct.MaSP != null)
            {
                db.ChiTietSps.Add(ct);
                db.SaveChanges();

            }
            else
            {
                Session["Erorr"] = "Trùng mã chi tiết sản phẩm vui lòng nhập mã khác!";
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
                return RedirectToAction("AddPhanLoaiSP");
            }
            Session["ThongBao"] = "Đã thêm thành công!";
            return RedirectToAction("index");
        }

        public ActionResult EditChiTietSP(int id)
        {
            var ds = db.ChiTietSps.SingleOrDefault(m => m.MaChiTietSP == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditChiTietSP(ChiTietSp chi_Tiet_SP)
        {
            var ds = db.ChiTietSps.SingleOrDefault(m => m.MaChiTietSP == chi_Tiet_SP.MaChiTietSP);
            ds.MaSP = chi_Tiet_SP.MaSP;
            ds.Hinh1 = chi_Tiet_SP.Hinh1;
            ds.Hinh2 = chi_Tiet_SP.Hinh2;
            ds.Hinh3 = chi_Tiet_SP.Hinh3;
            ds.Hinh4 = chi_Tiet_SP.Hinh4;
            db.SaveChanges();
            TempData["ThongBao"] = "Đã sửa thành công!";
            return RedirectToAction("Index");
        }
        public ActionResult DeleteChiTietsp(int id)
        {
            var ds = db.ChiTietSps.SingleOrDefault(m => m.MaChiTietSP == id);
            db.ChiTietSps.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}