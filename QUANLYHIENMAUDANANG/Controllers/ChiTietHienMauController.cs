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
    public class ChiTietHienMauController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: ChiTietHienMau
        public ActionResult Index(string keyword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var cHITIETHIENMAU = db.CHITIETHIENMAU.Include(c => c.PHIEUDANGKYHIENMAU).Include(c => c.NHOMMAU).Include(c => c.NHANVIENKHOAXETNGHIEM);

            if (!string.IsNullOrEmpty(keyword))
            {
                // Filter by keyword on relevant fields
                if (keyword.Equals("Ổn định", StringComparison.OrdinalIgnoreCase)) // Check for exact match
                {
                    cHITIETHIENMAU = cHITIETHIENMAU.Where(c => c.TinhTrangSucKhoe.Equals("Ổn định", StringComparison.OrdinalIgnoreCase));
                }
                else
                {
                    cHITIETHIENMAU = cHITIETHIENMAU.Where(c =>
                        c.PHIEUDANGKYHIENMAU.TenNguoiHien.Contains(keyword) ||
                        c.TheTichMau.ToString().Contains(keyword) ||
                        c.TinhTrangSucKhoe.Contains(keyword) ||
                        c.KetQua.Contains(keyword) ||
                        c.ChuThich.Contains(keyword) ||
                        c.NHOMMAU.MaNM.Contains(keyword) ||
                        c.NHANVIENKHOAXETNGHIEM.TenNV.Contains(keyword)
                    );
                }
            }

            ViewBag.CurrentFilter = keyword;

            return View(cHITIETHIENMAU.ToList());
        }

        // GET: ChiTietHienMau/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHIENMAU cHITIETHIENMAU = db.CHITIETHIENMAU.Find(id);
            if (cHITIETHIENMAU == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHIENMAU);
        }

        // GET: ChiTietHienMau/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            ViewBag.MaDKHM = new SelectList(db.PHIEUDANGKYHIENMAU, "MaDKHM", "MaDKHM");
            ViewBag.MaNM = new SelectList(db.NHOMMAU, "MaNM", "MaNM");
            ViewBag.MaNV = new SelectList(db.NHANVIENKHOAXETNGHIEM, "MaNV", "MaNV");
            return View();
        }

        // POST: ChiTietHienMau/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaCTHM,ThoiGianBatDau,ThoiGianKetThuc,TheTichMau,TinhTrangSucKhoe,KetQua,ChuThich,MaNV,MaNM,MaDKHM")] CHITIETHIENMAU cHITIETHIENMAU)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHIENMAU.Add(cHITIETHIENMAU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDKHM = new SelectList(db.PHIEUDANGKYHIENMAU, "MaDKHM", "MaDKHM", cHITIETHIENMAU.MaDKHM);
            ViewBag.MaNM = new SelectList(db.NHOMMAU, "MaNM", "MaNM", cHITIETHIENMAU.MaNM);
            ViewBag.MaNV = new SelectList(db.NHANVIENKHOAXETNGHIEM, "MaNV", "MaNV", cHITIETHIENMAU.MaNV);
            return View(cHITIETHIENMAU);
        }

        // GET: ChiTietHienMau/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHIENMAU cHITIETHIENMAU = db.CHITIETHIENMAU.Find(id);
            if (cHITIETHIENMAU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDKHM = new SelectList(db.PHIEUDANGKYHIENMAU, "MaDKHM", "MaDKHM", cHITIETHIENMAU.MaDKHM);
            ViewBag.MaNM = new SelectList(db.NHOMMAU, "MaNM", "MaNM", cHITIETHIENMAU.MaNM);
            ViewBag.MaNV = new SelectList(db.NHANVIENKHOAXETNGHIEM, "MaNV", "MaNV", cHITIETHIENMAU.MaNV);
            return View(cHITIETHIENMAU);
        }

        // POST: ChiTietHienMau/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaCTHM,ThoiGianBatDau,ThoiGianKetThuc,TheTichMau,TinhTrangSucKhoe,KetQua,ChuThich,MaNV,MaNM,MaDKHM")] CHITIETHIENMAU cHITIETHIENMAU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETHIENMAU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDKHM = new SelectList(db.PHIEUDANGKYHIENMAU, "MaDKHM", "MaDKHM", cHITIETHIENMAU.MaDKHM);
            ViewBag.MaNM = new SelectList(db.NHOMMAU, "MaNM", "MaNM", cHITIETHIENMAU.MaNM);
            ViewBag.MaNV = new SelectList(db.NHANVIENKHOAXETNGHIEM, "MaNV", "MaNV", cHITIETHIENMAU.MaNV);
            return View(cHITIETHIENMAU);
        }

        // GET: ChiTietHienMau/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHIENMAU cHITIETHIENMAU = db.CHITIETHIENMAU.Find(id);
            if (cHITIETHIENMAU == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHIENMAU);
        }

        // POST: ChiTietHienMau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETHIENMAU cHITIETHIENMAU = db.CHITIETHIENMAU.Find(id);
            db.CHITIETHIENMAU.Remove(cHITIETHIENMAU);
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
