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
    
    public partial class KHOAHUYETHOC_TRUYENMAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHOAHUYETHOC_TRUYENMAU()
        {
            this.NGANHANGMAU = new HashSet<NGANHANGMAU>();
            this.PHIEUNHAPMAU = new HashSet<PHIEUNHAPMAU>();
        }
    
        public string MaKhoa { get; set; }
        public string NguoiDaiDien { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string MaBV { get; set; }
    
        public virtual BENHVIEN BENHVIEN { get; set; }
        public virtual TAIKHOAN TAIKHOAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NGANHANGMAU> NGANHANGMAU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAPMAU> PHIEUNHAPMAU { get; set; }
    }
}
