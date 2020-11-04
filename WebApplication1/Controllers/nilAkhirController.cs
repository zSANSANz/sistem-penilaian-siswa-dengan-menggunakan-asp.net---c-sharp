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
    public class nilAkhirController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /nilAkhir/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilAkhir in db.nilAkhirCt
                                from PerSiswa in db.perSiswaCt
                                from Person in db.personCt
                                where
                                  NilAkhir.nis == PerSiswa.nis &&
                                  PerSiswa.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilAkhir;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("guru"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilAkhir in db.nilAkhirCt
                                from PerGuru in db.perGuruCt
                                from Person in db.personCt
                                where
                                  NilAkhir.nik == PerGuru.nik &&
                                  PerGuru.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilAkhir;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.nilAkhirCt.ToList());
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
        // GET: /nilAkhir/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilAkhir nilAkhirDb = db.nilAkhirCt.Find(id);
            if (nilAkhirDb == null)
            {
                return HttpNotFound();
            }
            return View(nilAkhirDb);
        }


        //
        // GET: /nilAkhir/Create
        public ActionResult Create()
        {
            dropDownSekolah();
            dropDownKelas();
            dropDownSiswa();
            dropDownMapel();
            dropDownGuru();
            return View();
        }

        //
        // POST: /nilAkhir/Create
        [HttpPost]
        public ActionResult Create(nilAkhir nilAkhirDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.nilAkhirCt.Add(nilAkhirDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilAkhirDb);
            }
            catch
            {
                return View();
            }

        }

        //
        // GET: /nilAkhir/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilAkhir nilAkhirDb = db.nilAkhirCt.Find(id);
            if (nilAkhirDb == null)
            {
                return HttpNotFound();
            }
            dropDownSekolah(nilAkhirDb.sekolahCode);
            dropDownKelas(nilAkhirDb.kelasCode);
            dropDownSiswa(nilAkhirDb.nis);
            dropDownMapel(nilAkhirDb.mapelCode);
            dropDownGuru(nilAkhirDb.nik);
            return View(nilAkhirDb);
        }

        //
        // POST: /nilAkhir/Edit/5
        [HttpPost]
        public ActionResult Edit(nilAkhir nilAkhirDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(nilAkhirDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilAkhirDb);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /nilAkhir/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilAkhir nilAkhirDb = db.nilAkhirCt.Find(id);
            if (nilAkhirDb == null)
            {
                return HttpNotFound();
            }
            return View(nilAkhirDb);
        }


        //
        // POST: /nilAkhir/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, nilAkhir per)
        {
            try
            {
                // TODO: Add delete logic here
                nilAkhir nilAkhirDb = new nilAkhir();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    nilAkhirDb = db.nilAkhirCt.Find(id);
                    if (nilAkhirDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.nilAkhirCt.Remove(nilAkhirDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilAkhirDb);
            }
            catch
            {
                return View();
            }
        }

        public void dropDownSekolah(object selectedSekolah = null)
        {
            var linq = from d in db.sysSekolahCt
                       orderby d.sekolahCode
                       select d;
            ViewBag.sekolahCode = new SelectList(linq, "sekolahCode", "namaSekolah", selectedSekolah);
        }

        public void dropDownKelas(object selectedKelas = null)
        {
            var linq = from d in db.sysKelasCt
                       orderby d.kelasCode
                       select d;
            ViewBag.kelasCode = new SelectList(linq, "kelasCode", "kelasCode", selectedKelas);
        }

        public void dropDownMapel(object selectedMapel = null)
        {
            var linq = from d in db.sysMapelCt
                       orderby d.mapelCode
                       select d;
            ViewBag.mapelCode = new SelectList(linq, "mapelCode", "namaMapel", selectedMapel);
        }

        public void dropDownSiswa(object selectedSiswa = null)
        {
            var linq = from d in db.perSiswaCt
                       orderby d.nis
                       select d;
            ViewBag.nis = new SelectList(linq, "nis", "namaSiswa", selectedSiswa);
        }

        public void dropDownGuru(object selectedGuru = null)
        {
            var linq = from d in db.perGuruCt
                       orderby d.nik
                       select d;
            ViewBag.nik = new SelectList(linq, "nik", "namaGuru", selectedGuru);
        }
    }
}
