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
    public class SectionsController : Controller
    {
        CobinDBCon con = new CobinDBCon();
        //
        // GET: /Setup/

        public ActionResult Index(int? Id)
        {
            // Id = Project Id 
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];

            List<sPlsSVQSections_Result> rslt = con.sPlsSVQSections("s", null, Id, null, null, null, null, User.Identity.Name).ToList();
            //ViewBag.ProjectId = rslt.FirstOrDefault().Id_SVQ_Headers;
            ViewBag.ProjectId = Id;
            return View(rslt);


        }

        #region Add New Section

        public ActionResult Create(int? ProjectId)
        {
            SectionModels model = new SectionModels();
            model.HeaderId = ProjectId;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SectionModels m)
        {

            try
            {

                iPlsSVQSections_Result rslt = con.iPlsSVQSections("i", m.SectionId, m.HeaderId, m.Section_Name, m.Section_Title, m.Section_Instructions, m.Is_Required_Section, User.Identity.Name).FirstOrDefault();

                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Sections", new { id = m.HeaderId });
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

        #region Edit Section
        public ActionResult Edit(int? Id, int? ProjectId)
        {
            ClsStatic.ValidateReferral();


            sPlsSVQSections_Result rslt = con.sPlsSVQSections("s", Id, ProjectId, null, null, null, null, User.Identity.Name).FirstOrDefault();
            SectionModels model = new SectionModels();

            model.SectionId = rslt.Id;
            model.HeaderId = rslt.Id_SVQ_Headers;
            model.Section_Name = rslt.Section_Name;
            model.Section_Title = rslt.Section_Title;
            model.Section_Instructions = rslt.Section_Instructions;
            model.Is_Required_Section = rslt.Is_Required_Section;

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SectionModels m)
        {

            try
            {
                uPlsSVQSections_Result rslt = con.uPlsSVQSections("u", m.SectionId, m.HeaderId, m.Section_Name, m.Section_Title, m.Section_Instructions, m.Is_Required_Section, User.Identity.Name).FirstOrDefault();
                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Sections", new { Id = m.HeaderId });
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

        #region Delete Section

        public ActionResult Delete(int Id, int? ProjectId)
        {
            int SectionId = Id;

            ClsStatic.ValidateReferral();
            try
            {
                dPlsSVQSections_Result rslt = con.dPlsSVQSections("d", SectionId, ProjectId, null, null, null, null, User.Identity.Name).FirstOrDefault();

                TempData["Message"] = rslt.Message;
                TempData["IsPopup"] = "y";
                return RedirectToAction("Index", "Sections", new { Id = ProjectId });
            }
            catch
            {
            }


            return View();
        }
        #endregion


        
        

    }
}
