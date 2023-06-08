using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;

namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class LienHeController : Controller
    {
        // GET: Admin/Contact
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public ActionResult Index(int? page)
        {
            if (Session["data"] != null)
            {
                if (page == null) page = 1;
                var dslienhe = (from u in db.Lien_He select u).OrderBy(u => u.Id);
                int pageSize = 10;
                int pageNumber = page ?? 1;
                return View(dslienhe.ToPagedList(pageNumber, pageSize));
            }
            return Redirect("~/Admin/TaiKhoang/Login");
           
        }
        public ActionResult DeleteLienHe(int id)
        {
            var ds = db.Lien_He.SingleOrDefault(m => m.Id == id);
            db.Lien_He.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}