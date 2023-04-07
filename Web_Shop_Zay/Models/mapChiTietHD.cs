using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class mapChiTietHD
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public List<Chi_Tiet_HD> ChiTietHD()
        {
            var ds = db.Chi_Tiet_HD.ToList();
            return ds;
        }
    }
}