using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Controllers
{
    public class DonHangController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: ChiTietHd

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HoaDon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HoaDon(Hoa_Don hoa_Don, int TongTien, int MaSP, int SoLuong, int Size)
        {
                db.Hoa_Don.Add(hoa_Don);
                db.SaveChanges();
            Session["mahd"] = hoa_Don.MaHD;
            Session["TongTien"] = TongTien;
            Chi_Tiet_HD hoa_Don1 = new Chi_Tiet_HD
            {
                MaHD = hoa_Don.MaHD,
                MaSP = MaSP,
                SoLuong = SoLuong,
                Size = Size,
                TongTien = TongTien,
            };
            var idkh = db.Khach_Hang.SingleOrDefault(m => m.IDKhachHang == (int)hoa_Don.IDKhachHang);
            db.Chi_Tiet_HD.Add(hoa_Don1);
            db.SaveChanges();
            try { 
            string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/neworder.html"));

            content = content.Replace("{{CustomerName}}",idkh.HoTenKH );
            content = content.Replace("{{Phone}}", idkh.DienThoai);
            content = content.Replace("{{Email}}", idkh.Email);
            content = content.Replace("{{Address}}", idkh.DiaChi);
            content = content.Replace("{{Total}}", Session["TongTien"].ToString());
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
            //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
            //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
            //của ứng dụng kém an toàn phải ở chế độ bật
            //  Đồng thời tài khoản Gmail cũng cần bật IMAP
            //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

            new MailHelper().SendMail(idkh.Email, "Shop Zay", content);
            new MailHelper().SendMail(toEmail, "Shop Zay", content);
            //Xóa hết giỏ hàng
            }
            catch (Exception ex)
            {
                //ghi log
                return RedirectToAction("ChiTietHD");
            }

            return RedirectToAction("Index");
        }

        public ActionResult ChiTietHD()
        {
            return View();
        }
        
    }
}