using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Areas.Admin.Controllers
{
    public class TaiKhoangController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: Admin/DangNhap
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TenUser, string MatKhauUser)
        {
            var ds = db.Users.Where(m => m.TenUser == TenUser && m.MatKhauUser == MatKhauUser).ToList();   
            if(ds.Count != 0)
            {
                Session["name"] = TenUser;
                Session["data"] = "Đã dang nhap thành công!";
                return Redirect("~/Admin/PhanLoaiSP/Index");
               
            }
            else
            {
                Session["Null"] = "Tai khoan mat khau khong chinh xac";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Signout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}