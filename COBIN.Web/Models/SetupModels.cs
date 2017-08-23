using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace COBIN.Web.Models
{
    
    public class QuestionModels
    {
        
        public int? QuestionId { get; set; }
        public int? SectionId { get; set; }

        [Display(Name = "Option Categories")]
        public int? Id_Option_Categories { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string Question_Name { get; set; }

        [Display(Name = "Question SubText")]
        public string Question_Subtext { get; set; }

        [Required]
        [Display(Name = "Question Code")]
        public string Question_Code { get; set; }

        [Display(Name = "Measurement Unit")]
        public string Measurement_Unit { get; set; }

        [Display(Name = "Input Types")]
        public int? Id_Input_Types { get; set; }

     }

    public class SectionModels
    {
        
        public int? SectionId { get; set; }
        public int? HeaderId { get; set; }

        [Required]
        [Display(Name = "Section Name")]
        public string Section_Name { get; set; }

        [Required]
        [Display(Name = "Section Title")]
        public string Section_Title { get; set; }

        
        [Display(Name = "Section Instruction")]
        public string Section_Instructions { get; set; }

        [Display(Name = "Is Required")]
        public string Is_Required_Section { get; set; }

     }

    public class HeaderModels
    {

        public int? InstitutionId { get; set; }
        public int? HeaderId { get; set; }

        [Required]
        [Display(Name = "Survey Name")]
        public string Survey_Name { get; set; }


        [Display(Name = "Survey Code")]
        public string Survey_Code { get; set; }

        [Display(Name = "Survey Instructions")]
        public string Survey_Instructions { get; set; }

        [Display(Name = "Miscellaneous Info")]
        public string MiscellaneousInfo { get; set; }


    }

    
   
   
}