using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QUANLYHIENMAUDANANG.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: DotHienMau
        public ActionResult DotHienMau()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            return View();
        }

        // GET: NganHangMau
        public ActionResult NganHangMau()
        {
            if (Session["admin"] == null)
                return RedirectToAction("../TrangChu/DangNhap");

            return View();
        }
    }
}