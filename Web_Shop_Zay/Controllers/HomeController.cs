using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;

namespace Web_Shop_Zay.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public ActionResult Index()
        {
            mapSanPham sp = new mapSanPham();
            Session["SpRan"] = "Sản phẩm nổi bật";
            var ds = (from result in db.San_Pham
                      orderby Guid.NewGuid()
                      select result).Take(3);
            return View(ds);
        }
        
    }
}