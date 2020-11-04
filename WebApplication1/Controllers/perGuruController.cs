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
    public class perGuruController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /perGuru/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.perGuruCt.ToList());
                }
                else if (Session["jabatan"].Equals("guru"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from p in db.perGuruCt
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
        // GET: /perGuru/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perGuru perGuruDb = db.perGuruCt.Find(id);
            if (perGuruDb == null)
            {
                return HttpNotFound();
            }
            dropDownUserName(perGuruDb.username);
            
            return View(perGuruDb);
        }


        //
        // GET: /perGuru/Create
        public ActionResult Create()
        {
            dropDownUserName();
            return View();
        }

        //
        // POST: /perGuru/Create
        [HttpPost]
        public ActionResult Create(perGuru perGuruDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.perGuruCt.Add(perGuruDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perGuruDb);
            }
            catch
            {
                return View();
            }

        }

        //
        // GET: /perGuru/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perGuru perGuruDb = db.perGuruCt.Find(id);
            if (perGuruDb == null)
            {
                return HttpNotFound();
            }
            return View(perGuruDb);
        }

        //
        // POST: /perGuru/Edit/5
        [HttpPost]
        public ActionResult Edit(perGuru perGuruDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(perGuruDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perGuruDb);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /perGuru/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perGuru perGuruDb = db.perGuruCt.Find(id);
            if (perGuruDb == null)
            {
                return HttpNotFound();
            }
            return View(perGuruDb);
        }


        //
        // POST: /perGuru/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, perGuru per)
        {
            try
            {
                // TODO: Add delete logic here
                perGuru perGuruDb = new perGuru();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    perGuruDb = db.perGuruCt.Find(id);
                    if (perGuruDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.perGuruCt.Remove(perGuruDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perGuruDb);
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
                       d.jabatan == "guru"
                       select d;
            ViewBag.username = new SelectList(linq, "username", "username", selectedUsername);
        }
    }
}
