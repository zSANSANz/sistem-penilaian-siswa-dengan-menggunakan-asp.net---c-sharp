using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class raporController : Controller
    {
        private siapsContext db = new siapsContext();
        
        //
        // GET: /rapor/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.perSiswaCt.ToList());
                }
                else if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from p in db.perSiswaCt
                                where
                                   p.username.Contains(user)
                                select p;
                    return View(siswa);
                }
                else
                {
                    return RedirectToAction("LogOn", "Account");
                }
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            }
        }

        //
        // GET: /rapor/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perSiswa perSiswaDb = db.perSiswaCt.Find(id);
            if (perSiswaDb == null)
            {
                return HttpNotFound();
            }

            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    var kelas = from t in db.nilSikapKI1KI2Ct
                                where
                                  t.nis == "2602659072"
                                select new
                                {
                                    t.kelasCode
                                };
                    return View(perSiswaDb);
                }
                else if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from p in db.perSiswaCt
                                where
                                   p.username.Contains(user)
                                select p;
                    return View(perSiswaDb);
                }
                else
                {
                    return RedirectToAction("LogOn", "Account");
                }
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            } 
        }

        //
        // GET: /rapor/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /rapor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /rapor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /rapor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /rapor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /rapor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
