using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class mapKhachHang
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public List<Khach_Hang> khach_Hang()
        {
            var ds = db.Khach_Hang.ToList();
            return ds;
        }
    }
}