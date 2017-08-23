using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Configuration;

namespace COBIN.Classes
{

    public class HeaderDetail
    {
        public string InstitutionName { get; set; }
        public string InstitutionAddress { get; set; }
        public string InstitutionPhone { get; set; }
        public string InstitutionEmail { get; set; }
        public string InstitutionLogo { get; set; }
    }

    public class ClsStatic

    {
        public static string GetIPAddress()
        {
            try
            {
                if (System.ServiceModel.OperationContext.Current != null)
                {
                    var endpoint = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    return endpoint.Address;
                }
                if (System.Web.HttpContext.Current != null)
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                        return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + " via " +
                            HttpContext.Current.Request.UserHostAddress;
                    else
                        return HttpContext.Current.Request.UserHostAddress;
                }
            }
            catch { }
            return "Unknown";
        }

        public static void ValidateReferral()
        {
            string URL = ConfigurationManager.AppSettings["LoginURL"].ToString();
            if (System.Web.HttpContext.Current.Request.UrlReferrer == null)
            {
                System.Web.HttpContext.Current.Response.Redirect(URL);
            }
            else
            {
                if (System.Web.HttpContext.Current.Request.Url.Host != System.Web.HttpContext.Current.Request.UrlReferrer.Host.ToString())
                {
                    System.Web.HttpContext.Current.Response.Redirect(URL);
                }
            }
        }
        public static string GetPageTitle()
        {
            string Title = ConfigurationManager.AppSettings["InstitutionName"].ToString();
            return Title;
        }

        public static HeaderDetail GetHeaderData(string InstitutionId = null, string UserName = null)
        {
           // RemitDBCon con = new RemitDBCon();
            HeaderDetail HeaderData = new HeaderDetail();
           // PlsGetHeader_Result header = con.PlsGetHeader(branchid, UserName).FirstOrDefault();

                HeaderData.InstitutionName = ConfigurationManager.AppSettings["InstitutionName"].ToString();
                HeaderData.InstitutionAddress = ConfigurationManager.AppSettings["InstitutionAddress"].ToString();
                HeaderData.InstitutionPhone = ConfigurationManager.AppSettings["InstitutionPhone"].ToString();
                HeaderData.InstitutionEmail = ConfigurationManager.AppSettings["InstitutionEmail"].ToString();
                HeaderData.InstitutionLogo = null;
                return HeaderData;
            
        }
    }
}
