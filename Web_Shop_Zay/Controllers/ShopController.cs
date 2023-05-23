using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using PagedList;
namespace Web_Shop_Zay.Controllers
{
    public class ShopController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Shop
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var SanPham = (from u in db.San_Pham select u).OrderBy(u => u.MaSP);
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(SanPham.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamID(int id, int? page)
        {
            mapSanPham sp = new mapSanPham();
            var ds = sp.SanPhamID(id);
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamGiam(int? page)
        {
            mapSanPham sp = new mapSanPham();
            Session["SpGiam"] = "Giá giảm dần";
            var ds = sp.SanPhamGiamDan();
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamTang(int? page)
        {
            mapSanPham sp = new mapSanPham();
            Session["SpTang"] = "Giá tăng dần";
            var ds = sp.SanPhamTangDan();
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamRan(int? page)
        {
            mapSanPham sp = new mapSanPham();
            Session["SpRan"] = "Sản phẩm nổi bật";
            var ds = (from result in db.San_Pham
              orderby Guid.NewGuid()
              select result).Take(12);
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamAZ(int? page)
        {
            mapSanPham sp = new mapSanPham();
            Session["SpAZ"] = "Tên:A-Z";
            var ds = sp.SanPhamAZ();
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamZA(int? page)
        {
            mapSanPham sp = new mapSanPham();
            Session["SpZA"] = "Tên:Z-A";
            var ds = sp.SanPhamZA();
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
        [HttpPost]
        public ActionResult TimKiem(int? page, string search)
        {
            mapSanPham sp = new mapSanPham();
            var ds = sp.TimKiem(search);
            if (page == null) page = 1;
            int pageSize = 20;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }
    }
}