using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Shop_Zay.Models
{
    public class Cart
    {
        public List<Carts> items { get; set; }
       public Cart()
        {
            this.items = new List<Carts>();
        }
        public void Addtocart(Carts carts , int Quantity,int Size)
        {
            var check = items.SingleOrDefault(m =>m.Id == carts.Id);
            if(check != null)
            {
                check.Quantity += Quantity;
                check.TotalPrice = check.Price * check.Quantity;
                check.Size = Size;
            }
            else
            {
                items.Add(carts);
            }
        }
        public void Deletecart(int id)
        {
            var check = items.SingleOrDefault(m => m.Id == id);
            if(check != null)
            {
                items.Remove(check);
            }
     
        }
        public void Updatequantity(int id, int quantity, int size)
        {
            var check = items.SingleOrDefault(m => m.Id == id);
            if (check != null)
            {
                check.Quantity = quantity;
                check.TotalPrice = check.Price * check.Quantity;
                check.Size = size;
            }
        }
        public decimal GetPrice()
        {
            return items.Sum(m => m.Price);
        }
        public decimal GetQuantity()
        {
            return items.Sum(m => m.Quantity);
        }
        public void ClearCart()
        {
            items.Clear();
        }
    }
    public class Carts
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string Img { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Size { get; set; }
    }
}