using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Shop_Zay.Models;
namespace Web_Shop_Zay.Models
{
    public class mapPhanLoaiSP
    {
        Web_Ban_ZayEntities3 db = new Web_Ban_ZayEntities3();
        public List<Phan_Loai_SP> phanLoaiSp()
        {
            var ds = db.Phan_Loai_SP.ToList();
            return  ds;
        }
        public List<Phan_Loai_SP> phanLoaiSpID(int idloai)
        {
            var ds = db.Phan_Loai_SP.Where(m => m.MaLoai == idloai).ToList(); ;
            return ds;
        }
        
    }
}