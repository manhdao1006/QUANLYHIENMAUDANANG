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
    
    public partial class TAIKHOAN
    {
        public string MaTK { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string MaQuyen { get; set; }
    
        public virtual KHOAHUYETHOC_TRUYENMAU KHOAHUYETHOC_TRUYENMAU { get; set; }
        public virtual KHOAXETNGHIEM KHOAXETNGHIEM { get; set; }
        public virtual PHONGNGHIEPVUY PHONGNGHIEPVUY { get; set; }
        public virtual QUYENTAIKHOAN QUYENTAIKHOAN { get; set; }
        public virtual THANHVIEN THANHVIEN { get; set; }
    }
}
