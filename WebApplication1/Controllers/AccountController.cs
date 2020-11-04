using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebApplication1.Models;
using WebApplication1.DAL;
using System.Web.Helpers;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private siapsContext db = new siapsContext();
        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }
        
        //
        // POST: /Account/LogOn
        [HttpPost]
        public ActionResult LogOn(person per)
        {
            try
            {
                var v = db.personCt.Where(a => a.username.Equals(per.username) && a.password.Equals(per.password)).FirstOrDefault();

                if (v != null)
                {
                    Session["user"] = v.username.ToString();
                    Session["jabatan"] = v.jabatan.ToString();
                    
                    string authId = Guid.NewGuid().ToString();

                    Session["AuthID"] = authId;

                    var cookie = new HttpCookie("AuthID");
                    cookie.Value = authId;
                    Response.Cookies.Add(cookie);
                    return RedirectToAction("loginSukses");
                }
                return RedirectToAction("loginGagal");
            }
            catch
            {
                return View();
                
            }
            
        }

        public ActionResult logOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult logOut(person per)
        {
            Session.Clear();
            return RedirectToAction("index", "home");
        }

        public ActionResult loginGagal()
        {
            return View();
        }

        public ActionResult loginSukses()
        {
            try
            {
                if (Request.Cookies["AuthID"].Value == Session["AuthID"].ToString())
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return RedirectToAction("Index");
                
            }
        }


        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }


        #region Status Codes
        
        #endregion
    }
}
