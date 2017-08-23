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
    [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class InterviewController : Controller
    {
        CobinDBCon con = new CobinDBCon();   
        //
        // GET: /Setup/

        
        public ActionResult Index()
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            //List<sPlsSVQHeaders_Result> rslt = con.sPlsSVQHeaders("s", null, null, null, null, null, null, User.Identity.Name).ToList();
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InterviewModels model)
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];
            int ProjectId = model.ProjectId;
            sPlsParticipants_Result rslt = con.sPlsParticipants("s",null,model.Participant_Id, null, null, null, null, null, null, null, User.Identity.Name).ToList().FirstOrDefault();

            if (rslt != null)
            {
                model.ProjectId = ProjectId;
                model.Participant_Id = rslt.ParticipantId;
                model.First_Name = rslt.First_Name;
                model.Last_Name = rslt.Last_Name;
                model.Address = rslt.Address;
                model.Ward_No = rslt.Ward_No;
            }
            return View(model);


        }

        //public ActionResult IntvSessionSetup(int ProjectId, string ParticipantId)
        //{
        //    ClsStatic.ValidateReferral();

        //    InterviewModels model = new InterviewModels();
        //    model.ProjectId = ProjectId;
        //    model.Participant_Id = ParticipantId;
            
        //    return RedirectToAction("IntvSession", "Interview");

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IntvSession(InterviewModels m)
        {
            ClsStatic.ValidateReferral();
            
            // Summary of Project disaply 
            // section name with total number of questions

            List<sPlsInterviewQuestions_Result> rslt;

            if (m.Session_Id == null)
            {
                rslt = con.sPlsInterviewQuestions("s", m.ProjectId, m.Participant_Id, m.Session_Id, m.Section_id, m.Id_SVQ_Questions, m.Id_Option_Groups_Choices, m.Measurement_Unit, User.Identity.Name).ToList();
            }
            else
            {
                rslt = con.sPlsInterviewQuestions("i", m.ProjectId, m.Participant_Id, m.Session_Id, m.Section_id, m.Id_SVQ_Questions, m.Id_Option_Groups_Choices, m.Measurement_Unit, User.Identity.Name).ToList();
            }

            sPlsInterviewQuestions_Result rslt_row = rslt.FirstOrDefault();

            
           InterviewModels model = new InterviewModels();



               model.TblRecord = rslt;
               model.Section_id = rslt_row.Id_SVQ_Sections.ToString();
               model.Id_SVQ_Questions = rslt_row.Id_SVQ_Questions;
               model.Session_Id = rslt_row.Session_Id;
               model.Id_Option_Groups = rslt_row.Id_Option_Groups;
               model.Measurement_Unit = rslt_row.Measurement_Unit;
         
            

            //m.Section_Name = rslt.Section_Name;
            //m.Section_Title = rslt.Section_Title;
            //m.Section_Instructions = rslt.Section_Instructions;

            //m.Question_Name = rslt.Question_Name;
            //m.Question_Subtext = rslt.Question_Subtext;
            //m.Question_Code = rslt.Question_Code;


            return View(model);


        }

        //public ActionResult ProjectList()
        //{
        //    ClsStatic.ValidateReferral();
        //    ViewBag.Message = TempData["Message"];
        //    ViewBag.IsPopup = TempData["IsPopup"];


        //    List<sPlsSVQHeaders_Result> rslt = con.sPlsSVQHeaders("s", null, null, null,null, null, null,User.Identity.Name).ToList();
        //    return View(rslt);

            
        //}

        //public ActionResult Sections(int? Id)
        //{
        //    ClsStatic.ValidateReferral();
        //    ViewBag.Message = TempData["Message"];
        //    ViewBag.IsPopup = TempData["IsPopup"];


        //    List<sPlsSVQSections_Result> rslt = con.sPlsSVQSections("s", null, Id, null, null, null, null, User.Identity.Name).ToList();
        //    return View(rslt);


        //}

        //public ActionResult Questions(int? Id)
        //{
        //    ClsStatic.ValidateReferral();
        //    ViewBag.Message = TempData["Message"];
        //    ViewBag.IsPopup = TempData["IsPopup"];


        //    List<sPlsSVQQuestions_Result> rslt = con.sPlsSVQQuestions("s", null, Id,null,null, null, null, null, null, User.Identity.Name).ToList();
        //    return View(rslt);


        //}

    }
}
