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
    public class ParticipantsController : Controller
    {
        CobinDBCon con = new CobinDBCon();
        //
        // GET: /Participants /
        #region List All Participants
        public ActionResult Index()
        {
            ClsStatic.ValidateReferral();
            ViewBag.Message = TempData["Message"];
            ViewBag.IsPopup = TempData["IsPopup"];


            List<sPlsParticipants_Result> rslt = con.sPlsParticipants("s", null, null, null, null, null, null, null, null,null, User.Identity.Name).ToList();
            return View(rslt);

        }
        #endregion

        #region Add New Participants

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParticipantsModels m)
        {

            try
            {

                iPlsParticipants_Result rslt = con.iPlsParticipants("i",null,m.Participant_Id,m.First_Name,m.Last_Name,m.Ward_No,m.Address,m.Contact_Number,m.Interview_Language,m.Consent_Obtained,User.Identity.Name).FirstOrDefault();

                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Participants");
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

        #region Edit Participants
        public ActionResult Edit(int? Id)
        {
            ClsStatic.ValidateReferral();

            sPlsParticipants_Result rslt = con.sPlsParticipants("s", Id,null, null, null, null, null, null, null, null, User.Identity.Name).ToList().FirstOrDefault();
            ParticipantsModels model = new ParticipantsModels();

            model.Id = rslt.Id;
            model.Participant_Id = rslt.ParticipantId;
            model.First_Name = rslt.First_Name;
            model.Last_Name = rslt.Last_Name;
            model.Address = rslt.Address;
            model.Ward_No = rslt.Ward_No;
            model.Contact_Number = rslt.Contact_Number;
            model.Consent_Obtained = rslt.Consent_Obtained;
            model.Interview_Language = rslt.Interview_Language;

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ParticipantsModels m)
        {

            try
            {
                uPlsParticipants_Result rslt = con.uPlsParticipants("u",m.Id,m.Participant_Id,m.First_Name,m.Last_Name,m.Ward_No,m.Address,m.Contact_Number,m.Interview_Language,m.Consent_Obtained,User.Identity.Name).FirstOrDefault();
                if (rslt.Code == "000")
                {
                    TempData["Message"] = rslt.Message;
                    TempData["IsPopup"] = "y";
                    return RedirectToAction("Index", "Participants");
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

        #region Delete Participant

        public ActionResult Delete(int? id)
        {
            ClsStatic.ValidateReferral();

            dPlsParticipants_Result rslt = con.dPlsParticipants("d", id, null,null, null, null, null, null, null, null, User.Identity.Name).FirstOrDefault();


            TempData["Message"] = rslt.Message;
            TempData["IsPopup"] = "y";
            return RedirectToAction("Index", "Participants");

        }
        #endregion

    }
}
