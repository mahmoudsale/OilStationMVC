using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.CommonFolder;
using OilStationMVC.OilStationService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    public class LoginController : Controller
    {
        //[CallActionLogFiles]
        //[Authorize]
       
        public ActionResult Login()
        {
           
            Models.Login login = new Models.Login();
            return View(login);
        }
        public void Exception()
        {
            throw new Exception("ssssssss");
        }
        [HttpPost]
        public ActionResult Login(Models.Login login)
        {
            DataBase.DB dB = new DataBase.DB();
            if (Licenses.class_License.CheckLicense())
            {
                string EncryptedPassword = Common.Class_Configration.Encrypt(login.Password);
                string sql = "select * from users where nId='" + login.UserName + "' and sPassword='" + EncryptedPassword + "'";
                DataTable dt = dB.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    Common.Class_Configration.BranchId = "1";
                    Common.Class_Configration.Licensedmodule = "(0,2,30,31,32)";
                    UserIdentity.UserId = Common.MW.CInt(dt.Rows[0]["nId"]);
                    UserIdentity.UserGroupId = Common.MW.Cstr(dt.Rows[0]["nGroup"]);
                    Common.Class_Configration.BranchId = "1";
                    if (login.Language == "A")
                    {
                        Session["LoginedUserName"] = Common.MW.Cstr(dt.Rows[0]["sName"]);
                        SetCulture(true);
                    }
                    else
                    {
                        Session["LoginedUserName"] = Common.MW.Cstr(dt.Rows[0]["sName"]);
                        SetCulture(false);
                    }
                    if (UserIdentity.UserGroupId == ((int)Roles.Administrators).ToString())
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                   else if (UserIdentity.UserGroupId == ((int)Roles.Employees).ToString())
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                  
                    ModelState.AddModelError(string.Empty, "Please Enter Valid Credintials");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }




        }

        private void SetCulture(bool IsArabic)
        {
            HttpCookie CultureInfo = new HttpCookie("CultureInfo");
            Response.Cookies.Add(CultureInfo);
            HttpCookie IsArabicLang = new HttpCookie("IsArabicLang");
            Response.Cookies.Add(IsArabicLang);

            if (!string.IsNullOrEmpty(Common.MW.Cstr(IsArabic)))
            {
                if (IsArabic)
                {
                    Common.Current.IsEnglish = false;
                    CultureInfo.Value = "ar-JO";
                    IsArabicLang.Value = "True";
                }
                else
                {
                    Common.Current.IsEnglish = true;
                    CultureInfo.Value = "en";
                    IsArabicLang.Value = "false";
                }
            }
            else
            {
                Common.Current.IsEnglish = true;
                CultureInfo.Value = "en";
                IsArabicLang.Value = "false";
            }

        }



        public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoverPassword(Models.Login login)
        {
            return View("Login");
        }


        [HttpGet]
        public ActionResult Logout()
        {
            Session["LoginedUserName"] = null;
            Session.Clear();
            Response.Cookies.Clear();
            return View("Login");
        }


    }
}