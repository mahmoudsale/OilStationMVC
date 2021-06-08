using OilStationMVC.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace OilStationMVC.CommonFolder
{
    public static class Common
    {
        public static class Current
        {
            public static bool IsEnglish { get; set; } = true;
            public static int BranchId { get; set; } = 1;
        }

    }
}