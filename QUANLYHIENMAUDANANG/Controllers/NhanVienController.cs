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
    public class NhanVienController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: NhanVien
        public ActionResult Index(string keyword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var nhanViens = db.NHANVIENKHOAXETNGHIEM.Include(n => n.KHOAXETNGHIEM);

            if (!string.IsNullOrEmpty(keyword))
            {
                nhanViens = nhanViens.Where(n =>
                    n.TenNV.Contains(keyword) ||
                    n.ChucVu.Contains(keyword) ||
                    n.GioiTinh.Contains(keyword) ||
                    n.SoDienThoai.Contains(keyword) ||
                    n.KHOAXETNGHIEM.BENHVIEN.TenBV.Contains(keyword)
                );
            }

            ViewBag.CurrentFilter = keyword;

            return View(nhanViens.ToList());
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIENKHOAXETNGHIEM nHANVIENKHOAXETNGHIEM = db.NHANVIENKHOAXETNGHIEM.Find(id);
            if (nHANVIENKHOAXETNGHIEM == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIENKHOAXETNGHIEM);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,TenNV,ChucVu,GioiTinh,SoDienThoai,Email,MaKhoa")] NHANVIENKHOAXETNGHIEM nHANVIENKHOAXETNGHIEM)
        {
            if (ModelState.IsValid)
            {
                db.NHANVIENKHOAXETNGHIEM.Add(nHANVIENKHOAXETNGHIEM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa", nHANVIENKHOAXETNGHIEM.MaKhoa);
            return View(nHANVIENKHOAXETNGHIEM);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIENKHOAXETNGHIEM nHANVIENKHOAXETNGHIEM = db.NHANVIENKHOAXETNGHIEM.Find(id);
            if (nHANVIENKHOAXETNGHIEM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa", nHANVIENKHOAXETNGHIEM.MaKhoa);
            return View(nHANVIENKHOAXETNGHIEM);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,TenNV,ChucVu,GioiTinh,SoDienThoai,Email,MaKhoa")] NHANVIENKHOAXETNGHIEM nHANVIENKHOAXETNGHIEM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIENKHOAXETNGHIEM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa", nHANVIENKHOAXETNGHIEM.MaKhoa);
            return View(nHANVIENKHOAXETNGHIEM);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIENKHOAXETNGHIEM nHANVIENKHOAXETNGHIEM = db.NHANVIENKHOAXETNGHIEM.Find(id);
            if (nHANVIENKHOAXETNGHIEM == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIENKHOAXETNGHIEM);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NHANVIENKHOAXETNGHIEM nHANVIENKHOAXETNGHIEM = db.NHANVIENKHOAXETNGHIEM.Find(id);
            db.NHANVIENKHOAXETNGHIEM.Remove(nHANVIENKHOAXETNGHIEM);
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
