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
    
    public partial class NGANHANGMAU
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGANHANGMAU()
        {
            this.CHITIETPHANBOMAU = new HashSet<CHITIETPHANBOMAU>();
        }
    
        public string MaNHM { get; set; }
        public Nullable<int> TheTichMau { get; set; }
        public Nullable<System.DateTime> NgayNhap { get; set; }
        public Nullable<System.DateTime> HanSuDung { get; set; }
        public string MaKhoa { get; set; }
        public string MaNM { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHANBOMAU> CHITIETPHANBOMAU { get; set; }
        public virtual KHOAHUYETHOC_TRUYENMAU KHOAHUYETHOC_TRUYENMAU { get; set; }
        public virtual NHOMMAU NHOMMAU { get; set; }
    }
}
