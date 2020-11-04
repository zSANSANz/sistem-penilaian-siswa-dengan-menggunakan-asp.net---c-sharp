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
    public class sysMapelController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /sysMapel/

        //public ActionResult RemoteValidation(sysMapel pd)
        //{
        //    return View(pd);
        //}

        //public JsonResult CheckForDuplication(string FirstName)
        //{
        //    var data = db.sysMapelCt.Where(p => p.mapelCode.Equals(FirstName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        //    if (data != null)
        //    {
        //        return Json("Sorry, this name already exists", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.sysMapelCt.ToList());
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
        // GET: /sysMapel/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysMapel sysMapelDb = db.sysMapelCt.Find(id);
            if (sysMapelDb == null)
            {
                return HttpNotFound();
            }
            return View(sysMapelDb);
        }

        //
        // GET: /sysMapel/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /sysMapel/Create
        [HttpPost]
        public ActionResult Create(sysMapel sysMapelDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.sysMapelCt.Add(sysMapelDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysMapelDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /sysMapel/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysMapel sysMapelDb = db.sysMapelCt.Find(id);
            if (sysMapelDb == null)
            {
                return HttpNotFound();
            }
            return View(sysMapelDb);
        }

        //
        // POST: /sysMapel/Edit/5
        [HttpPost]
        public ActionResult Edit(sysMapel sysMapelDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(sysMapelDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysMapelDb);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /sysMapel/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sysMapel sysMapelDb = db.sysMapelCt.Find(id);
            if (sysMapelDb == null)
            {
                return HttpNotFound();
            }
            return View(sysMapelDb);
        }

        //
        // POST: /sysMapel/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, sysMapel per)
        {
            try
            {
                // TODO: Add delete logic here
                sysMapel sysMapelDb = new sysMapel();
                if (ModelState.IsValid)
                {
                    if (id == "")
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    sysMapelDb = db.sysMapelCt.Find(id);
                    if (sysMapelDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.sysMapelCt.Remove(sysMapelDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sysMapelDb);
            }
            catch
            {
                return View();
            }
        }
    }
}
