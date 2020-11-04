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
    public class sysSekolahController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /sysSekolah/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null) 
            { 
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.sysSekolahCt.ToList());
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
        // GET: /sysSekolah/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysSekolah sysSekolahDb = db.sysSekolahCt.Find(id);
            if (sysSekolahDb == null)
            {
                return HttpNotFound();
            }
            return View(sysSekolahDb);
        }

        //
        // GET: /sysSekolah/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /sysSekolah/Create
        [HttpPost]
        public ActionResult Create(sysSekolah sysSekolahDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.sysSekolahCt.Add(sysSekolahDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysSekolahDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /sysSekolah/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysSekolah sysSekolahDb = db.sysSekolahCt.Find(id);
            if (sysSekolahDb == null)
            {
                return HttpNotFound();
            }
            return View(sysSekolahDb);
        }

        //
        // POST: /sysSekolah/Edit/5
        [HttpPost]
        public ActionResult Edit(sysSekolah sysSekolahDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(sysSekolahDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysSekolahDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /sysSekolah/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysSekolah sysSekolahDb = db.sysSekolahCt.Find(id);
            if (sysSekolahDb == null)
            {
                return HttpNotFound();
            }
            return View(sysSekolahDb);
        }

        //
        // POST: /sysSekolah/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, sysSekolah per)
        {
            try
            {
                // TODO: Add delete logic here
                sysSekolah sysSekolahDb = new sysSekolah();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    sysSekolahDb = db.sysSekolahCt.Find(id);
                    if (sysSekolahDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.sysSekolahCt.Remove(sysSekolahDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysSekolahDb);
            }
            catch
            {
                return View();
            }
        }
    }
}
