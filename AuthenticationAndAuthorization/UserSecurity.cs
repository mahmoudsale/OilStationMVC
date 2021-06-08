using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilStationMVC.AuthenticationAndAuthorization
{
    public class UserSecurity
    {
        public static bool Login(string UserName, string Password)
        {
            DataBase.DB dB = new DataBase.DB();
            string sql = "select count (*) from users where nId='" + UserName + "' and sPassword='" + Password + "'";
            //string sql = "select count (*) from Customer where AuthorizedUserName='" + UserName + "' and AuthorizedUserPassword='" + Password + "'";
            int IsFound = Common.MW.CInt(dB.GetValue(sql));
            if (IsFound > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}