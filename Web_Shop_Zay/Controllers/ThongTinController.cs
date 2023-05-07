using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Controllers
{
    public class ThongTinController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: ThongTin
        public ActionResult Index()
        {
            if (Session["DangNhap"] != null)
            {
                return Redirect("~/Home/Index");
            }
            return RedirectToAction("DangNhap");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(Khach_Hang khach_Hang)
        {
            var ds = db.Khach_Hang.Where(m => m.Email == khach_Hang.Email);
            if(khach_Hang.HoTenKH == null || khach_Hang.Email == null || khach_Hang.DienThoai == null || ds == null || khach_Hang.MatKhau != khach_Hang.ReMatKhau)
            {
                return RedirectToAction("DangKy");
            }
            db.Khach_Hang.Add(khach_Hang);
            db.SaveChanges();
            ViewBag.id = khach_Hang.IDKhachHang;
            return RedirectToAction("DangNhap");
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(string email, string matkhau) 
        {
            var ds1 = db.Khach_Hang.SingleOrDefault(m => m.Email == email);
            var ds = db.Khach_Hang.Where(m => m.Email == email && m.MatKhau == matkhau).ToList();
            if(ds.Count > 0)
            {
                Session["idkhachhang"] = (int)ds1.IDKhachHang;
                Session["Ten"] = ds1.HoTenKH;
                Session["DangNhap"] = ds;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ThongBao"] = "Tài khoản mật khẩu không tồn tại";
                return RedirectToAction("DangNhap");
            }

        }
        public ActionResult EditThongTin(int id)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditThongTin(Khach_Hang khach_Hang)
        {
            if(Session["idkhachhang"] != null)
            {
                var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == khach_Hang.IDKhachHang);
                ds.HoTenKH = khach_Hang.HoTenKH;
                ds.DiaChi = khach_Hang.DiaChi;
                ds.GioiTinh = khach_Hang.GioiTinh;
                ds.DienThoai = khach_Hang.DienThoai;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("EditThongTin");
        }
        public ActionResult DangXuat()
        {
            Session.RemoveAll();
            return Redirect("~/Home/Index");
        }
    }
}