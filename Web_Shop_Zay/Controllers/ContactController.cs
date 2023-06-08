using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Shop_Zay.Models;

namespace Web_Shop_Zay.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Lien_He lien_He)
        {
            if(lien_He != null)
            {
                db.Lien_He.Add(lien_He);
                db.SaveChanges();
            }
            return View();
        }
    }
}