﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web_Shop_Zay.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Web_Ban_ZayEntities3 : DbContext
    {
        public Web_Ban_ZayEntities3()
            : base("name=Web_Ban_ZayEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Chi_Tiet_HD> Chi_Tiet_HD { get; set; }
        public virtual DbSet<Hoa_Don> Hoa_Don { get; set; }
        public virtual DbSet<Khach_Hang> Khach_Hang { get; set; }
        public virtual DbSet<Loai_San_Pham> Loai_San_Pham { get; set; }
        public virtual DbSet<Phan_Loai_SP> Phan_Loai_SP { get; set; }
        public virtual DbSet<San_Pham> San_Pham { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
