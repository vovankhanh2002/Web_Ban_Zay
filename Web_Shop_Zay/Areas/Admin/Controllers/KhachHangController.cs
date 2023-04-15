using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using PagedList;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (Session["data"] != null)
            {
                if (page == null) page = 1;
                var khachhang = (from u in db.Khach_Hang select u).OrderBy(u => u.IDKhachHang);
                int pageSize = 10;
                int pageNumber = page ?? 1;
                return View(khachhang.ToPagedList(pageNumber, pageSize));
            }
            return Redirect("~/Admin/TaiKhoang/Login");
        }
        public ActionResult AddKhachhang()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddKhachhang(Khach_Hang khach_Hang)
        {
            if (khach_Hang.HoTenKH != null && khach_Hang.DiaChi != null && khach_Hang.DienThoai != null && khach_Hang.MatKhau == khach_Hang.ReMatKhau)
            {
                db.Khach_Hang.Add(khach_Hang);
                db.SaveChanges();
                TempData["ThongBao"] = "Đã thêm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin hoặc mật khẩu không trùng khớp!";
            }
            return RedirectToAction("AddKhachhang");
        }

        public ActionResult EditKhachHang(int id)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditKhachHang(Khach_Hang khach_Hang)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == khach_Hang.IDKhachHang);
            if (khach_Hang.HoTenKH != null && khach_Hang.DiaChi != null && khach_Hang.DienThoai != null && khach_Hang.MatKhau == khach_Hang.ReMatKhau)
            {
                ds.HoTenKH = khach_Hang.HoTenKH;
                ds.DiaChi = khach_Hang.DiaChi;
                ds.Email = khach_Hang.Email;
                ds.DienThoai = khach_Hang.DienThoai;
                ds.GioiTinh = khach_Hang.GioiTinh;
                ds.MatKhau = khach_Hang.MatKhau;
                ds.ReMatKhau = khach_Hang.ReMatKhau;
                db.SaveChanges();
                TempData["ThongBao"] = "Đã sửa thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin hoặc mật khẩu không trùng khớp!";
                return RedirectToAction("EditKhachHang");
            }

        }
        public ActionResult DeleteKhachHang(int id)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == id);
            db.Khach_Hang.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}