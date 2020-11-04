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
    public class nilUtsUasController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /nilUtsUas/
        public ActionResult Index()
        {
            if (Session["jabatan"] != null)
            {
                if (Session["jabatan"].Equals("siswa"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilUtsUas in db.nilUtsUasCt
                                from PerSiswa in db.perSiswaCt
                                from Person in db.personCt
                                where
                                  NilUtsUas.nis == PerSiswa.nis &&
                                  PerSiswa.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilUtsUas;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("guru"))
                {
                    string user = (string)System.Web.HttpContext.Current.Session["user"];
                    var siswa = from NilUtsUas in db.nilUtsUasCt
                                from PerGuru in db.perGuruCt
                                from Person in db.personCt
                                where
                                  NilUtsUas.nik == PerGuru.nik &&
                                  PerGuru.username == Person.username &&
                                  Person.username.Contains(user)
                                select NilUtsUas;
                    return View(siswa);
                }
                else if (Session["jabatan"].Equals("admin"))
                {
                    return View(db.nilUtsUasCt.ToList());
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

        
        // GET: /nilUtsUas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilUtsUas nilUtsUasDb = db.nilUtsUasCt.Find(id);
            if (nilUtsUasDb == null)
            {
                return HttpNotFound();
            }
            return View(nilUtsUasDb);
        }


        //
        // GET: /nilUtsUas/Create
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
        // POST: /nilUtsUas/Create
        [HttpPost]
        public ActionResult Create(nilUtsUas nilUtsUasDb)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    db.nilUtsUasCt.Add(nilUtsUasDb);
                    db.SaveChanges();

                        try
                        {
                       
                        var dataUpdate = (from NilAkhir in db.nilAkhirCt
                                          where
                                            NilAkhir.sekolahCode == nilUtsUasDb.sekolahCode &&
                                            NilAkhir.kelasCode == nilUtsUasDb.kelasCode &&
                                            NilAkhir.nis == nilUtsUasDb.nis &&
                                            NilAkhir.mapelCode == nilUtsUasDb.mapelCode &&
                                            NilAkhir.nik == nilUtsUasDb.nik
                                          select NilAkhir).ToList();


                        double dataNilKi3 = (from NilAkhir in db.nilPengetahuanKognitifKI3Ct
                                             where
                                              NilAkhir.sekolahCode == nilUtsUasDb.sekolahCode &&
                                              NilAkhir.kelasCode == nilUtsUasDb.kelasCode &&
                                              NilAkhir.nis == nilUtsUasDb.nis &&
                                              NilAkhir.mapelCode == nilUtsUasDb.mapelCode &&
                                              NilAkhir.nik == nilUtsUasDb.nik
                                             select NilAkhir.KI3nilaiTotal).Average();

                        double dataNilKi4 = (from NilAkhir in db.nilKetrampilanPsikomotorikKI4Ct
                                             where
                                              NilAkhir.sekolahCode == nilUtsUasDb.sekolahCode &&
                                              NilAkhir.kelasCode == nilUtsUasDb.kelasCode &&
                                              NilAkhir.nis == nilUtsUasDb.nis &&
                                              NilAkhir.mapelCode == nilUtsUasDb.mapelCode &&
                                              NilAkhir.nik == nilUtsUasDb.nik
                                             select NilAkhir.KI4nilaiTotal).Average();

                        double dataNilSikap = (from NilAkhir in db.nilSikapKI1KI2Ct
                                               where
                                                NilAkhir.sekolahCode == nilUtsUasDb.sekolahCode &&
                                                NilAkhir.kelasCode == nilUtsUasDb.kelasCode &&
                                                NilAkhir.nis == nilUtsUasDb.nis &&
                                                NilAkhir.mapelCode == nilUtsUasDb.mapelCode &&
                                                NilAkhir.nik == nilUtsUasDb.nik
                                               select NilAkhir.KI1KI2nilaiTotal).Average();

                        double dataNilUts = (from NilAkhir in db.nilUtsUasCt
                                             where
                                              NilAkhir.sekolahCode == nilUtsUasDb.sekolahCode &&
                                              NilAkhir.kelasCode == nilUtsUasDb.kelasCode &&
                                              NilAkhir.nis == nilUtsUasDb.nis &&
                                              NilAkhir.mapelCode == nilUtsUasDb.mapelCode &&
                                              NilAkhir.nik == nilUtsUasDb.nik
                                             select NilAkhir.nilaiUts).Average();

                        double dataNilUas = (from NilAkhir in db.nilUtsUasCt
                                             where
                                              NilAkhir.sekolahCode == nilUtsUasDb.sekolahCode &&
                                              NilAkhir.kelasCode == nilUtsUasDb.kelasCode &&
                                              NilAkhir.nis == nilUtsUasDb.nis &&
                                              NilAkhir.mapelCode == nilUtsUasDb.mapelCode &&
                                              NilAkhir.nik == nilUtsUasDb.nik
                                             select NilAkhir.nilaiUas).Average();


                        if (dataUpdate.Count > 0)
                        {
                            //update here

                            foreach (nilAkhir nilAkhirUpdate in dataUpdate)
                            {
                                //Field which will be update
                                nilAkhirUpdate.nilKI3 = dataNilKi3;
                                nilAkhirUpdate.nilKI4 = dataNilKi4;
                                nilAkhirUpdate.nilSikap = dataNilSikap;
                                nilAkhirUpdate.nilUts = dataNilUts;
                                nilAkhirUpdate.nilUas = dataNilUas;
                                nilAkhirUpdate.nilNaKI3 = (((dataNilKi3 * 3) + (dataNilUts * 1) + (dataNilUas * 2)) / 6);
                            }
                        }
                        else
                        {
                            //insert here
                            nilAkhir nilAkhirInsert = new nilAkhir();
                            nilAkhirInsert.sekolahCode = nilUtsUasDb.sekolahCode;
                            nilAkhirInsert.kelasCode = nilUtsUasDb.kelasCode;
                            nilAkhirInsert.nis = nilUtsUasDb.nis;
                            nilAkhirInsert.mapelCode = nilUtsUasDb.mapelCode;
                            nilAkhirInsert.nik = nilUtsUasDb.nik;
                            nilAkhirInsert.nilKI3 = dataNilKi3;
                            nilAkhirInsert.nilKI4 = dataNilKi4;
                            nilAkhirInsert.nilSikap = dataNilSikap;
                            nilAkhirInsert.nilUts = dataNilUts;
                            nilAkhirInsert.nilUas = dataNilUas;
                            nilAkhirInsert.nilNaKI3 = (((dataNilKi3 * 3) + (dataNilUts * 1) + (dataNilUas * 2)) / 6);
                            db.nilAkhirCt.Add(nilAkhirInsert);
                        }
                    
                    }
                    catch 
                    {
                        //log the error
                        return RedirectToAction("Index");
                    }     

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilUtsUasDb);
            }
            catch
            {
                return View();
            }

        }

        //
        // GET: /nilUtsUas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilUtsUas nilUtsUasDb = db.nilUtsUasCt.Find(id);
            if (nilUtsUasDb == null)
            {
                return HttpNotFound();
            }
            dropDownSekolah(nilUtsUasDb.sekolahCode);
            dropDownKelas(nilUtsUasDb.kelasCode);
            dropDownSiswa(nilUtsUasDb.nis);
            dropDownMapel(nilUtsUasDb.mapelCode);
            dropDownGuru(nilUtsUasDb.nik);
            return View(nilUtsUasDb);
        }

        //
        // POST: /nilUtsUas/Edit/5
        [HttpPost]
        public ActionResult Edit(nilUtsUas nilUtsUasDb)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    db.Entry(nilUtsUasDb).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilUtsUasDb);
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /nilUtsUas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            nilUtsUas nilUtsUasDb = db.nilUtsUasCt.Find(id);
            if (nilUtsUasDb == null)
            {
                return HttpNotFound();
            }
            return View(nilUtsUasDb);
        }


        //
        // POST: /nilUtsUas/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, nilUtsUas per)
        {
            try
            {
                // TODO: Add delete logic here
                nilUtsUas nilUtsUasDb = new nilUtsUas();
                if (ModelState.IsValid)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    nilUtsUasDb = db.nilUtsUasCt.Find(id);
                    if (nilUtsUasDb == null)
                    {
                        return HttpNotFound();
                    }
                    db.nilUtsUasCt.Remove(nilUtsUasDb);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nilUtsUasDb);
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
