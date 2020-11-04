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
    public class perSiswaController : Controller
    {
        private siapsContext db = new siapsContext();
            
        //
        // GET: /perSiswa/
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
        // GET: /perSiswa/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perSiswa perSiswaDb = db.perSiswaCt.Find(id);
            if (perSiswaDb == null)
            {
                return HttpNotFound();
            }
            return View(perSiswaDb);
        }


        //
        // GET: /perSiswa/Create
        public ActionResult Create()
        {
            dropDownUserName();
            return View();
        }

        //
        // POST: /perSiswa/Create
        [HttpPost]
        public ActionResult Create(perSiswa perSiswaDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.perSiswaCt.Add(perSiswaDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perSiswaDb);
            }
            catch
            {
                return View();
            }
        
        }

        //
        // GET: /perSiswa/Edit/5
        public ActionResult Edit(String id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perSiswa perSiswaDb = db.perSiswaCt.Find(id);
            if (perSiswaDb == null)
            {
                return HttpNotFound();
            }
            dropDownUserName(perSiswaDb.username);
            
            return View(perSiswaDb);
        }

        //
        // POST: /perSiswa/Edit/5
        [HttpPost]
        public ActionResult Edit(perSiswa perSiswaDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(perSiswaDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perSiswaDb);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /perSiswa/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            perSiswa perSiswaDb = db.perSiswaCt.Find(id);
            if (perSiswaDb == null)
            {
                return HttpNotFound();
            }
            return View(perSiswaDb);
        }


        //
        // POST: /perSiswa/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, perSiswa per)
        {
            try
            {
                // TODO: Add delete logic here
                perSiswa perSiswaDb = new perSiswa();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    perSiswaDb = db.perSiswaCt.Find(id);
                    if (perSiswaDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.perSiswaCt.Remove(perSiswaDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(perSiswaDb);
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
                       d.jabatan == "siswa"
                       select d;
            ViewBag.username = new SelectList(linq, "username", "username", selectedUsername);
        }
    }
}
