using QUANLYHIENMAUDANANG.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QUANLYHIENMAUDANANG.Controllers
{
    public class ThongTinController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: ChoXacNhan
        public ActionResult ChoXacNhan(string MaTV)
        {
            if (Session["username"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            if (MaTV == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var phieuDangKyHienMaus = db.PHIEUDANGKYHIENMAU.Where(pdk => pdk.THANHVIEN.MaTV.Contains(MaTV) && pdk.DOTHIENMAU.NgayBatDau >= DateTime.Now).ToList();
            if (phieuDangKyHienMaus == null)
            {
                return HttpNotFound();
            }

            return View(phieuDangKyHienMaus);
        }

        // POST: ChoXacNhan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string MaDKHM)
        {
            PHIEUDANGKYHIENMAU phieuDangKyHienMau = db.PHIEUDANGKYHIENMAU.Find(MaDKHM);
            db.PHIEUDANGKYHIENMAU.Remove(phieuDangKyHienMau);
            db.SaveChanges();

            return RedirectToAction("ChoXacNhan", new { MaTV = Session["userid"] });
        }

        // GET: DaHien
        public ActionResult DaHien(string MaTV)
        {
            if (Session["username"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            if (MaTV == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var chiTietHienMaus = db.CHITIETHIENMAU.Where(cthm => cthm.PHIEUDANGKYHIENMAU.MaTV.Contains(MaTV)).ToList();
            if (chiTietHienMaus == null)
            {
                return HttpNotFound();
            }

            return View(chiTietHienMaus);
        }

        // GET: CapNhatPhieuDangKy
        public ActionResult CapNhatPhieuDangKy(string MaTV, string MaDot)
        {
            if (Session["username"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            if (MaTV == null || MaDot == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            PHIEUDANGKYHIENMAU phieuDangKy = db.PHIEUDANGKYHIENMAU.FirstOrDefault(pdk => pdk.THANHVIEN.MaTV.Contains(MaTV) && pdk.DOTHIENMAU.MaDot.Contains(MaDot));
            if (phieuDangKy == null)
            {
                return HttpNotFound();
            }

            return View(phieuDangKy);
        }

        // POST: CapNhatPhieuDangKy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatPhieuDangKy([Bind(Include = "MaDKHM,TenNguoiHien,NgaySinh,GioiTinh,DiaChi,SoDienThoai,Email,SoCCCD,NgayDKHM,MaTV,MaDot")] PHIEUDANGKYHIENMAU phieuDangKyHienMau)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuDangKyHienMau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ChoXacNhan", new { MaTV = phieuDangKyHienMau.MaTV });
            }

            return View(phieuDangKyHienMau);
        }
    }
}