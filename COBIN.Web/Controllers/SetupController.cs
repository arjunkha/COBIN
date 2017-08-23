using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COBIN.DAL;
using COBIN.Classes;
using COBIN.Web.Models;

namespace COBIN.Web.Controllers
{
    public class SetupController : Controller
    {
        CobinDBCon con = new CobinDBCon();   
        //
        // GET: /Setup/

        public ActionResult ProjectList()
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            List<sPlsSVQHeaders_Result> rslt = con.sPlsSVQHeaders("s", null, null, null,null, null, null,User.Identity.Name).ToList();
            return View(rslt);

            
        }

        #region Add New Project

        public ActionResult Create(int? InstitutionId)
        {
            HeaderModels model = new HeaderModels();
            model.InstitutionId = InstitutionId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HeaderModels m)
        {

            try
            {

                iPlsSVQHeaders_Result rslt = con.iPlsSVQHeaders("i",m.HeaderId,m.InstitutionId,m.Survey_Name,m.Survey_Code,m.Survey_Instructions,m.MiscellaneousInfo,User.Identity.Name).FirstOrDefault();

                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("ProjectList", "Setup", new { id = m.InstitutionId });
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

        public ActionResult Sections(int? Id)
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            List<sPlsSVQSections_Result> rslt = con.sPlsSVQSections("s", null, Id, null, null, null, null, User.Identity.Name).ToList();
            return View(rslt);


        }

        public ActionResult Questions(int? Id)
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            List<sPlsSVQQuestions_Result> rslt = con.sPlsSVQQuestions("s", null, Id,null,null, null, null, null, null, User.Identity.Name).ToList();
            return View(rslt);


        }

    }
}
