using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/User
        public ActionResult Index(int? page)
        {
            if (Session["data"] != null)
            {
                if (page == null) page = 1;
                var User = (from u in db.Users select u).OrderBy(u => u.TenUser);
                int pageSize = 10;
                int pageNumber = page ?? 1;
                return View(User.ToPagedList(pageNumber, pageSize));
            }
            return Redirect("~/Admin/TaiKhoang/Login");
    }

        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if(user.TenUser != null || user.MatKhauUser != null || user.MatKhauNhapLai != null)
            {
                db.Users.Add(user);
                db.SaveChanges();
                TempData["ThongBao"] = "Đã thêm thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
            }
            return RedirectToAction("AddUser");
        }

        public ActionResult EditUser(int id)
        {
            var ds = db.Users.SingleOrDefault(m => m.IDUser == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult EditUser(User user)
        {
            var ds = db.Users.SingleOrDefault(m => m.IDUser == user.IDUser);
            if (user.TenUser != null || user.MatKhauUser != null || user.MatKhauNhapLai != null) {
                ds.TenUser = user.TenUser;
                ds.MatKhauUser = user.MatKhauUser;
                ds.MatKhauNhapLai = user.MatKhauNhapLai;
                db.SaveChanges();
                TempData["ThongBao"] = "Đã sửa thành công!";
                return RedirectToAction("Index");
            }
            else
            {
                Session["Null"] = "Mời bạn nhập đầy đủ thông tin!";
                return RedirectToAction("EditUser");
            }
            
        }
        public ActionResult DeleteUser(int id)
        {
            var ds = db.Users.SingleOrDefault(m => m.IDUser == id);
            db.Users.Remove(ds);
            db.SaveChanges();
            TempData["ThongBao"] = "Đã xoá thành công!";
            return RedirectToAction("Index");
        }
    }
}