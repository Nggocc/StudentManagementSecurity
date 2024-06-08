using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLySinhVien.Models;

namespace QuanLySinhVien.Controllers
{
    public class lophocsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: lophocs
        public ActionResult Index()
        {
            
            return View(db.lophocs.ToList());
        }

        // GET: lophocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lophoc lophoc = db.lophocs.Find(id);
            if (lophoc == null)
            {
                return HttpNotFound();
            }
            return View(lophoc);
        }
        public ActionResult GiangVien()
        {
            return View(db.giangviens.ToList());
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
