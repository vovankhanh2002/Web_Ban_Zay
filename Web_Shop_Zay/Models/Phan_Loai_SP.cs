//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Phan_Loai_SP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phan_Loai_SP()
        {
            this.San_Pham = new HashSet<San_Pham>();
        }
    
        public int MaPL { get; set; }
        public Nullable<int> MaLoai { get; set; }
        public string TenPL { get; set; }
    
        public virtual Loai_San_Pham Loai_San_Pham { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<San_Pham> San_Pham { get; set; }
    }
}