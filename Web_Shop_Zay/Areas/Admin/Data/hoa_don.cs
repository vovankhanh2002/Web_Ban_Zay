using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Shop_Zay.Models;

namespace Web_Shop_Zay.Areas.Admin.Data
{
    public class hoa_don
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();

        public int Chane(int id)
        {
            var hoadon = db.Hoa_Don.Find(id);
            if (hoadon.Duyet == 0)
            {
                hoadon.Duyet = 1;
                db.SaveChanges();
                return 1;
            }
            else
            {
                hoadon.Duyet = 0;
                db.SaveChanges();
                return 0;
            }
        }
    }
}