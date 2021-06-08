using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class PieChart
    {
        //public PieChart()
        //{
        //    Datasets = new PieChartDataset[1];
        //}
        //public string[] labels { get; set; }
        //public PieChartDataset[] Datasets { get; set; } 
        public PieChart()
        {
           datasets = new pieChartDataset[1];
        }
        public string[] labels { get; set; }
        public pieChartDataset[] datasets { get; set; }
    }
    public class pieChartDataset
    {
        public pieChartDataset()
        {
            backgroundColor = new string[] { "#f56954", "#00a65a", "#f39c12", "#00c0ef" };
        }
        public decimal[] data { get; set; } 
        public string[] backgroundColor { get; set; }

    }
}
