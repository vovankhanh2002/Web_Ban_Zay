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
            var ds = db.San_Pham.Find(id);
            if(ds != null)
            {
                db.San_Pham.Attach(ds);
                ds.LuotXem = ds.LuotXem + 1;
                db.Entry(ds).Property(x => x.LuotXem).IsModified = true;
                db.SaveChanges();
            }
            return View(ds);
        }
        
    }
}