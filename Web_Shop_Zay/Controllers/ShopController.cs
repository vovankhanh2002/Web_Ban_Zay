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
            int pageSize = 10;
            int pageNumber = page ?? 1;
            return View(SanPham.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SanPhamID(int id, int? page)
        {
            mapSanPham sp = new mapSanPham();
            var ds = sp.SanPhamID(id);
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNumber = page ?? 1;
            return View(ds.ToPagedList(pageNumber, pageSize));
        }

    }
}