using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class mapSanPham
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public List<San_Pham> SanPham()
        {
            var ds = db.San_Pham.ToList();
            return ds;
        }
        public List<San_Pham> SanPhamID(int? id)
        {
            var ds = db.San_Pham.Where(m => m.MaPL == id).ToList();
            return ds;
        }
        
    }
}