using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace COBIN.Web.Models
{
    
    public class UsersModels
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Mobile Number is not valid")]
        [Display(Name = "Mobile No.")]
        public string MobileNo { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Status { get; set; }

    }
    public class PasswordModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Old Password")]
        //  [StringLength(30, MinimumLength = 8, ErrorMessage = "Password Must be at least 8 character")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password Must be at least 8 character")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirm New Password must match")]
        public string ConfirmNewPassword { get; set; }
    }

    public class ParticipantsModels
    {
        public int Id { get; set; }

        public string Participant_Id { get; set; }

        public string Interview_Date { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string First_Name { get; set; }

        [Display(Name = "Last Name")]
        public string Last_Name { get; set; }

        [Required]
        [Display(Name = "Ward Number")]
        public string Ward_No { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Contact Number")]
        public string Contact_Number { get; set; }

        [Required]
        [Display(Name = "Interview Language")]
        public string Interview_Language { get; set; }

        [Required]
        [Display(Name = "Consent Obtained")]
        public string Consent_Obtained { get; set; }

      }
   
}