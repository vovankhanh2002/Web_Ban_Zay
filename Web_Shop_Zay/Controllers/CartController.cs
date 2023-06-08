using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
using Web_Shop_Zay.Models.Payment;

namespace Web_Shop_Zay.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public ActionResult Index()
        {
           
                return View();
           
        }
        public ActionResult Checkout()
        {
            if (Session["DangNhap"] != null)
            {
                Cart cart = (Cart)Session["cart"];
                if (cart != null)
                {
                    ViewBag.checkcart = cart;
                }
                return View();
            }
            return Redirect("~/ThongTin/DangNhap");
        }

        public ActionResult Partial_CheckOut()
        {

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(Hoa_Don hoa_Don)
        {
            
            var code = new { Success = false, code = -1,Url = "" }; 
            if (ModelState.IsValid)
            {
                Cart cart = (Cart)Session["cart"];
            if (cart != null)
            {
                    Hoa_Don hoa_ = new Hoa_Don();
                    if(Session["idkhachhang"] != null)
                    {
                        hoa_.IDKhachHang = (int)Session["idkhachhang"];
                    }
                    hoa_.Name = hoa_Don.Name;
                    hoa_.Phone = hoa_Don.Phone;
                    hoa_.Address = hoa_Don.Address;
                    hoa_.Email = hoa_Don.Email;
                    hoa_.NgayMua = DateTime.Now;
                    hoa_.Total = cart.items.Sum(x => (x.Price * x.Quantity));
                    hoa_.Payment = hoa_Don.Payment;
               
                    db.Hoa_Don.Add(hoa_);
                    db.SaveChanges();
                    
                    foreach(var i in cart.items)
                    {
                        Chi_Tiet_HD chi_Tiet_HD = new Chi_Tiet_HD
                        {
                                MaHD = hoa_.MaHD,
                                MaSP = i.Id,
                                SoLuong = i.Quantity,
                                TongTien =(int)cart.items.Sum(x => (x.Price * x.Quantity))  
                        };
                        db.Chi_Tiet_HD.Add(chi_Tiet_HD);
                        db.SaveChanges();
                    }
                    try
                    {
                        string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/send.html"));
                        var thanhtien = decimal.Zero;
                        var Tongtien = decimal.Zero;
                        var strSanpham = "";
                        foreach(var item in cart.items)
                        {
                            strSanpham += "<tr>";
                            strSanpham += "<td>"+item.Name+"</td>";
                            strSanpham += "<td>"+item.Quantity+"</td>";
                            strSanpham += "<td>"+item.Price+"</td>";
                            strSanpham += "<td>"+item.Size+"</td>";
                            strSanpham += "</tr>";
                            thanhtien += item.Quantity * item.Price;
                        }
                        Tongtien = thanhtien;
                        content = content.Replace("{{Tenkh}}", hoa_Don.Name);
                        content = content.Replace("{{Gia}}", thanhtien.ToString());
                        content = content.Replace("{{sanpham}}", strSanpham);

                        content = content.Replace("{{Tongtien}}", Tongtien.ToString());
                        content = content.Replace("{{Phuongthuc}}", hoa_Don.Payment.ToString());

                        content = content.Replace("{{Email}}", hoa_Don.Email);
                        content = content.Replace("{{Ngaymua}}", DateTime.Now.ToString());
                        content = content.Replace("{{Phone}}", hoa_Don.Phone);
                        content = content.Replace("{{Address}}", hoa_Don.Address);
                       
                        var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                        // Để Gmail cho phép SmtpClient kết nối đến server SMTP của nó với xác thực 
                        //là tài khoản gmail của bạn, bạn cần thiết lập tài khoản email của bạn như sau:
                        //Vào địa chỉ https://myaccount.google.com/security  Ở menu trái chọn mục Bảo mật, sau đó tại mục Quyền truy cập 
                        //của ứng dụng kém an toàn phải ở chế độ bật
                        //  Đồng thời tài khoản Gmail cũng cần bật IMAP
                        //Truy cập địa chỉ https://mail.google.com/mail/#settings/fwdandpop

                        new MailHelper().SendMail(hoa_Don.Email, "Shop Zay", content);
                        new MailHelper().SendMail(toEmail, "Shop Zay", content);

                        //Xóa hết giỏ hàng
                        cart.ClearCart();
                        code = new { Success = true, code = (int)hoa_Don.Payment, Url = "" };
                        var hd = db.Hoa_Don.SingleOrDefault(m => m.MaHD == hoa_.MaHD);
                        if (hoa_Don.Payment == 2)
                        {
                            string url = UrlPayment((int)hoa_Don.TypePayMentVN, hoa_.MaHD);
                            code = new { Success = true, code = (int)hoa_Don.Payment, Url = url };
                            
                            hd.Buy = 1;
                            hd.Duyet = 0;
                            db.SaveChanges();
                            return Redirect(url);
                        }
                        else if(hoa_Don.Payment == 1)
                        {
                            hd.Buy = 0;
                            hd.Duyet = 0;
                            db.SaveChanges();
                            return RedirectToAction("Success");
                        }
                    }
                    catch (Exception ex)
                    {
                        //ghi log
                        return RedirectToAction("ChiTietHD");
                    }
                    //code = new { Success = true, code = 1, Url="" };

                    //return RedirectToAction("Success");
                }
            }
            return Json(code); 
        }

        public ActionResult PartialCart_Bill()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart != null)
            {
                return View(cart.items);
            }
            return View();
        }
        public ActionResult PartialCart()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart != null)
            {
                return View(cart.items);
            }
            return View();
        }

        public ActionResult ShowCart()
        {
            Cart cart = (Cart)Session["cart"];
            if(cart != null)
            {
                return Json(new { Sucsess = true , Count = cart.items.Count()}, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Sucsess = false, Count = 0, JsonRequestBehavior.AllowGet });
        }


        [HttpPost]
        public ActionResult Addtocart(int id, int quantity,int size)
        {
            var code = new { Sucsess = false, msg = "", code = -1,count = 0};
            var check = db.San_Pham.FirstOrDefault(m => m.MaSP == id);
           
            
                if (check != null)
                {
                    Cart cart = (Cart)Session["cart"];
                    if (cart == null)
                    {
                        cart = new Cart();
                    }
                    Carts carts = new Carts
                    {
                        Id = check.MaSP,
                        Name = check.TenSP,
                        Category = (int)check.MaPL,
                        Quantity = quantity,
                        Size = size
                    };
                    carts.Img = check.Hinh;
                    carts.Price = (decimal)check.Gia;
                    if (check.GiamGia != null)
                    {
                        carts.Price = (decimal)(check.Gia / 2);
                    }
                    carts.TotalPrice = carts.Price * carts.Quantity;
                    Session["cart"] = cart;
                    cart.Addtocart(carts, quantity, size);
                    code = new { Sucsess = true, msg = "Thêm thành công", code = 1, count = cart.items.Count() };

                }
                return Json(code);
           


        }




        [HttpPost]
        public ActionResult DeleteCart(int id)
        {
            var code = new { Sucsess = false, msg = "", code = -1, count = 0 };
            Cart cart = (Cart)Session["cart"];
            if (cart != null)
            {
                var check = cart.items.SingleOrDefault(x => x.Id == id);
                if(check != null)
                {
                    cart.Deletecart(id);
                    code = new { Sucsess = true, msg = "", code = 1, count = cart.items.Count() };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult UpdateCart(int id, int quantity,int size)
        {
            Cart cart = (Cart)Session["cart"];
            if (cart != null)
            {
                cart.Updatequantity(id,quantity,size);
                return Json(new { Sucsess = true });
            }
            return Json(new { Sucsess = false });
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Sucsess = true });
            }
            return Json(new { Sucsess = false });
        }
        public ActionResult Success()
        {

            return View();
        }

        public string UrlPayment(int TypePaymentVN, int orderCode)
        {
            var urlPayment = "";
            var order = db.Hoa_Don.FirstOrDefault(x => x.MaHD == orderCode);
            DateTime dt = DateTime.Now; // Or whatever
            string s = dt.ToString("yyyyMMddHHmmss");
            //Get Config Info
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma định danh merchant kết nối (Terminal Id)
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Secret Key



            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();
            var Price = (decimal)order.Total * 100;
            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", Price.ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000
            if (TypePaymentVN == 1)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
            }
            else if (TypePaymentVN == 2)
            {
                vnpay.AddRequestData("vnp_BankCode", "VNBANK");
            }
            else if (TypePaymentVN == 3)
            {
                vnpay.AddRequestData("vnp_BankCode", "INTCARD");
            }

            vnpay.AddRequestData("vnp_CreateDate", s);
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán đơn hàng :" + order.MaHD.ToString());
            vnpay.AddRequestData("vnp_OrderType", "San_Pham"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", order.MaHD.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

            //Add Params of 2.1.0 Version
            //Billing

            urlPayment = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //log.InfoFormat("VNPAY URL: {0}", paymentUrl);
            return urlPayment;
         }



    }

}
