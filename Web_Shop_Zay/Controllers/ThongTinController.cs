using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Controllers
{
    public class ThongTinController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        private object textBox1;

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
            else
            {
                Session["k"] = "tt";
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
            if(Session["idkhachhang"] != null && khach_Hang.MatKhau == khach_Hang.ReMatKhau)
            {
                var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == khach_Hang.IDKhachHang);
                ds.HoTenKH = khach_Hang.HoTenKH;
                ds.DiaChi = khach_Hang.DiaChi;
                ds.GioiTinh = khach_Hang.GioiTinh;
                ds.DienThoai = khach_Hang.DienThoai;
                ds.Anh = khach_Hang.Anh;
             
                ds.MatKhau = khach_Hang.MatKhau;
                ds.ReMatKhau = khach_Hang.ReMatKhau;
                    
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                TempData["check_mk"] = "Mật khẩu không trung khớp";
                return Redirect("~/ThongTin/EditThongTin?id=" + khach_Hang.IDKhachHang);
            }
        }
        public ActionResult Uploadanh(int id)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult Uploadanh(Khach_Hang khach_Hang)
        {
            if (Session["idkhachhang"] != null)
            {
                var ds = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == khach_Hang.IDKhachHang);
                ds.Anh = khach_Hang.Anh;
                db.SaveChanges();
                return RedirectToAction("EditThongTin");
            }
            return RedirectToAction("EditThongTin");
        }
        public ActionResult DangXuat()
        {
            Session.Remove("idkhachhang");
            Session.Remove("Ten");
            Session.Remove("DangNhap");
            return Redirect("~/Home/Index");
        }
        public ActionResult QuenPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QuenPass(string email)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.Email == email);
            if (ds != null)
            {
                try
                {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/quenpass.html"));

                    content = content.Replace("{{Name}}", ds.HoTenKH);
                    content = content.Replace("{{Email}}", ds.Email);
                    content = content.Replace("{{Address}}", ds.DiaChi);
                    content = content.Replace("{{Link}}", ds.Email);
                    var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                    // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
                    //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
                    //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
                    //của ứng dụng kém an toàn phải ở chế độ bật
                    //  Đồng thời tài khoản Gmail cũng cần bật IMAP
                    //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop
                    TempData["mail"] = "Thông tin đã gửi vào mail thành công";
                    new MailHelper().SendMail(ds.Email, "Shop Zay", content);
                    new MailHelper().SendMail(toEmail, "Shop Zay", content);
                    //Xóa hết giỏ hàng
                }
                catch (Exception ex)
                {
                    //ghi log
                    return RedirectToAction("DangNhap");
                }

            }
            return RedirectToAction("DangNhap");
        }
       public ActionResult newpass(string email)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.Email == email);
            return View(ds);

        }
        [HttpPost]
        public ActionResult newpass(Khach_Hang khach)
        {
            var ds = db.Khach_Hang.SingleOrDefault(m => m.Email == khach.Email);
            ds.MatKhau = khach.MatKhau;
            ds.ReMatKhau = khach.ReMatKhau;
            db.SaveChanges();
            return RedirectToAction("DangNhap");
        }
    }
}