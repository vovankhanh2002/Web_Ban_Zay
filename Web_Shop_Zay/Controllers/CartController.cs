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
            var ds = db.San_Pham.SingleOrDefault(m => m.MaSP == id);
            if(ds != null)
            {
                GetCart().AddItem(ds);
                Carts carts = Session["Carts"] as Carts;
                Session["soluong"] = carts.Items.Count();
            }
            return Redirect("~/Shop/Index");
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
            int sl = int.Parse(form["slo"]);
            carts.Update(id, sl);
            return RedirectToAction("Show");
        }

        public ActionResult Delete(FormCollection form)
        {
            Carts carts = (Carts)Session["Carts"];
            int id = int.Parse(form["id"]);
            carts.Delete(id);
            return RedirectToAction("Show");
        }
    }
}