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
    public class QuestionsController : Controller
    {
        CobinDBCon con = new CobinDBCon();   
        //
        // GET: /Setup/
        #region List All Questions
        public ActionResult Index(int? Id)
        {
             // Here Id = SectionId
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            List<sPlsSVQQuestions_Result> rslt = con.sPlsSVQQuestions("s", null, Id,null,null, null, null, null, null, User.Identity.Name).ToList();
            
            ViewBag.SectionId = Id;
            return View(rslt);

        }
        #endregion

        #region Add New Question

        public ActionResult Create(int? Id)
        {
            QuestionModels model = new QuestionModels();
            model.SectionId = Id;
            return View(model);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionModels m)
        {
            
            try
            {
                iPlsSVQQuestions_Result rslt = con.iPlsSVQQuestions("i",null, m.SectionId,m.Id_Input_Types,m.Id_Option_Categories,m.Question_Name,m.Question_Subtext,m.Question_Code,m.Measurement_Unit, User.Identity.Name).FirstOrDefault();
                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Questions", new { id = m.SectionId });
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

        #region Edit Question
        public ActionResult Edit(int? Id )
        {
            ClsStatic.ValidateReferral();

            sPlsSVQQuestions_Result rslt = con.sPlsSVQQuestions("s", Id, null, null, null, null, null, null, null, User.Identity.Name).ToList().FirstOrDefault();
            QuestionModels model = new QuestionModels();

            model.QuestionId = rslt.Id;
            model.SectionId = rslt.Id_SVQ_Sections;
            model.Question_Name = rslt.Question_Name;
            model.Question_Subtext = rslt.Question_Subtext;
            model.Question_Code = rslt.Question_Code;
            model.Measurement_Unit = rslt.Measurement_Unit.ToString();
            model.Id_Option_Categories = rslt.Id_Option_Groups;
            model.Id_Input_Types = rslt.Id_Input_Types;

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionModels m)
        {
            
            try
            {
                uPlsSVQQuestions_Result rslt = con.uPlsSVQQuestions("u",m.QuestionId,m.SectionId,m.Id_Input_Types,m.Id_Option_Categories,m.Question_Name,m.Question_Subtext,m.Question_Code,m.Measurement_Unit,User.Identity.Name).FirstOrDefault();
                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Questions", new { Id = m.SectionId });
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

        #region Delete Question

        public ActionResult Delete(int? id , int? SectionId)
        {
            ClsStatic.ValidateReferral();
            try
            {
                dPlsSVQQuestions_Result rslt = con.dPlsSVQQuestions("d",id, SectionId,null, null, null, null, null, null, User.Identity.Name).FirstOrDefault();

                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Questions", new { Id = SectionId });
                }

            }
            catch
            {
                ModelState.AddModelError("", "Techinical Error Occurred While Processing Your Request");
            }

            return View();
        }
        #endregion

   
    }
}
        

  