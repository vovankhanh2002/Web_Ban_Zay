using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Controllers
{
    public class ChiTietController : Controller
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        // GET: ChiTiet
        public ActionResult Index(int id)
        {
            var ds = db.San_Pham.SingleOrDefault(e => e.MaSP == id);
            return View(ds);
        }
        [HttpPost]
        public ActionResult Index(San_Pham sp)
        {
            var ds = db.San_Pham.SingleOrDefault(e => e.MaSP == sp.MaSP);
            return View(ds);
        }
    }
}