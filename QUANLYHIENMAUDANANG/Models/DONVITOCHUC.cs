//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QUANLYHIENMAUDANANG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DONVITOCHUC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONVITOCHUC()
        {
            this.DOTHIENMAU = new HashSet<DOTHIENMAU>();
        }
    
        public string MaDVTC { get; set; }
        public string TenDVTC { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public string NguoiDaiDien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOTHIENMAU> DOTHIENMAU { get; set; }
    }
}
