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
        [HttpPost]
        public ActionResult Index(int? page,string select, string searchString)
        {
                Session["searchString"] = searchString;
                if (page == null) page = 1;
                var SanPham = from m in db.San_Pham
                              select m;
                int pageSize = 20;
                int pageNumber = page ?? 1;
                if (!String.IsNullOrEmpty(searchString) && select == "TenSP")
                {
                    SanPham = SanPham.Where(s =>s.TenSP.ToLower().Contains(searchString)).OrderBy(m => m.MaSP);
                    Session["seleted"] = "selected";
                    return View(SanPham.ToPagedList(pageNumber, pageSize));
                }
                else if (!String.IsNullOrEmpty(searchString) && select == "MaPL")
                {
                    SanPham = SanPham.Where(s => s.MaPL.ToString().Contains(searchString)).OrderBy(m => m.MaSP);
                    Session["seleted1"] = "selected";
                    return View(SanPham.ToPagedList(pageNumber, pageSize));
                }
                else if (!String.IsNullOrEmpty(searchString) && select == "MaSP")
                {
                    SanPham = SanPham.Where(s => s.MaSP.ToString().Contains(searchString)).OrderBy(m => m.MaSP);
                    Session["seleted2"] = "selected";
                    return View(SanPham.ToPagedList(pageNumber, pageSize));
                }
            return View();
        }
      
      
        public ActionResult AddSanPham()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddSanPham(San_Pham san_Pham,string mapl)
        {
            var ds = db.Phan_Loai_SP.ToList();
            if (san_Pham.MaPL != null || san_Pham.TenSP != null || san_Pham.TrangThai != null)
            {
                foreach (var i in ds)
                {
                    if (i.TenPL == mapl || i.MaPL == san_Pham.MaPL)
                    {
                        var item = new San_Pham
                        {
                            TenSP = san_Pham.TenSP,
                            MaPL = i.MaPL,
                            MoTa = san_Pham.MoTa,
                            SoLuong = san_Pham.SoLuong,
                            Gia = san_Pham.Gia,
                            Hinh = san_Pham.Hinh,
                            TrangThai = san_Pham.TrangThai,
                            Size = san_Pham.Size,
                            GiamGia = san_Pham.GiamGia,
                        };
                        db.San_Pham.Add(item);
                        db.SaveChanges();
                    }
                }
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
                ds.GiamGia = san_Pham.GiamGia;
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
            
            return RedirectToAction("Index");
        }
    }
}