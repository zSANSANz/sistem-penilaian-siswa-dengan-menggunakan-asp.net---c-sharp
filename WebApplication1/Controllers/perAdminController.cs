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
    public class perAdminController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /perAdmin/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.perAdminCt.ToList());
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
        // GET: /perAdmin/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perAdmin perAdminDb = db.perAdminCt.Find(id);
            if (perAdminDb == null)
            {
                return HttpNotFound();
            }
            return View(perAdminDb);
        }

        //
        // GET: /perAdmin/Create
        public ActionResult Create()
        {
            dropDownUserName();
            return View();
        }

        //
        // POST: /perAdmin/Create
        [HttpPost]
        public ActionResult Create(perAdmin perAdminDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.perAdminCt.Add(perAdminDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perAdminDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /perAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == "")
            {
                
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perAdmin perAdminDb = db.perAdminCt.Find(id);
            if (perAdminDb == null)
            {
                return HttpNotFound();
            }
            dropDownUserName(perAdminDb.username);
            return View(perAdminDb);
        }

        //
        // POST: /perAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(perAdmin perAdminDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(perAdminDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perAdminDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /perAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perAdmin perAdminDb = db.perAdminCt.Find(id);
            if (perAdminDb == null)
            {
                return HttpNotFound();
            }
            return View(perAdminDb);
        }

        //
        // POST: /perAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, perAdmin per)
        {
            try
            {
                // TODO: Add delete logic here
                perAdmin perAdminDb = new perAdmin();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    perAdminDb = db.perAdminCt.Find(id);
                    if (perAdminDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.perAdminCt.Remove(perAdminDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perAdminDb);
            }
            catch
            {
                return View();
            }
        }
        public void dropDownUserName(object selectedUsername = null)
        {
            var linq = from d in db.personCt
                       orderby d.username
                       where
                       d.jabatan == "admin"
                       select d;
            ViewBag.username = new SelectList(linq, "username", "username", selectedUsername);
        }
    }
}
