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
    
    public partial class NHOMMAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHOMMAU()
        {
            this.CHITIETPHIEUNHAP = new HashSet<CHITIETPHIEUNHAP>();
            this.CHITIETHIENMAU = new HashSet<CHITIETHIENMAU>();
            this.NGANHANGMAU = new HashSet<NGANHANGMAU>();
            this.THANHVIEN = new HashSet<THANHVIEN>();
        }
    
        public string MaNM { get; set; }
        public string ChuThich { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHIENMAU> CHITIETHIENMAU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NGANHANGMAU> NGANHANGMAU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THANHVIEN> THANHVIEN { get; set; }
    }
}