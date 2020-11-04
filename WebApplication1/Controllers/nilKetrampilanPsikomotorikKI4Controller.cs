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
    public class nilKetrampilanPsikomotorikKI4Controller : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /nilKetrampilanPsikomotorikKI4/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilKetrampilanPsikomotorikKI4 in db.nilKetrampilanPsikomotorikKI4Ct
                                from PerSiswa in db.perSiswaCt
                                from Person in db.personCt
                                where
                                  NilKetrampilanPsikomotorikKI4.nis == PerSiswa.nis &&
                                  PerSiswa.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilKetrampilanPsikomotorikKI4;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("guru"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilKetrampilanPsikomotorikKI4 in db.nilKetrampilanPsikomotorikKI4Ct
                                from PerGuru in db.perGuruCt
                                from Person in db.personCt
                                where
                                  NilKetrampilanPsikomotorikKI4.nik == PerGuru.nik &&
                                  PerGuru.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilKetrampilanPsikomotorikKI4;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.nilKetrampilanPsikomotorikKI4Ct.ToList());
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
        // GET: /nilKetrampilanPsikomotorikKI4/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilKetrampilanPsikomotorikKI4 nilKetrampilanPsikomotorikKI4Db = db.nilKetrampilanPsikomotorikKI4Ct.Find(id);
            if (nilKetrampilanPsikomotorikKI4Db == null)
            {
                return HttpNotFound();
            }
            return View(nilKetrampilanPsikomotorikKI4Db);
        }


        //
        // GET: /nilKetrampilanPsikomotorikKI4/Create
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
        // POST: /nilKetrampilanPsikomotorikKI4/Create
        [HttpPost]
        public ActionResult Create(nilKetrampilanPsikomotorikKI4 nilKetrampilanPsikomotorikKI4Db)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    nilKetrampilanPsikomotorikKI4Db.KI4nilaiTotal = (nilKetrampilanPsikomotorikKI4Db.KI4nilaiSatu + nilKetrampilanPsikomotorikKI4Db.KI4nilaiDua + nilKetrampilanPsikomotorikKI4Db.KI4nilaiTiga + nilKetrampilanPsikomotorikKI4Db.KI4nilaiEmpat) / 4; 
                    db.nilKetrampilanPsikomotorikKI4Ct.Add(nilKetrampilanPsikomotorikKI4Db);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilKetrampilanPsikomotorikKI4Db);
            }
            catch
            {
                return View();
            }

        }

        //
        // GET: /nilKetrampilanPsikomotorikKI4/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilKetrampilanPsikomotorikKI4 nilKetrampilanPsikomotorikKI4Db = db.nilKetrampilanPsikomotorikKI4Ct.Find(id);
            if (nilKetrampilanPsikomotorikKI4Db == null)
            {
                return HttpNotFound();
            }
            dropDownSekolah(nilKetrampilanPsikomotorikKI4Db.sekolahCode);
            dropDownKelas(nilKetrampilanPsikomotorikKI4Db.kelasCode);
            dropDownSiswa(nilKetrampilanPsikomotorikKI4Db.nis);
            dropDownMapel(nilKetrampilanPsikomotorikKI4Db.mapelCode);
            dropDownGuru(nilKetrampilanPsikomotorikKI4Db.nik);
            return View(nilKetrampilanPsikomotorikKI4Db);
        }

        //
        // POST: /nilKetrampilanPsikomotorikKI4/Edit/5
        [HttpPost]
        public ActionResult Edit(nilKetrampilanPsikomotorikKI4 nilKetrampilanPsikomotorikKI4Db)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(nilKetrampilanPsikomotorikKI4Db).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilKetrampilanPsikomotorikKI4Db);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /nilKetrampilanPsikomotorikKI4/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilKetrampilanPsikomotorikKI4 nilKetrampilanPsikomotorikKI4Db = db.nilKetrampilanPsikomotorikKI4Ct.Find(id);
            if (nilKetrampilanPsikomotorikKI4Db == null)
            {
                return HttpNotFound();
            }
            return View(nilKetrampilanPsikomotorikKI4Db);
        }


        //
        // POST: /nilKetrampilanPsikomotorikKI4/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, nilKetrampilanPsikomotorikKI4 per)
        {
            try
            {
                // TODO: Add delete logic here
                nilKetrampilanPsikomotorikKI4 nilKetrampilanPsikomotorikKI4Db = new nilKetrampilanPsikomotorikKI4();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    nilKetrampilanPsikomotorikKI4Db = db.nilKetrampilanPsikomotorikKI4Ct.Find(id);
                    if (nilKetrampilanPsikomotorikKI4Db == null)
                    {
                        return HttpNotFound();
                    }
                    db.nilKetrampilanPsikomotorikKI4Ct.Remove(nilKetrampilanPsikomotorikKI4Db);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilKetrampilanPsikomotorikKI4Db);
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
