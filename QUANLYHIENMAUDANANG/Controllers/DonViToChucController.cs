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
    public class DonViToChucController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: DonViToChuc
        public ActionResult Index(string keyword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var donViToChucs = db.DONVITOCHUC.ToList();

            if (!string.IsNullOrEmpty(keyword))
            {
                donViToChucs = donViToChucs.Where(d =>
                    d.TenDVTC.Contains(keyword) ||
                    d.DiaChi.Contains(keyword) ||
                    d.SoDienThoai.Contains(keyword) ||
                    d.NguoiDaiDien.Contains(keyword)
                ).ToList();
            }

            ViewBag.CurrentFilter = keyword;

            return View(donViToChucs);
        }

        // GET: DonViToChuc/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONVITOCHUC dONVITOCHUC = db.DONVITOCHUC.Find(id);
            if (dONVITOCHUC == null)
            {
                return HttpNotFound();
            }
            return View(dONVITOCHUC);
        }

        // GET: DonViToChuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonViToChuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDVTC,TenDVTC,DiaChi,SoDienThoai,NguoiDaiDien")] DONVITOCHUC dONVITOCHUC)
        {
            if (ModelState.IsValid)
            {
                db.DONVITOCHUC.Add(dONVITOCHUC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dONVITOCHUC);
        }

        // GET: DonViToChuc/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONVITOCHUC dONVITOCHUC = db.DONVITOCHUC.Find(id);
            if (dONVITOCHUC == null)
            {
                return HttpNotFound();
            }
            return View(dONVITOCHUC);
        }

        // POST: DonViToChuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDVTC,TenDVTC,DiaChi,SoDienThoai,NguoiDaiDien")] DONVITOCHUC dONVITOCHUC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONVITOCHUC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dONVITOCHUC);
        }

        // GET: DonViToChuc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONVITOCHUC dONVITOCHUC = db.DONVITOCHUC.Find(id);
            if (dONVITOCHUC == null)
            {
                return HttpNotFound();
            }
            return View(dONVITOCHUC);
        }

        // POST: DonViToChuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DONVITOCHUC dONVITOCHUC = db.DONVITOCHUC.Find(id);
            db.DONVITOCHUC.Remove(dONVITOCHUC);
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
