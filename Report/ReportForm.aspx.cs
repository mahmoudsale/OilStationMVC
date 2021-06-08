using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OilStationMVC.Report
{
    public partial class ReportForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            LoadReport();
        }

        private void LoadReport()
       {  
            string ReportName = (string)Session["ReportName"];
            DataSet ds = Session["DataSet"] as DataSet;
            ViewReport(ds, ReportName);
        }
        public void ViewReport(DataSet ds, string ReportName)
        {  if(ds!=null)
            {
                ReportDocument rep = new ReportDocument();
                if (ds.Tables[0].Rows.Count >= 1)
                {
                    string ReportBath = "";
                   
                    if (ReportName.Contains(".rpt"))
                    {
                        ReportBath = Server.MapPath("~/Report/" + ReportName + "");
                        //ReportBath = @"E:\msmk\TFS Collecton\ERB_Project\MainProject\bin\Debug\OilStationReport\" + ReportName + "";
                    } 
                    else
                    {
                        ReportBath = Server.MapPath("~/Report/" + ReportName + ".rpt");
                        //ReportBath = @"E:\msmk\TFS Collecton\ERB_Project\MainProject\bin\Debug\OilStationReport\" + ReportName + "+.rpt";
                    }
                    rep.Load(ReportBath);
                    rep.SetDataSource(ds.Tables[0]);
                    CrystalDecisions.Web.CrystalReportViewer report = new CrystalDecisions.Web.CrystalReportViewer();
                    report.Width = 600;
                    report.Height = 400;
                    MainContaner.Controls.Add(report);
                    report.ReportSource = rep;
                    report.RefreshReport();
                }
            }
           
            else
            {
                //return null;
            }
        }
    }
}