using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class personController : Controller
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
                    return View(db.personCt.ToList());
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
        // GET: /person/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            person personDb = db.personCt.Find(id);
            if (personDb == null)
            {
                return HttpNotFound();
            }
            return View(personDb);
        }

        //
        // GET: /person/Create
        public ActionResult Create()
        {
            LoadJabatan();
            LoadPertanyaan();
            return View();
        }

        //
        // POST: /person/Create
        [HttpPost]
        public ActionResult Create(person personDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.personCt.Add(personDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(personDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /person/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoadJabatan();
            LoadPertanyaan();
            person personDb = db.personCt.Find(id);
            if (personDb == null)
            {
                return HttpNotFound();
            }

        
            return View(personDb);
        }

        //
        // POST: /person/Edit/5
        [HttpPost]
        public ActionResult Edit(person personDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(personDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(personDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /person/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            person personDb = db.personCt.Find(id);
            if (personDb == null)
            {
                return HttpNotFound();
            }
            return View(personDb);
        }

        //
        // POST: /person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, person per)
        {
            person personDb = new person();
            personDb = db.personCt.Find(id);
            db.personCt.Remove(personDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult LoadJabatan()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "Admin", Value = "admin" });
            li.Add(new SelectListItem { Text = "Peserta Didik", Value = "siswa" });
            li.Add(new SelectListItem { Text = "Tenaga Pendidik", Value = "guru" });
            li.Add(new SelectListItem { Text = "Wali Kelas", Value = "wali" });
            ViewData["jabatan"] = li;
            return View();
        }

        public ActionResult LoadPertanyaan()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem { Text = "siapa guru favorit?", Value = "siapa guru favorit?" });
            li.Add(new SelectListItem { Text = "siapa nama paman favorit?", Value = "siapa nama paman favorit?" });
            li.Add(new SelectListItem { Text = "nama hewan peliharaan pertama?", Value = "nama hewan peliharaan pertama?" });
            li.Add(new SelectListItem { Text = "dimana kota lahir anda?", Value = "dimana kota lahir anda?" });
            li.Add(new SelectListItem { Text = "apa tim sepakbola favorit anda?", Value = "apa tim sepakbola favorit anda?" });
            ViewData["pertanyaan"] = li;
            return View();
        }
    }
}
