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
        public List<San_Pham> SanPhamGiamDan()
        {
            var ds = db.San_Pham.ToList().OrderByDescending(m => m.Gia).ToList();
            return ds;
        }
        public List<San_Pham> SanPhamTangDan()
        {
            var ds = db.San_Pham.ToList().OrderBy(m => m.Gia).ToList();
            return ds;
        }
        public List<San_Pham> SanPhamAZ()
        {
            var ds = db.San_Pham.ToList().OrderBy(m => m.TenSP).ToList();
            return ds;
        }
        public List<San_Pham> SanPhamZA()
        {
            var ds = db.San_Pham.ToList().OrderByDescending(m => m.TenSP).ToList();
            return ds;
        }
        public List<San_Pham> TimKiem(string search)
        {
            var ds = db.San_Pham.Where(m => m.TenSP.Contains(search)).ToList();
            return ds;
        }
    }
}