using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class mapHoaDon
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public List<Hoa_Don> HoaDon()
        {
            var ds = db.Hoa_Don.ToList();
            return ds;
        }
    }
}