using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Controllers
{
    public class sinhviensController : Controller
    {
        private DBContext db = new DBContext();

        // GET: sinhviens
        public ActionResult Index()
        {
            taikhoan taikhoan = (taikhoan)Session["taikhoan"];
            if (taikhoan == null)
            {
                return Redirect("/DangNhap/DangNhap");
            }

            return View(db.sinhviens.ToList());
        }
        public ActionResult TimKiem(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return View("Index");
            }
            else
            {
                var lisv = db.sinhviens.Where(sv => sv.maSV.ToString().Contains(key) || sv.hoTen.Contains(key)||sv.tenDangNhap.Contains(key)).ToList();
                return View(lisv);
            }

        }
        // GET: sinhviens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            return View(sinhvien);
        }
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ViewBag.maLop = new SelectList(db.lophocs, "maLop", "tenLop");
                return View();
            }
            catch (Exception e)
            {
                // Log the error message
                ViewBag.err = "Error loading form: " + e.Message;
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSV, maLop,hoTen,diachi,sdt,email,diemTX1,diemXT2,diemThi")] sinhvien sinhvien)
        {
            try
            {
                sinhvien.hinhAnh = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Content/Images/" + fileName);
                    f.SaveAs(uploadPath);
                    sinhvien.hinhAnh = fileName;
                }
                else
                {
                    ViewBag.Error = "Ảnh không được bỏ trống!";
                }

                var lastStudent = db.sinhviens.OrderByDescending(s => s.maSV).FirstOrDefault();
                int newStudentId = lastStudent != null ? lastStudent.maSV : 1;
                do
                {
                    newStudentId ++;
                    
                } while (db.sinhviens.Any(t => t.maSV == newStudentId));
                sinhvien.maSV = newStudentId;

                sinhvien.tenDangNhap = "2024SV00" + newStudentId;

                // Tạo tài khoản mới
                taikhoan tk = new taikhoan()
                {
                    tenDangNhap = sinhvien.tenDangNhap,
                    matkhau = "abc123"
                };
                db.taikhoans.Add(tk);

                // Thêm sinh viên vào cơ sở dữ liệu
                db.sinhviens.Add(sinhvien);
                db.SaveChanges();

                ViewBag.Error = "Thêm mới thành công!";
                ViewBag.maLop = new SelectList(db.lophocs, "maLop", "tenLop", sinhvien.maLop);
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = sinhvien.maSV;
                ViewBag.Error = "Có lỗi: " + e.Message;
                if (e.InnerException != null)
                ViewBag.Error += e.InnerException.ToString();
                ViewBag.maLop = new SelectList(db.lophocs, "maLop", "tenLop", sinhvien.maLop);

                return View(sinhvien);
            }
        }



        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }

            ViewBag.maLop = new SelectList(db.lophocs, "maLop", "tenLop", sinhvien.maLop);
            return View(sinhvien);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(sinhvien sv)
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
                        sv.hinhAnh = fileName;
                    }
                    else
                    {
                        sv.hinhAnh = Request.Form["hinhAnh"];
                    }

                    db.Entry(sv).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi: " + ex.Message;

            }
            ViewBag.maLop = new SelectList(db.lophocs, "maLop", "tenLop", sv.maLop);
            return View(sv);
        }
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sinhvien sinhvien = db.sinhviens.Find(id);
            if (sinhvien == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.sinhviens.Remove(sinhvien);
                db.SaveChanges();
                TempData["DeleteSuccess"] = true;
            }

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
