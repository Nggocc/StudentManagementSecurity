using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;


namespace QuanLySachThuVien.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        DBContext db = new DBContext();
        private byte[] CalculateSHA256(string str)
        {
            // Tạo đối tượng SHA256
            SHA256 sha256 = SHA256Managed.Create();

            // Khai báo mảng byte để lưu trữ giá trị băm
            byte[] hashValue;

            // Tạo đối tượng mã hóa UTF8
            UTF8Encoding objUtf8 = new UTF8Encoding();

            // Chuyển đổi chuỗi đầu vào thành mảng byte và tính toán giá trị băm
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

            // Trả về giá trị băm
            return hashValue;
        }
        private void ConvertToHash()
        {
            List<taikhoan> taikhoans = db.taikhoans.ToList();
            foreach (taikhoan tk in taikhoans)
            {
                tk.hash = CalculateSHA256(tk.matkhau).ToString();
                db.SaveChanges();
            }
        }

        public ActionResult DangXuat()
        {
            Session["taikhoan"] = null;
            return RedirectToAction("DangNhap");
        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tenDangNhap, string matKhau)
        {
            //Kiểm tra xem đã điền đủ tài khoản và mật khẩu chưa
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.thongBao = "Vui lòng nhập đầy đủ thông tin !";
                return View();
            }

            //Tìm tài khoản theo tên đăng nhập trong database
            DBContext db = new DBContext();
            var taiKhoan = db.taikhoans.SingleOrDefault(m => m.tenDangNhap == tenDangNhap);

            //Kiểm tra tài khoản tồn tại không
            if (taiKhoan == null)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                return View();
            }
            else
            {
                if (taiKhoan.tenDangNhap.Contains("2024SV"))
                {
                    ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                    return View();
                }
            }

            ConvertToHash();
            //Kiểm tra mật khẩu có đúng không
            if (taiKhoan.hash != CalculateSHA256(matKhau).ToString())
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                return View();
            }
            Session["taikhoan"] = taiKhoan;
            return Redirect("/sinhviens/Index");
        }
        public ActionResult TaiKhoanCuaToi()
        {
            taikhoan tk = (taikhoan)Session["taikhoan"];
            giangvien gv = db.giangviens.FirstOrDefault(dn => dn.tenDangNhap == tk.tenDangNhap);
            return View(gv);
        }
        [HttpPost]
        public ActionResult TaiKhoanCuaToi(giangvien gv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var f = Request.Files["ImageFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/Content/Images/" + fileName);
                        f.SaveAs(uploadPath);
                        gv.hinhAnh = fileName;
                    }
                    else
                    {
                        gv.hinhAnh = Request.Form["hinhAnh"];
                    }

                    db.Entry(gv).State = EntityState.Modified;
                    db.SaveChanges();
                    View(gv);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi: " + ex.Message;

            }
            return View(gv);
        }
        [HttpGet]
        public ActionResult SetPass()
        {
            taikhoan tk = (taikhoan)Session["taikhoan"];
            return View(tk);
        }
        public ActionResult SetPass(string oldpass, string newpass, taikhoan tk)
        {

            if (ModelState.IsValid)
            {
                if (oldpass != db.taikhoans.FirstOrDefault(t => t.tenDangNhap == tk.tenDangNhap).matkhau)
                {
                    ViewBag.Error = "Mật khẩu cũ không đúng!";
                    return View();
                }
                if (String.IsNullOrEmpty(newpass))
                {
                    ViewBag.Error = "Bạn cần xác nhận lại mật khẩu!";
                    return View();
                }
                if (String.IsNullOrEmpty(newpass))
                {
                    ViewBag.Error = "Mật khẩu mới đang trống";
                    return View();
                }
                if (newpass.Length < 8)
                {

                    ViewBag.Error = "Mật khẩu phải có độ dài tối thiểu 8 ký tự!";
                    return View();

                }
                if (tk.matkhau == newpass)
                {
                    db.taikhoans.Where(t => t.tenDangNhap == tk.tenDangNhap).FirstOrDefault().matkhau = tk.matkhau;
                    db.SaveChanges();
                    return RedirectToAction("DangNhap");

                }

            }
            return View();
        }

    }
}