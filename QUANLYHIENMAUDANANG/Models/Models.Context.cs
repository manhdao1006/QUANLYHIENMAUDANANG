﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLHIENMAU_31Entities : DbContext
    {
        public QLHIENMAU_31Entities()
            : base("name=QLHIENMAU_31Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BENHVIEN> BENHVIEN { get; set; }
        public virtual DbSet<CHITIETPHANBOMAU> CHITIETPHANBOMAU { get; set; }
        public virtual DbSet<CHITIETPHIEUNHAP> CHITIETPHIEUNHAP { get; set; }
        public virtual DbSet<CHITIETHIENMAU> CHITIETHIENMAU { get; set; }
        public virtual DbSet<DONVITOCHUC> DONVITOCHUC { get; set; }
        public virtual DbSet<DOTHIENMAU> DOTHIENMAU { get; set; }
        public virtual DbSet<KHOAHUYETHOC_TRUYENMAU> KHOAHUYETHOC_TRUYENMAU { get; set; }
        public virtual DbSet<KHOAXETNGHIEM> KHOAXETNGHIEM { get; set; }
        public virtual DbSet<NGANHANGMAU> NGANHANGMAU { get; set; }
        public virtual DbSet<NHANVIENKHOAXETNGHIEM> NHANVIENKHOAXETNGHIEM { get; set; }
        public virtual DbSet<NHOMMAU> NHOMMAU { get; set; }
        public virtual DbSet<PHANBOMAU> PHANBOMAU { get; set; }
        public virtual DbSet<PHIEUDANGKYHIENMAU> PHIEUDANGKYHIENMAU { get; set; }
        public virtual DbSet<PHIEUNHAPMAU> PHIEUNHAPMAU { get; set; }
        public virtual DbSet<PHONGNGHIEPVUY> PHONGNGHIEPVUY { get; set; }
        public virtual DbSet<QUYENTAIKHOAN> QUYENTAIKHOAN { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOAN { get; set; }
        public virtual DbSet<THANHVIEN> THANHVIEN { get; set; }
    }
}