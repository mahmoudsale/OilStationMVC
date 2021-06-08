using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    public class CommonControllerController : Controller
    {
        // GET: CommonController
        public string GetProductPricesPerStock(int StockId)
        {
            DataBase.DB db = new DataBase.DB();
            DataTable dtWillConvertToJson = new DataTable();
            dtWillConvertToJson.Columns.Add("SolarBuyPrice", typeof(decimal));
            dtWillConvertToJson.Columns.Add("SolarSalePrice", typeof(decimal));
            dtWillConvertToJson.Columns.Add("Oil80BuyPrice", typeof(decimal));
            dtWillConvertToJson.Columns.Add("Oil80SalePrie", typeof(decimal));
            dtWillConvertToJson.Columns.Add("Oil92BuyPrice", typeof(decimal));
            dtWillConvertToJson.Columns.Add("Oil92SalePrice", typeof(decimal));
            dtWillConvertToJson.Columns.Add("Oil95BuyPrice", typeof(decimal));
            dtWillConvertToJson.Columns.Add("Oil95SalePrie", typeof(decimal));
            DataTable dt = db.GetDataTable("select * from OilProdcutPrice where nStockId=" + StockId + "");
            if (dt.Rows.Count > 0)
            {
                decimal SolarBuyPrice = 0;
                decimal SolarSalePrice = 0;
                decimal Oil80BuyPrice = 0;
                decimal Oil80SalePrice = 0;
                decimal Oil92BuyPrice = 0;
                decimal Oil92SalePrice = 0;
                decimal Oil95BuyPrice = 0;
                decimal Oil95SalePrie = 0;
                DataRow[] solar = dt.Select("nProductId=1");
                if (solar != null && solar.Length > 0)
                {
                    SolarBuyPrice = Common.MW.CDec(solar[0]["nBuyrice"]);
                    SolarSalePrice = Common.MW.CDec(solar[0]["nSalePrice"]);
                }
                DataRow[] Oil80 = dt.Select("nProductId=2");
                if (solar != null && solar.Length > 0)
                {
                    Oil80BuyPrice = Common.MW.CDec(solar[0]["nBuyrice"]);
                    Oil80SalePrice = Common.MW.CDec(solar[0]["nSalePrice"]);
                }
                DataRow[] Oil92 = dt.Select("nProductId=3");
                if (solar != null && solar.Length > 0)
                {
                    Oil92BuyPrice = Common.MW.CDec(solar[0]["nBuyrice"]);
                    Oil92SalePrice = Common.MW.CDec(solar[0]["nSalePrice"]);
                }
                DataRow[] Oil95 = dt.Select("nProductId=4");
                if (Oil95 != null && Oil95.Length > 0)
                {
                    Oil95BuyPrice = Common.MW.CDec(Oil95[0]["nBuyrice"]);
                    Oil95SalePrie = Common.MW.CDec(Oil95[0]["nSalePrice"]);
                }

                dtWillConvertToJson.Rows.Add(SolarBuyPrice, SolarSalePrice, Oil80BuyPrice, Oil80SalePrice, Oil92BuyPrice, Oil92SalePrice, Oil95BuyPrice, Oil95SalePrie);
            }
            string Json = DataTableToJSONWithJSONNet(dtWillConvertToJson);
            return Json;
        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public string xxx()
        {
            return "ddddddddd";
        }


        public string GetCustCredit(string Id)
        {
            string CusCredit = "0";
            if (string.IsNullOrEmpty(Id))
            {
                return CusCredit;
            }
            else
            {
                return "53";
            }
        }
    }
}