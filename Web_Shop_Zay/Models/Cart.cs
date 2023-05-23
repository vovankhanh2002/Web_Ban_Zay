using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class Cart
    {
        public San_Pham san_pham { get; set; }
        public int So_Luong { get; set; }
        public int Size { get; set; }
    }
    public class Carts
    {
        List<Cart> items = new List<Cart>();
        public IEnumerable<Cart> Items
        {
            get { return items; }
        }
        public void AddItem(San_Pham sp , int soluong =1 ,int size=35 )
        {
            var ds = items.FirstOrDefault(m => m.san_pham.MaSP == sp.MaSP);
            if(ds == null)
            {
                items.Add(new Cart
                {
                    san_pham = sp,
                    So_Luong = soluong,
                    Size = size
                }); 
            }
            else
            {
                ds.So_Luong++;
            }
        }
        public void Update(int id, int slo, int size )
        {
            var ds = items.SingleOrDefault(s => s.san_pham.MaSP == id);
            if (ds != null)
            {
                ds.So_Luong = slo;
                ds.Size = size;
            }
        }
        public void Delete(int id)
        {
            var ds = items.SingleOrDefault(m => m.san_pham.MaSP == id);
            items.Remove(ds);
        }
    }
}