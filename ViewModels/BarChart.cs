using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OilStationMVC.ViewModels
{
    public class BarChart
    {

        public BarChart()
        {
            datasets = new Dataset[2];
            //Labels = new string[] { "January", "February", "March", "April", "May", "June", "July", "Aughts", "Septemper", "October", "November", "December" };
            //Datasets = new Dataset[]{ new Dataset
            //{
            //    Label = "Digital Goods",
            //    BackgroundColor = "rgba(60,141,188,0.9)",
            //    BorderColor = "rgba(60,141,188,0.8)",
            //    PointRadius = false,
            //    PointColor = "#3b8bba",
            //    PointStrokeColor = "rgba(60,141,188,1)",
            //    PointHighlightFill = "#fff",
            //    PointHighlightStroke = "rgba(60,141,188,1)",
            //    Data = new decimal[] { 28, 48, 40, 19, 86, 27, 90 ,88,77,10,11,12}
            //},
            //new Dataset
            //{
            //    Label = "xxx Goods",
            //    BackgroundColor = "rgba(60,141,188,0.9)",
            //    BorderColor = "rgba(60,141,188,0.8)",
            //    PointRadius = false,
            //    PointColor = "#3b8bba",
            //    PointStrokeColor = "rgba(60,141,188,1)",
            //    PointHighlightFill = "#fff",
            //    PointHighlightStroke = "rgba(60,141,188,1)",
            //    Data = new decimal[] { 28, 48, 40, 19, 86, 27, 90,58,45,14,25,20 }
            //} };
        }
        [JsonProperty("labels")]
        public string[] labels { get; set; }

        [JsonProperty("datasets")]
        public Dataset[] datasets { get; set; }
    }

    public class Dataset
    {
         
        public Dataset()
        {
            backgroundColor = "rgba(60,141,188,0.9)";
            borderColor = "rgba(60,141,188,0.8)";
            pointRadius = false;
            pointColor = "#3b8bba";
            pointStrokeColor = "rgba(60,141,188,1)";
            pointHighlightFill = "#fff";
            pointHighlightStroke = "rgba(60,141,188,1)";
            data = new decimal[] { 28, 48, 40, 19, 86, 27, 90, 58, 45, 14, 25, 20 };
        }
        [JsonProperty("label")]
        public string label { get; set; } 

        [JsonProperty("data")]
        public decimal[] data { get; set; }

        [JsonProperty("backgroundColor")]
        public string backgroundColor { get; set; }

        [JsonProperty("borderColor")]
        public string borderColor { get; set; }

        [JsonProperty("pointRadius")]
        public bool pointRadius { get; set; }

        [JsonProperty("pointColor")]
        public string pointColor { get; set; }

        [JsonProperty("pointStrokeColor")]
        public string pointStrokeColor { get; set; }

        [JsonProperty("pointHighlightFill")]
        public string pointHighlightFill { get; set; }

        [JsonProperty("pointHighlightStroke")]
        public string pointHighlightStroke { get; set; }
    }
}
