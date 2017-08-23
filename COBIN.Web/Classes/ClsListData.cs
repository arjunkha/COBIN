using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COBIN.DAL;
using COBIN.Web;
using COBIN.Classes;


namespace COBIN.Web.Classes
{
    public class ClsListData
    {
       
        public static List<SelectListItem> UserStatus = new List<SelectListItem>()
        {
          new SelectListItem() { Text="Active", Value="Active",Selected=true},
          new SelectListItem() { Text="Locked", Value="Locked",Selected=false},
        };

        public static List<SelectListItem> YesNo = new List<SelectListItem>()
        {
          new SelectListItem() { Text="Yes", Value="Yes",Selected=true},
          new SelectListItem() { Text="No", Value="No",Selected=false},
        };

        public static List<SelectListItem> ProjectList = new List<SelectListItem>()
        {
          new SelectListItem() { Text="Cobin", Value="2",Selected=true},
          new SelectListItem() { Text="Others", Value="1",Selected=false},

        };

        public static List<SelectListItem> GetProjectList(int? InstitutionId)
        {
            CobinDBCon con = new CobinDBCon();
            List<SelectListItem> ProjectList = new List<SelectListItem>();
            List<sPlsStaticData_Result> allProjectList = con.sPlsStaticData(null, "projectlist").ToList();
            //if (BlankLabel == null)
            //    BlankLabel = "--Select--";
            ProjectList.Add(new SelectListItem() { Text = "--Select--", Value = "", Selected = true });
            foreach (sPlsStaticData_Result row in allProjectList)
            {
                ProjectList.Add(new SelectListItem() { Text = row.Label, Value = row.Value.ToString(), Selected = false });
            }
            return ProjectList;
        }

        public static List<SelectListItem> GetQuestionOptions(int? Id_Option_Groups)
        {
             CobinDBCon con = new CobinDBCon();
            List<SelectListItem> OptionList = new List<SelectListItem>();
            List<sPlsOptionChoices_Result> alloption = con.sPlsOptionChoices("s",Id_Option_Groups,null).ToList();
            //if (BlankLabel == null)
            //    BlankLabel = "--Select--";
            OptionList.Add(new SelectListItem() { Text = "--Select--", Value = "", Selected = true });
            foreach (sPlsOptionChoices_Result row in alloption)
            {
                OptionList.Add(new SelectListItem() { Text = row.Option_Choice_Name, Value = row.Id_Option_Groups_Choices.ToString(), Selected = false });
            }
            return OptionList;
        }

        public static List<SelectListItem> InputTypes = new List<SelectListItem>()
        {
          new SelectListItem() { Text="DropDown", Value="1",Selected=false},
          new SelectListItem() { Text="Text", Value="2",Selected=true},
        };

        public static List<SelectListItem> Option_Group_Name = new List<SelectListItem>()
        {
          new SelectListItem() { Text="Yes-No", Value="1",Selected=false},
          new SelectListItem() { Text="Gender", Value="2",Selected=false},
          new SelectListItem() { Text="Never-Always", Value="3",Selected=false},
          new SelectListItem() { Text="Level Of Education", Value="5",Selected=false},
          new SelectListItem() { Text="Ethnic Background", Value="6",Selected=false},
          new SelectListItem() { Text="Marital Status", Value="7",Selected=false},
          new SelectListItem() { Text="Main Work", Value="8",Selected=false},
          new SelectListItem() { Text="Frequency Per Month", Value="9",Selected=false},
          new SelectListItem() { Text="Others", Value="4",Selected=true},
        };

    }
}