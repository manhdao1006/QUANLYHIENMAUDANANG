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
    
    public partial class CHITIETPHANBOMAU
    {
        public string MaPB { get; set; }
        public string MaNHM { get; set; }
        public Nullable<int> TheTichMau { get; set; }
    
        public virtual NGANHANGMAU NGANHANGMAU { get; set; }
        public virtual PHANBOMAU PHANBOMAU { get; set; }
    }
}
