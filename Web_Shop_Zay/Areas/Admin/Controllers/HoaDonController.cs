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
        [HttpPost]
        public ActionResult Index(int? page, string select, string searchString)
        {
            Session["searchString"] = searchString;
           
            int pageSize = 20;
            int pageNumber = page ?? 1;
            if (page == null) page = 1;
            var ds = db.Hoa_Don.ToList();
            var sql = from table_1 in db.Khach_Hang
                      where table_1.HoTenKH.Contains(searchString)
                      select table_1;
            List<Hoa_Don> hoadon = new List<Hoa_Don>();
            if(select == "HoTenKH")
            {
                foreach (var item in ds)
                {
                    foreach (var d in sql)
                    {
                        if (item.IDKhachHang == d.IDKhachHang)
                        {
                            hoadon.Add(item);
                        }
                    }
                }
                return View(hoadon.ToPagedList(pageNumber, pageSize));
            }else if(select == "NgayMua")
            {
                ds = (List<Hoa_Don>)ds.Where(m => m.NgayMua.Value.Day.ToString().Contains(searchString));
            }
            return View();
        }
        [HttpPost]
        public ActionResult Them(Hoa_Don hoa_Don)
        {
            db.Hoa_Don.Add(hoa_Don);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Sua(int id)
        {
            var ds = db.Hoa_Don.SingleOrDefault(m => m.MaHD == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult Sua(Hoa_Don hoa_Don)
        {
            var ds = db.Hoa_Don.SingleOrDefault(m => m.MaHD == hoa_Don.MaHD);
            ds.IDKhachHang = hoa_Don.IDKhachHang;
            ds.NgayMua = hoa_Don.NgayMua;
            ds.Name = hoa_Don.Name;
            ds.Phone = hoa_Don.Phone;
            ds.Address = hoa_Don.Address;
            ds.Total = hoa_Don.Total;
            ds.Quantity = hoa_Don.Quantity;
            ds.Payment = hoa_Don.Payment;
            ds.Email = hoa_Don.Email;
            ds.Buy = hoa_Don.Buy;
            ds.Duyet = hoa_Don.Duyet;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHoaDon(int id)
        { 
            
            var jont = from i in db.Hoa_Don
                       join k in db.Chi_Tiet_HD
                       on i.MaHD equals k.MaHD
                       where k.MaHD == id
                       select k;
            foreach (var item in jont)
            {
                db.Chi_Tiet_HD.Remove(item);
            }
            var ds = db.Hoa_Don.SingleOrDefault(m => m.MaHD == id);
            db.Hoa_Don.Remove(ds);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult ChangeSatus(int id)
        {
            var rs = new Data.hoa_don().Chane(id);
            return Json(new
            {
                duyet = rs
            });
        }
    }
}