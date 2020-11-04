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
    public class nilPengetahuanKognitifKI3Controller : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /nilPengetahuanKognitifKI3/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilPengetahuanKognitifKI3 in db.nilPengetahuanKognitifKI3Ct
                                from PerSiswa in db.perSiswaCt
                                from Person in db.personCt
                                where
                                  NilPengetahuanKognitifKI3.nis == PerSiswa.nis &&
                                  PerSiswa.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilPengetahuanKognitifKI3;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("guru"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilPengetahuanKognitifKI3 in db.nilPengetahuanKognitifKI3Ct
                                from PerGuru in db.perGuruCt
                                from Person in db.personCt
                                where
                                  NilPengetahuanKognitifKI3.nik == PerGuru.nik &&
                                  PerGuru.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilPengetahuanKognitifKI3;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.nilPengetahuanKognitifKI3Ct.ToList());
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
        // GET: /nilPengetahuanKognitifKI3/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilPengetahuanKognitifKI3 nilPengetahuanKognitifKI3Db = db.nilPengetahuanKognitifKI3Ct.Find(id);
            if (nilPengetahuanKognitifKI3Db == null)
            {
                return HttpNotFound();
            }
            return View(nilPengetahuanKognitifKI3Db);
        }


        //
        // GET: /nilPengetahuanKognitifKI3/Create
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
        // POST: /nilPengetahuanKognitifKI3/Create
        [HttpPost]
        public ActionResult Create(nilPengetahuanKognitifKI3 nilPengetahuanKognitifKI3Db)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    nilPengetahuanKognitifKI3Db.KI3nilaiTotal = (nilPengetahuanKognitifKI3Db.KI3nilaiSatu + nilPengetahuanKognitifKI3Db.KI3nilaiDua + nilPengetahuanKognitifKI3Db.KI3nilaiTiga + nilPengetahuanKognitifKI3Db.KI3nilaiEmpat) / 4;
                    db.nilPengetahuanKognitifKI3Ct.Add(nilPengetahuanKognitifKI3Db);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilPengetahuanKognitifKI3Db);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /nilPengetahuanKognitifKI3/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilPengetahuanKognitifKI3 nilPengetahuanKognitifKI3Db = db.nilPengetahuanKognitifKI3Ct.Find(id);
            if (nilPengetahuanKognitifKI3Db == null)
            {
                return HttpNotFound();
            }
            dropDownSekolah(nilPengetahuanKognitifKI3Db.sekolahCode);
            dropDownKelas(nilPengetahuanKognitifKI3Db.kelasCode);
            dropDownSiswa(nilPengetahuanKognitifKI3Db.nis);
            dropDownMapel(nilPengetahuanKognitifKI3Db.mapelCode);
            dropDownGuru(nilPengetahuanKognitifKI3Db.nik);
            return View(nilPengetahuanKognitifKI3Db);
        }

        //
        // POST: /nilPengetahuanKognitifKI3/Edit/5
        [HttpPost]
        public ActionResult Edit(nilPengetahuanKognitifKI3 nilPengetahuanKognitifKI3Db)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(nilPengetahuanKognitifKI3Db).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilPengetahuanKognitifKI3Db);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /nilPengetahuanKognitifKI3/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilPengetahuanKognitifKI3 nilPengetahuanKognitifKI3Db = db.nilPengetahuanKognitifKI3Ct.Find(id);
            if (nilPengetahuanKognitifKI3Db == null)
            {
                return HttpNotFound();
            }
            return View(nilPengetahuanKognitifKI3Db);
        }


        //
        // POST: /nilPengetahuanKognitifKI3/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, nilPengetahuanKognitifKI3 per)
        {
            try
            {
                // TODO: Add delete logic here
                nilPengetahuanKognitifKI3 nilPengetahuanKognitifKI3Db = new nilPengetahuanKognitifKI3();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    nilPengetahuanKognitifKI3Db = db.nilPengetahuanKognitifKI3Ct.Find(id);
                    if (nilPengetahuanKognitifKI3Db == null)
                    {
                        return HttpNotFound();
                    }
                    db.nilPengetahuanKognitifKI3Ct.Remove(nilPengetahuanKognitifKI3Db);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilPengetahuanKognitifKI3Db);
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
            ViewBag.nis = new SelectList(linq, "nis", "nis", selectedSiswa);
        }

        public void dropDownGuru(object selectedGuru = null)
        {
            var linq = from d in db.perGuruCt
                       orderby d.nik
                       select d;
            ViewBag.nik = new SelectList(linq, "nik", "nik", selectedGuru);
        }
    }
}
