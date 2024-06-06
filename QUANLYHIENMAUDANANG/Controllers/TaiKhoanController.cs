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
    public class TaiKhoanController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: TaiKhoan
        public ActionResult Index(string MaQuyen)
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            var taikhoans = db.TAIKHOAN.Include(t => t.KHOAHUYETHOC_TRUYENMAU).Include(t => t.KHOAXETNGHIEM).Include(t => t.PHONGNGHIEPVUY).Include(t => t.QUYENTAIKHOAN).Include(t => t.THANHVIEN);

            if (!string.IsNullOrEmpty(MaQuyen))
            {
                taikhoans = taikhoans.Where(sv => sv.QUYENTAIKHOAN.MaQuyen == MaQuyen);
                ViewBag.CurrentFilter = string.Empty;
            }

            ViewBag.Quyens = new SelectList(db.QUYENTAIKHOAN, "MaQuyen", "TenQuyen");
            return View(taikhoans);
        }

        // GET: TaiKhoan/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // GET: TaiKhoan/Create
        public ActionResult Create()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap"); 
            
            ViewBag.MaTK = new SelectList(db.KHOAHUYETHOC_TRUYENMAU, "MaKhoa", "NguoiDaiDien");
            ViewBag.MaTK = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "NguoiDaiDien");
            ViewBag.MaTK = new SelectList(db.PHONGNGHIEPVUY, "MaPhong", "NguoiDaiDien");
            ViewBag.MaQuyen = new SelectList(db.QUYENTAIKHOAN, "MaQuyen", "TenQuyen");
            ViewBag.MaTK = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen");
            return View();
        }

        // POST: TaiKhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTK,TenDangNhap,MatKhau,MaQuyen")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.TAIKHOAN.Add(tAIKHOAN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTK = new SelectList(db.KHOAHUYETHOC_TRUYENMAU, "MaKhoa", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaTK = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaTK = new SelectList(db.PHONGNGHIEPVUY, "MaPhong", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaQuyen = new SelectList(db.QUYENTAIKHOAN, "MaQuyen", "TenQuyen", tAIKHOAN.MaQuyen);
            ViewBag.MaTK = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen", tAIKHOAN.MaTK);
            return View(tAIKHOAN);
        }

        // GET: TaiKhoan/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTK = new SelectList(db.KHOAHUYETHOC_TRUYENMAU, "MaKhoa", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaTK = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaTK = new SelectList(db.PHONGNGHIEPVUY, "MaPhong", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaQuyen = new SelectList(db.QUYENTAIKHOAN, "MaQuyen", "TenQuyen", tAIKHOAN.MaQuyen);
            ViewBag.MaTK = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen", tAIKHOAN.MaTK);
            return View(tAIKHOAN);
        }

        // POST: TaiKhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTK,TenDangNhap,MatKhau,MaQuyen")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTK = new SelectList(db.KHOAHUYETHOC_TRUYENMAU, "MaKhoa", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaTK = new SelectList(db.KHOAXETNGHIEM, "MaKhoa", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaTK = new SelectList(db.PHONGNGHIEPVUY, "MaPhong", "NguoiDaiDien", tAIKHOAN.MaTK);
            ViewBag.MaQuyen = new SelectList(db.QUYENTAIKHOAN, "MaQuyen", "TenQuyen", tAIKHOAN.MaQuyen);
            ViewBag.MaTK = new SelectList(db.THANHVIEN, "MaTV", "HoVaTen", tAIKHOAN.MaTK);
            return View(tAIKHOAN);
        }

        // GET: TaiKhoan/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: TaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TAIKHOAN tAIKHOAN = db.TAIKHOAN.Find(id);
            db.TAIKHOAN.Remove(tAIKHOAN);
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
