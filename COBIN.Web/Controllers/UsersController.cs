using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COBIN.Classes;
using COBIN.Web.Models;
using COBIN.DAL;
using System.Data.Entity;
using System.Data;

namespace COBIN.Web.Controllers
{
    public class UsersController : Controller
    {
        CobinDBCon con = new CobinDBCon();
        //
        // GET: /User/
        #region List All User
        public ActionResult Index()
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            List<sPlsUsers_Result> rslt = con.sPlsUsers("s", null, null, null, null, null, null, null, null, User.Identity.Name).ToList();
            return View(rslt);

        }
        #endregion

        #region Add New User

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsersModels m)
        {

            try
            {

                iPlsUsers_Result rslt = con.iPlsUsers("i", null, m.UserName, m.Password, null, m.FullName, m.MobileNo, m.Email, m.Status, User.Identity.Name).FirstOrDefault();

                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Error! Code[" + rslt.Code + "]: " + rslt.Message);
                }
            }

            catch
            {
                ModelState.AddModelError("", "Techinical Error Occurred While Processing Your Request");
            }
            return View(m);
        }
        #endregion

        #region Edit User
        public ActionResult Edit(int? Id)
        {
            ClsStatic.ValidateReferral();

            sPlsUsers_Result rslt = con.sPlsUsers("s", Id, null, null, null, null, null, null, null, User.Identity.Name).ToList().FirstOrDefault();
            UsersModels model = new UsersModels();

            model.UserId = rslt.UserId;
            model.UserName = rslt.UserName;
            model.FullName = rslt.FullName;
            model.Email = rslt.EmailAddress;
            model.MobileNo = rslt.MobileNumber;
            model.Status = rslt.Status;

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsersModels m)
        {

            try
            {
                uPlsUsers_Result rslt = con.uPlsUsers("u", m.UserId, m.UserName, null, null, m.FullName, m.MobileNo, m.Email, m.Status, User.Identity.Name).FirstOrDefault();
                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Error! Code[" + rslt.Code + "]: " + rslt.Message);
                }
            }

            catch
            {
                ModelState.AddModelError("", "Techinical Error Occurred While Processing Your Request");
            }
            return View(m);
        }
        #endregion

        #region Change Password
        public ActionResult ChangePassword(int? id)
        {

            ClsStatic.ValidateReferral();
            string UserName = null;
            PasswordModel model = new PasswordModel();
            if (id == null)
            {
                UserName = User.Identity.Name;
            }
            sPlsUsers_Result rslt = con.sPlsUsers("s", id, UserName, null, null, null, null, null, null, User.Identity.Name).FirstOrDefault();
            model.UserName = rslt.UserName;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(PasswordModel m)
        {

            try
            {

                pPlsUsers_Result rslt = con.pPlsUsers("p", null, m.UserName, m.OldPassword, m.NewPassword, null, null, null, null, User.Identity.Name).FirstOrDefault();

                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";

                    return RedirectToAction("Index", "Users");

                }

                else
                {
                    ModelState.AddModelError("", "Error! Code[" + rslt.Code + "]: " + rslt.Message);
                }

            }
            catch
            {
                ModelState.AddModelError("", "Techinical Error Occurred While Processing Your Request");
            }
            return View(m);

        }

        #endregion

        #region Delete User

        public ActionResult Delete(int? id)
        {
            ClsStatic.ValidateReferral();
   
                dPlsUsers_Result rslt = con.dPlsUsers("d", id, null, null, null, null, null, null, null, User.Identity.Name).FirstOrDefault();


                TempData["Message"] = rslt.Message;
                TempData["IsPopup"] = "y";
                return RedirectToAction("Index", "Users");

             }
        #endregion

        
    }
}
