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
    public class BenhVienController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: BenhVien
        public ActionResult Index(string keyword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var benhViens = db.BENHVIEN.ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                benhViens = benhViens.Where(b =>
                    b.TenBV.Contains(keyword) ||
                    b.NguoiDaiDien.Contains(keyword) ||
                    b.DiaChi.Contains(keyword) ||
                    b.SoDienThoai.Contains(keyword) ||
                    b.Email.Contains(keyword)
                ).ToList();
            }

            ViewBag.CurrentFilter = keyword;

            return View(benhViens);
        }

        // GET: BenhVien/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENHVIEN bENHVIEN = db.BENHVIEN.Find(id);
            if (bENHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(bENHVIEN);
        }

        // GET: BenhVien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BenhVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaBV,TenBV,NguoiDaiDien,DiaChi,SoDienThoai,Email")] BENHVIEN bENHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.BENHVIEN.Add(bENHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bENHVIEN);
        }

        // GET: BenhVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENHVIEN bENHVIEN = db.BENHVIEN.Find(id);
            if (bENHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(bENHVIEN);
        }

        // POST: BenhVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaBV,TenBV,NguoiDaiDien,DiaChi,SoDienThoai,Email")] BENHVIEN bENHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bENHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bENHVIEN);
        }

        // GET: BenhVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BENHVIEN bENHVIEN = db.BENHVIEN.Find(id);
            if (bENHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(bENHVIEN);
        }

        // POST: BenhVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BENHVIEN bENHVIEN = db.BENHVIEN.Find(id);
            db.BENHVIEN.Remove(bENHVIEN);
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
