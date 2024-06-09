using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QUANLYHIENMAUDANANG.Models;

namespace QUANLYHIENMAUDANANG.Controllers
{
    public class DangKyHienMauController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: DangKyHienMau
        public ActionResult Index(string keyword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var phieuDangKyHienMau = db.PHIEUDANGKYHIENMAU.Include(p => p.DOTHIENMAU).Include(p => p.THANHVIEN).OrderByDescending(p => p.MaDKHM);

            if (!string.IsNullOrEmpty(keyword))
            {
                phieuDangKyHienMau = phieuDangKyHienMau.Where(p => p.TenNguoiHien.Contains(keyword) ||
                                                                   p.GioiTinh.Contains(keyword) ||
                                                                   p.DiaChi.Contains(keyword) ||
                                                                   p.SoDienThoai.Contains(keyword) ||
                                                                   p.SoCCCD.Contains(keyword) ||
                                                                   p.DOTHIENMAU.TenDot.Contains(keyword))
                                                       .OrderByDescending(p => p.MaDKHM);
            }

            ViewBag.CurrentFilter = keyword;

            return View(phieuDangKyHienMau.ToList());
        }

        // GET: DangKyHienMau/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDANGKYHIENMAU pHIEUDANGKYHIENMAU = db.PHIEUDANGKYHIENMAU.Find(id);
            if (pHIEUDANGKYHIENMAU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUDANGKYHIENMAU);
        }

        // GET: DangKyHienMau/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            ViewBag.MaDot = new SelectList(db.DOTHIENMAU, "MaDot", "TenDot");
            ViewBag.MaTV = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen");
            return View();
        }

        // POST: DangKyHienMau/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDKHM,TenNguoiHien,NgaySinh,GioiTinh,DiaChi,SoDienThoai,Email,SoCCCD,NgayDKHM,MaTV,MaDot")] PHIEUDANGKYHIENMAU pHIEUDANGKYHIENMAU)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUDANGKYHIENMAU.Add(pHIEUDANGKYHIENMAU);

                var dotHienMau = db.DOTHIENMAU.Find(pHIEUDANGKYHIENMAU.MaDot);
                if (dotHienMau != null)
                {
                    dotHienMau.SoLuongDangKy++;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDot = new SelectList(db.DOTHIENMAU, "MaDot", "TenDot", pHIEUDANGKYHIENMAU.MaDot);
            ViewBag.MaTV = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen", pHIEUDANGKYHIENMAU.MaTV);
            return View(pHIEUDANGKYHIENMAU);
        }

        // GET: DangKyHienMau/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDANGKYHIENMAU pHIEUDANGKYHIENMAU = db.PHIEUDANGKYHIENMAU.Find(id);
            if (pHIEUDANGKYHIENMAU == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaDot = new SelectList(db.DOTHIENMAU, "MaDot", "TenDot", pHIEUDANGKYHIENMAU.MaDot);
            ViewBag.MaTV = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen", pHIEUDANGKYHIENMAU.MaTV);
            return View(pHIEUDANGKYHIENMAU);
        }

        // POST: DangKyHienMau/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDKHM,TenNguoiHien,NgaySinh,GioiTinh,DiaChi,SoDienThoai,Email,SoCCCD,NgayDKHM,MaTV,MaDot")] PHIEUDANGKYHIENMAU phieuDangKyHienMau)
        {
            if (ModelState.IsValid)
            {
                var phieuGoc = db.PHIEUDANGKYHIENMAU.AsNoTracking().FirstOrDefault(p => p.MaDKHM == phieuDangKyHienMau.MaDKHM);

                if (phieuGoc == null)
                {
                    return HttpNotFound();
                }

                db.Entry(phieuDangKyHienMau).State = EntityState.Modified;

                if (phieuGoc.MaDot != phieuDangKyHienMau.MaDot)
                {
                    var dotHienMauCu = db.DOTHIENMAU.Find(phieuGoc.MaDot);
                    if (dotHienMauCu != null && dotHienMauCu.SoLuongDangKy > 0)
                    {
                        dotHienMauCu.SoLuongDangKy--;
                    }

                    var dotHienMauMoi = db.DOTHIENMAU.Find(phieuDangKyHienMau.MaDot);
                    if (dotHienMauMoi != null)
                    {
                        dotHienMauMoi.SoLuongDangKy++;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDot = new SelectList(db.DOTHIENMAU, "MaDot", "TenDot", phieuDangKyHienMau.MaDot);
            ViewBag.MaTV = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen", phieuDangKyHienMau.MaTV);
            return View(phieuDangKyHienMau);
        }

        // GET: DangKyHienMau/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUDANGKYHIENMAU phieuDangKyHienMau = db.PHIEUDANGKYHIENMAU.Find(id);
            if (phieuDangKyHienMau == null)
            {
                return HttpNotFound();
            }
            return View(phieuDangKyHienMau);
        }

        // POST: DangKyHienMau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PHIEUDANGKYHIENMAU phieuDangKyHienMau = db.PHIEUDANGKYHIENMAU.Find(id);

            var dotHienMau = db.DOTHIENMAU.Find(phieuDangKyHienMau.MaDot);
            if (dotHienMau != null && dotHienMau.SoLuongDangKy > 0)
            {
                dotHienMau.SoLuongDangKy--;
            }

            db.PHIEUDANGKYHIENMAU.Remove(phieuDangKyHienMau);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
