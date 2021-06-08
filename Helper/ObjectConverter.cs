using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OilStationMVC.Helper
{
    public static class ObjectConverter
    {
        public static int CInt(object p)
        {
            try
            {
                if (p.ToString() == "")
                {
                    return 0;
                }
                return Convert.ToInt32(p);
            }
            catch (Exception)
            {
                return 0;
            }

        }
        public static long CLng(object p)
        {
            try
            {
                if (p.ToString() == "")
                {
                    return 0;
                }
                return Convert.ToInt64(p);
            }
            catch (Exception)
            {
                return 0;
            }

        }



        public static double CDbl(object p)
        {
            try
            {
                if (p.ToString() == "")
                {
                    return 0;
                }
                return Convert.ToDouble(p);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static string Cstr(object p)
        {
            string value = "";
            try
            {

                if (p != null)
                {
                    value = p.ToString();

                }
            }
            catch (Exception)
            {
                return "";
            }
            return value;

        }



        public static decimal CDec(object p)
        {
            try
            {
                if (p.ToString() == "")
                {
                    return 0;
                }
                return Convert.ToDecimal(p);
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public static bool CBool(object p)
        {
            try
            {
                if (p.ToString() == "")
                {
                    return false;
                }
                return Convert.ToBoolean(p);
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static DateTime CDate(object p)
        {
            try
            {
                return Convert.ToDateTime(p);
            }
            catch (Exception)
            {
                return Convert.ToDateTime("1900/01/01");
            }

        }

    }
}