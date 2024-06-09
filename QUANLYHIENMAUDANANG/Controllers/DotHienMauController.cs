using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QUANLYHIENMAUDANANG.Models;

namespace QUANLYHIENMAUDANANG.Controllers
{
    public class DotHienMauController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: DotHienMau
        public ActionResult Index(string keyword)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var dothienmaus = db.DOTHIENMAU.Include(d => d.DONVITOCHUC).Include(d => d.KHOAXETNGHIEM).OrderByDescending(d => d.MaDot);

            if (!String.IsNullOrEmpty(keyword))
            {
                keyword = keyword.Trim();
                dothienmaus = dothienmaus.Where(d => d.MaDot.Contains(keyword) ||
                                                     d.TenDot.Contains(keyword) ||
                                                     d.KHOAXETNGHIEM.BENHVIEN.TenBV.Contains(keyword) ||
                                                     d.DONVITOCHUC.TenDVTC.Contains(keyword))
                                         .OrderByDescending(d => d.MaDot);
            }

            ViewBag.CurrentFilter = keyword;

            return View(dothienmaus.ToList());
        }

        // GET: DotHienMau/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOTHIENMAU dOTHIENMAU = db.DOTHIENMAU.Find(id);
            if (dOTHIENMAU == null)
            {
                return HttpNotFound();
            }
            return View(dOTHIENMAU);
        }

        // GET: DotHienMau/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            ViewBag.MaDVTC = new SelectList(db.DONVITOCHUC, "MaDVTC", "TenDVTC");
            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa");
            return View();
        }

        // POST: DotHienMau/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DOTHIENMAU dothHienMau, HttpPostedFileBase campaignImage)
        {
            if (ModelState.IsValid)
            {
                if (campaignImage != null && campaignImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(campaignImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/images/campaign"), fileName);
                    campaignImage.SaveAs(filePath);
                    dothHienMau.HinhAnh = fileName;
                }

                db.DOTHIENMAU.Add(dothHienMau);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.MaDVTC = new SelectList(db.DONVITOCHUC, "MaDVTC", "TenDVTC", dothHienMau.MaDVTC);
            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa", dothHienMau.MaKhoa);
            return View(dothHienMau);
        }

        // GET: DotHienMau/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOTHIENMAU dOTHIENMAU = db.DOTHIENMAU.Find(id);
            if (dOTHIENMAU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDVTC = new SelectList(db.DONVITOCHUC, "MaDVTC", "TenDVTC", dOTHIENMAU.MaDVTC);
            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa", dOTHIENMAU.MaKhoa);
            return View(dOTHIENMAU);
        }

        // POST: DotHienMau/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DOTHIENMAU dothHienMau, HttpPostedFileBase campaignImage)
        {
            if (ModelState.IsValid)
            {
                if(campaignImage != null && campaignImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(campaignImage.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Content/images/campaign"), fileName);
                    campaignImage.SaveAs(filePath);
                    dothHienMau.HinhAnh = fileName;
                }

                db.Entry(dothHienMau).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDVTC = new SelectList(db.DONVITOCHUC, "MaDVTC", "TenDVTC", dothHienMau.MaDVTC);
            ViewBag.MaKhoa = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "MaKhoa", dothHienMau.MaKhoa);
            return View(dothHienMau);
        }

        // GET: DotHienMau/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOTHIENMAU dOTHIENMAU = db.DOTHIENMAU.Find(id);
            if (dOTHIENMAU == null)
            {
                return HttpNotFound();
            }
            return View(dOTHIENMAU);
        }

        // POST: DotHienMau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DOTHIENMAU dOTHIENMAU = db.DOTHIENMAU.Find(id);
            db.DOTHIENMAU.Remove(dOTHIENMAU);
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
