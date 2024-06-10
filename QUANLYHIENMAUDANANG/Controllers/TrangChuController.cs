using QUANLYHIENMAUDANANG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QUANLYHIENMAUDANANG.Controllers
{
    public class TrangChuController : Controller
    {
        private QLHIENMAU_31Entities db = new QLHIENMAU_31Entities();

        // GET: DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: TrangChu/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap([Bind(Include = "MaTK,TenDangNhap,MatKhau,MaQuyen")] TAIKHOAN taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var user = db.TAIKHOAN.FirstOrDefault(acc => acc.TenDangNhap == taiKhoan.TenDangNhap 
                                                        && acc.MatKhau == taiKhoan.MatKhau 
                                                        && acc.MaQuyen == "TK05");
                var admin = db.TAIKHOAN.FirstOrDefault(acc => acc.TenDangNhap == taiKhoan.TenDangNhap
                                                        && acc.MatKhau == taiKhoan.MatKhau
                                                        && acc.MaQuyen == "TK01");
                if (admin != null)
                {
                    Session["admin"] = admin.TenDangNhap;
                    return RedirectToAction("../DotHienMau/Index");
                }
                else if (user != null)
                {
                    Session["username"] = user.TenDangNhap;
                    Session["userid"] = user.MaTK;
                    Session["fullname"] = user.THANHVIEN.HoVaTen;
                    return RedirectToAction("DanhSach");                    
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác");
                }
            }

            return View(taiKhoan);
        }

        // GET: DangKy
        public ActionResult DangKy()
        {
            return View();
        }

        // POST: DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(TAIKHOAN taiKhoan)
        {
            if (ModelState.IsValid)
            {
                var user = db.TAIKHOAN.FirstOrDefault(acc => acc.TenDangNhap == taiKhoan.TenDangNhap
                                                        && acc.MaQuyen == "TK05");
                var admin = db.TAIKHOAN.FirstOrDefault(acc => acc.TenDangNhap == taiKhoan.TenDangNhap
                                                        && acc.MaQuyen == "TK01");

                if (user != null || admin != null)
                {
                    ModelState.AddModelError("", "Tài khoản đã tồn tại!");
                }
                else
                {
                    string maTK = GenerateMaTK();
                    taiKhoan.MaTK = maTK;
                    taiKhoan.MaQuyen = "TK05";

                    db.TAIKHOAN.Add(taiKhoan);
                    try
                    {
                        db.SaveChanges();
                        return RedirectToAction("DangKyThanhVien", new { MaTK = maTK });
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi khi đăng ký tài khoản: " + ex.Message);
                    }
                }                
            }

            return View(taiKhoan);
        }

        // Hàm tạo mã tài khoản tự động
        private string GenerateMaTK()
        {
            var maxMaTK = db.TAIKHOAN.OrderByDescending(tk => tk.MaTK).FirstOrDefault()?.MaTK;
            if (maxMaTK != null)
            {
                int number = int.Parse(maxMaTK.Substring(2)) + 1;
                return "TK" + number.ToString("D5");
            }
            else
            {
                return "TK00001";
            }
        }

        // GET: DangKyThanhVien
        public ActionResult DangKyThanhVien(string MaTK)
        {
            if (MaTK == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            TAIKHOAN taiKhoan = db.TAIKHOAN.Find(MaTK);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }

            // Tạo một đối tượng THANHVIEN và gán các giá trị cần thiết từ TAIKHOAN
            THANHVIEN thanhVien = new THANHVIEN();
            thanhVien.MaTV = taiKhoan.MaTK; // Gán MaTK từ TAIKHOAN sang THANHVIEN
                                            // Thêm các dữ liệu khác của THANHVIEN nếu cần thiết

            // Đặt ViewBag.MaNM với SelectList tương ứng
            ViewBag.MaNM = new SelectList(db.NHOMMAU, "MaNM", "MaNM");
            return View(thanhVien);
        }

        // POST: DangKyThanhVien
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKyThanhVien([Bind(Include = "MaTV,HoVaTen,NgaySinh,GioiTinh,DiaChi,SoDienThoai,Email,MaNM")] THANHVIEN thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.THANHVIEN.Add(thanhVien);
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("DangNhap");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi khi đăng ký thành viên: " + ex.Message);
                }
            }

            ViewBag.MaNM = new SelectList(db.NHOMMAU, "MaNM", "MaNM");
            return View(thanhVien);
        }

        // GET: DangXuatAdmin
        public ActionResult DangXuatAdmin()
        {
            Session["admin"] = null;
            return RedirectToAction("DangNhap");
        }

        // GET: DangXuatUser
        public ActionResult DangXuatUser()
        {
            Session["username"] = null;
            return RedirectToAction("DanhSach");
        }

        // GET: DanhSach
        public ActionResult DanhSach()
        {
            try
            {
                var dotHienMauList = db.DOTHIENMAU.Include("DONVITOCHUC").ToList(); // Lấy danh sách đợt hiến máu kèm theo thông tin đơn vị tổ chức
                return View(dotHienMauList); // Truyền danh sách đợt hiến máu vào view
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ, có thể ghi log lỗi, hiển thị thông báo cho người dùng, chuyển hướng đến trang lỗi, vv.
                return View("Error");
            }
        }

        // GET: DanhSachTruong
        public ActionResult DanhSachTruong()
        {
            try
            {
                var dotHienMauList = db.DOTHIENMAU
                    .Where(d => d.DONVITOCHUC.TenDVTC.Contains("Trường") && d.NgayBatDau > DateTime.Now)
                    .ToList();
                return View(dotHienMauList);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: DanhSachBenhVien
        public ActionResult DanhSachBenhVien()
        {
            try
            {
                var dotHienMauList = db.DOTHIENMAU
                    .Where(d => d.DONVITOCHUC.TenDVTC.Contains("Bệnh viện") && d.NgayBatDau > DateTime.Now)
                    .ToList();
                return View(dotHienMauList);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: DanhSachKhac
        public ActionResult DanhSachKhac()
        {
            try
            {
                var dotHienMauList = db.DOTHIENMAU
                    .Where(d => !d.DONVITOCHUC.TenDVTC.Contains("Trường") && !d.DONVITOCHUC.TenDVTC.Contains("Bệnh viện") && d.NgayBatDau > DateTime.Now)
                    .ToList();
                return View(dotHienMauList);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // GET: DangKyHienMau
        public ActionResult DangKyHienMau(string MaDot)
        {
            if (Session["username"] == null)
                return RedirectToAction("DangNhap");

            if (MaDot == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DOTHIENMAU dotHienMau = db.DOTHIENMAU.Find(MaDot);
            if (dotHienMau == null)
            {
                return HttpNotFound();
            }

            return View(dotHienMau);
        }

        // POST: TrangChu/DangKyHienMau
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKyHienMau([Bind(Include = "MaDKHM,TenNguoiHien,NgaySinh,GioiTinh,DiaChi,SoDienThoai,Email,SoCCCD,NgayDKHM,MaTV,MaDot")] PHIEUDANGKYHIENMAU phieuDangKyHienMau)
        {
            if (ModelState.IsValid)
            {
                string maDKHM = GenerateMaDKHM();
                phieuDangKyHienMau.MaDKHM = maDKHM;
                phieuDangKyHienMau.NgayDKHM = DateTime.Now;

                db.PHIEUDANGKYHIENMAU.Add(phieuDangKyHienMau);
                db.SaveChanges();
                return RedirectToAction("DanhSach");
            }

            return View(phieuDangKyHienMau);
        }

        // Hàm tạo mã đăng ký hiến máu tự động
        private string GenerateMaDKHM()
        {
            var maxMaDKHM = db.PHIEUDANGKYHIENMAU.OrderByDescending(dkhm => dkhm.MaDKHM).FirstOrDefault()?.MaDKHM;
            if (maxMaDKHM != null)
            {
                int number = int.Parse(maxMaDKHM.Substring(2)) + 1;
                return "DK" + number.ToString("D3");
            }
            else
            {
                return "DK001";
            }
        }
    }
}