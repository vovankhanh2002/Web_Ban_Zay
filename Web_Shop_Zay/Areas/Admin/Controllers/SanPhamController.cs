using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using PagedList;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (Session["data"] != null)
            {
                if (page == null) page = 1;
                var SanPham = (from u in db.San_Pham select u).OrderBy(u => u.MaSP);
                int pageSize = 20;
                int pageNumber = page ?? 1;
                return View(SanPham.ToPagedList(pageNumber, pageSize));
            }
            return Redirect("~/Admin/TaiKhoang/Login");
        }
        public ActionResult AddSanPham()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddSanPham(San_Pham san_Pham)
        {
            if (san_Pham.MaPL != null || san_Pham.TenSP != null || san_Pham.TrangThai != null)
            {
                db.San_Pham.Add(san_Pham);
                db.SaveChanges();
                TempData["ThongBao"] = "Đã thêm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
            }
            return RedirectToAction("AddSanPham");
        }

        public ActionResult EditSanPham(int id)
        {
            var ds = db.San_Pham.SingleOrDefault(m => m.MaSP == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditSanPham(San_Pham san_Pham)
        {
            var ds = db.San_Pham.SingleOrDefault(m => m.MaSP == san_Pham.MaSP);
            if (san_Pham.MaPL != null || san_Pham.TenSP != null || san_Pham.TrangThai != null)
            {
                ds.MaPL = san_Pham.MaPL;
                ds.TenSP = san_Pham.TenSP;
                ds.MoTa = san_Pham.MoTa;
                ds.SoLuong = san_Pham.SoLuong;
                ds.Gia = san_Pham.Gia;
                ds.Hinh = san_Pham.Hinh;
                ds.TrangThai = san_Pham.TrangThai;
                db.SaveChanges();
                TempData["ThongBao"] = "Đã sửa thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
                return RedirectToAction("EditSanPham");
            }

        }
        public ActionResult DeleteSanPham(int id)
        {
            var ds = db.San_Pham.SingleOrDefault(m => m.MaSP == id);
            db.San_Pham.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}