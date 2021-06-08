using OilStationMVC.CommonFolder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.CommonFolder
{

        
    public static class Lookups
    {
        public static List<SelectListItem>  CompanyLst()
        {
            string sql = "";
            if(Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from OilCompany";
            }
            else
            {
                sql = "select nId as Value,sName as Text from OilCompany";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> StationLst()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sNameEng as Text from Stations";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Stations";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> CarLst()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from Cars";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Cars";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> DriverLst()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sNameEng as Text from Driver";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Driver";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem>StockLst()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sNameEng as Text from Stocks";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Stocks";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> BalanceState()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nStateCode as Value,sStateNameEng as Text from Acc_State";
            }
            else
            {
                sql = "select nStateCode as Value,sStateName as Text from Acc_State";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> CustomerType()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from OilCustomerType";
            }
            else
            {
                sql = "select nId as Value,sName as Text from OilCustomerType";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> Customers()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from Customer";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Customer";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> Users()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from Users";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Users";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> Acc_State()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nStateCode as Value,sStateNameEng as Text from Acc_State";
            }
            else
            {
                sql = "select nStateCode as Value,sStateName as Text from Acc_State";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> RolesLst()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sNameEng as Text from UserGroup";
            }
            else
            {
                sql = "select nId as Value,sName as Text from Acc_State";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> CostCenterList(int SubledgerType)
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from vuw_Oilstation_Subledgers where SubledgerType="+ SubledgerType + "";
            }
            else
            {
                sql = "select nId as Value,sName as Text from vuw_Oilstation_Subledgers where SubledgerType=" + SubledgerType + "";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> ChargeAreaLst()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from ChargeArea";
            }
            else
            {
                sql = "select nId as Value,sName as Text from ChargeArea";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }

        public static List<SelectListItem> AreaTransportLST()
        {
            string sql = "";
            if (Common.Current.IsEnglish)
            {
                sql = "select nId as Value,sName as Text from AreaTransport";
            }
            else
            {
                sql = "select nId as Value,sName as Text from AreaTransport";
            }
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable(sql);
            List<SelectListItem> list = CommonFolder.Mapper.ToList<SelectListItem>(dt);
            return list;
        }
    }
}