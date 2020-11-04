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
    public class sysKelasController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /sysKelas/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.sysKelasCt.ToList());
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
        // GET: /sysKelas/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysKelas sysKelasDb = db.sysKelasCt.Find(id);
            if (sysKelasDb == null)
            {
                return HttpNotFound();
            }
            return View(sysKelasDb);
        }

        //
        // GET: /sysKelas/Create
        public ActionResult Create()
        {
            dropDownWaliKelas();
            return View();
        }

        //
        // POST: /sysKelas/Create
        [HttpPost]
        public ActionResult Create(sysKelas sysKelasDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.sysKelasCt.Add(sysKelasDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysKelasDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /sysKelas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysKelas sysKelasDb = db.sysKelasCt.Find(id);
            if (sysKelasDb == null)
            {
                return HttpNotFound();
            }
            dropDownWaliKelas(sysKelasDb.nik); 
            return View(sysKelasDb);
        }

        //
        // POST: /sysKelas/Edit/5
        [HttpPost]
        public ActionResult Edit(sysKelas sysKelasDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(sysKelasDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysKelasDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /sysKelas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysKelas sysKelasDb = db.sysKelasCt.Find(id);
            if (sysKelasDb == null)
            {
                return HttpNotFound();
            }
            dropDownWaliKelas(sysKelasDb.nik);
            return View(sysKelasDb);
        }

        //
        // POST: /sysKelas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, sysKelas per)
        {
            try
            {
                // TODO: Add delete logic here
                sysKelas sysKelasDb = new sysKelas();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    sysKelasDb = db.sysKelasCt.Find(id);
                    if (sysKelasDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.sysKelasCt.Remove(sysKelasDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysKelasDb);
            }
            catch
            {
                return View();
            }
        }

        public void dropDownWaliKelas(object selectedGuru = null)
        {
            var linq = from d in db.perWaliKelasCt 
                       orderby d.nik
                       select d;
            ViewBag.nik = new SelectList(linq, "nik", "nik", selectedGuru);
        }
    }
}
