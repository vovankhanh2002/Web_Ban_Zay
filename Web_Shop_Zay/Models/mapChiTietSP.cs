using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class mapChiTietSP
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public ChiTietSp ChiTiet(int id)
        {
            var ds = db.ChiTietSps.SingleOrDefault(e => e.MaSP == id);
            return ds;
        }
    }
}