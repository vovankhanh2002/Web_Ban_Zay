using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Web_Shop_Zay.Models
{
    public class mapLoaiSP
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public List<Loai_San_Pham> LoaiSP()
        {
            var ds = db.Loai_San_Pham.ToList();
            return ds;
        }

    }
}