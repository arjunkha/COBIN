using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using COBIN.Classes;
using COBIN.DAL;
using COBIN.Web;

namespace COBIN.Web.Models
{
    
    public class InterviewModels
    {
        [Required]
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }

        [Required]
        [Display(Name = "Participant Id")]
        public string Participant_Id { get; set; }

        //public string Question_Name { get; set; }
        //public string Question_Subtext { get; set; }
        //public string Question_Code { get; set; }

        public List<sPlsInterviewQuestions_Result> TblRecord { get; set; }

        //public string Section_Name { get; set; }
        //public string Section_Title { get; set; }
        //public string Section_Instructions { get; set; }


        public string Session_Id { get; set; }
        public string Section_id { get; set; }
        public int Id_SVQ_Questions { get; set; }
        public int? Id_Option_Groups { get; set; }
        public int? Id_Option_Groups_Choices { get; set; }
        public int? Measurement_Unit { get; set; }
        
        // fields for participant validation
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        
        [Display(Name = "Ward Number")]
        public string Ward_No { get; set; }

 
        [Display(Name = "Address")]
        public string Address { get; set; }

}
   
   
}