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
    public class perWaliKelasController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /person/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.perWaliKelasCt.ToList());
                }
                if (Session["jabatan"].Equals("wali"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from p in db.perWaliKelasCt
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
        // GET: /perWaliKelas/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perWaliKelas perWaliKelasDb = db.perWaliKelasCt.Find(id);
            if (perWaliKelasDb == null)
            {
                return HttpNotFound();
            }
            return View(perWaliKelasDb);
        }


        //
        // GET: /perWaliKelas/Create
        public ActionResult Create()
        {
            dropDownUserName();
            return View();
        }

        //
        // POST: /perWaliKelas/Create
        [HttpPost]
        public ActionResult Create(perWaliKelas perWaliKelasDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.perWaliKelasCt.Add(perWaliKelasDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perWaliKelasDb);
            }
            catch
            {
                return View();
            }

        }

        //
        // GET: /perWaliKelas/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perWaliKelas perWaliKelasDb = db.perWaliKelasCt.Find(id);
            if (perWaliKelasDb == null)
            {
                return HttpNotFound();
            }
            dropDownUserName(perWaliKelasDb.username);
            
            return View(perWaliKelasDb);
        }

        //
        // POST: /perWaliKelas/Edit/5
        [HttpPost]
        public ActionResult Edit(perWaliKelas perWaliKelasDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(perWaliKelasDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perWaliKelasDb);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /perWaliKelas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perWaliKelas perWaliKelasDb = db.perWaliKelasCt.Find(id);
            if (perWaliKelasDb == null)
            {
                return HttpNotFound();
            }
            return View(perWaliKelasDb);
        }


        //
        // POST: /perWaliKelas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, perWaliKelas per)
        {
            try
            {
                // TODO: Add delete logic here
                perWaliKelas perWaliKelasDb = new perWaliKelas();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    perWaliKelasDb = db.perWaliKelasCt.Find(id);
                    if (perWaliKelasDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.perWaliKelasCt.Remove(perWaliKelasDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perWaliKelasDb);
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
                       d.jabatan == "wali"
                       select d;
            ViewBag.username = new SelectList(linq, "username", "username", selectedUsername);
        }
    }
}
