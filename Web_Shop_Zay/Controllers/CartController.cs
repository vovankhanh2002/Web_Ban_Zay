using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public Carts GetCart()
        {
            Carts carts = Session["Carts"] as Carts;
            
            if(carts == null)
            {
                carts = new Carts();
                Session["Carts"] = carts;
            }
            return carts;
        }
        public ActionResult AddSp(int id)
        {
            if (Session["DangNhap"] != null)
            {
                var ds = db.San_Pham.SingleOrDefault(m => m.MaSP == id);
                if (ds != null)
                {
                    GetCart().AddItem(ds);
                    Carts carts = Session["Carts"] as Carts;
                    Session["soluong"] = carts.Items.Count();
                }
                return Redirect("~/Shop/Index");
            }
            return Redirect("~/ThongTin/DangNhap");
        }
        public ActionResult Show()
        {
            Carts carts = (Carts)Session["Carts"];
            return View(carts);
        }
        public ActionResult Update(FormCollection form)
        {
            Carts carts = Session["Carts"] as Carts;

            int id = int.Parse(form["id"]);
            if (int.Parse(form["slo"]) > 0)
            {
                int sl = int.Parse(form["slo"]);
                carts.Update(id, sl, int.Parse(form["size"]));
                //int sl = int.Parse(form["slo"]);
            }
            else
            {
                Session["alert"] = "min";
            }
            return RedirectToAction("Show");
        }

        public ActionResult Delete(FormCollection form)
        {
            Carts carts = (Carts)Session["Carts"];
            int id = int.Parse(form["id"]);
            carts.Delete(id);
            Session["soluong"] = carts.Items.Count();
            return RedirectToAction("Show");
        }
        public ActionResult Post(FormCollection form)
        {

            int id = int.Parse(form["id"]);
            int sum = int.Parse(form["sum"]);
            int sl = int.Parse(form["slo"]);
            Session["idsanpham"] = id;
            Session["sum"] = sum;
            Session["slo"] = sl;
            Session["size"] = int.Parse(form["size"]);
            return Redirect("~/DonHang/Index");
        }
    }
}