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
    public class nilSikapKI1KI2Controller : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /nilSikapKI1KI2/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilSikapKI1KI2 in db.nilSikapKI1KI2Ct
                                from PerSiswa in db.perSiswaCt
                                from Person in db.personCt
                                where
                                  NilSikapKI1KI2.nis == PerSiswa.nis &&
                                  PerSiswa.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilSikapKI1KI2;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("guru"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilSikapKI1KI2 in db.nilSikapKI1KI2Ct
                                from PerGuru in db.perGuruCt
                                from Person in db.personCt
                                where
                                  NilSikapKI1KI2.nik == PerGuru.nik &&
                                  PerGuru.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilSikapKI1KI2;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.nilSikapKI1KI2Ct.ToList());
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
        // GET: /nilSikapKI1KI2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilSikapKI1KI2 nilSikapKI1KI2Db = db.nilSikapKI1KI2Ct.Find(id);
            if (nilSikapKI1KI2Db == null)
            {
                return HttpNotFound();
            }
            return View(nilSikapKI1KI2Db);
        }


        //
        // GET: /nilSikapKI1KI2/Create
        public ActionResult Create()
        {
            dropDownSekolah();
            dropDownKelas();
            dropDownSiswa ();
            dropDownMapel();
            dropDownGuru();
            return View();
        }

        //
        // POST: /nilSikapKI1KI2/Create
        [HttpPost]
        public ActionResult Create(nilSikapKI1KI2 nilSikapKI1KI2Db)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    nilSikapKI1KI2Db.KI1KI2nilaiTotal = (nilSikapKI1KI2Db.KI1KI2nilaiSatu + nilSikapKI1KI2Db.KI1KI2nilaiDua + nilSikapKI1KI2Db.KI1KI2nilaiTiga + nilSikapKI1KI2Db.KI1KI2nilaiEmpat) / 4; 
                    db.nilSikapKI1KI2Ct.Add(nilSikapKI1KI2Db);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilSikapKI1KI2Db);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /nilSikapKI1KI2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilSikapKI1KI2 nilSikapKI1KI2Db = db.nilSikapKI1KI2Ct.Find(id);
            if (nilSikapKI1KI2Db == null)
            {
                return HttpNotFound();
            }
            dropDownSekolah(nilSikapKI1KI2Db.sekolahCode);
            dropDownKelas(nilSikapKI1KI2Db.kelasCode);
            dropDownSiswa(nilSikapKI1KI2Db.nis);
            dropDownMapel(nilSikapKI1KI2Db.mapelCode);
            dropDownGuru(nilSikapKI1KI2Db.nik);
            return View(nilSikapKI1KI2Db);
        }

        //
        // POST: /nilSikapKI1KI2/Edit/5
        [HttpPost]
        public ActionResult Edit(nilSikapKI1KI2 nilSikapKI1KI2Db)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(nilSikapKI1KI2Db).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilSikapKI1KI2Db);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /nilSikapKI1KI2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilSikapKI1KI2 nilSikapKI1KI2Db = db.nilSikapKI1KI2Ct.Find(id);
            if (nilSikapKI1KI2Db == null)
            {
                return HttpNotFound();
            }
            return View(nilSikapKI1KI2Db);
        }


        //
        // POST: /nilSikapKI1KI2/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, nilSikapKI1KI2 per)
        {
            try
            {
                // TODO: Add delete logic here
                nilSikapKI1KI2 nilSikapKI1KI2Db = new nilSikapKI1KI2();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    nilSikapKI1KI2Db = db.nilSikapKI1KI2Ct.Find(id);
                    if (nilSikapKI1KI2Db == null)
                    {
                        return HttpNotFound();
                    }
                    db.nilSikapKI1KI2Ct.Remove(nilSikapKI1KI2Db);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilSikapKI1KI2Db);
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
