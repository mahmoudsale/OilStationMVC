using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.CommonFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class CustomerFromApiController : Controller
    {
        // GET: CustomerFromApi
        public ActionResult Index()
        {
            ServiceRepository serviceObj = new ServiceRepository();
  //HttpResponseMessage response = serviceObj.GetResponse("api/Cust");
            //HttpResponseMessage response = serviceObj.GetResponse("/posts");
            //response.EnsureSuccessStatusCode();
            //List<Models.Customer> products = response.Content.ReadAsAsync<List<Models.Customer>>().Result;
            //ViewBag.Title = "All Products";
            //return View(products);

          
            HttpResponseMessage response = serviceObj.GetResponse("/posts");
            response.EnsureSuccessStatusCode();
            List<Post> products = response.Content.ReadAsAsync<List<Post>>().Result;
            ViewBag.Title = "All Products";
            return View(products);
        }

        public ActionResult EditProduct(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/Cust?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Customer products = response.Content.ReadAsAsync<Models.Customer>().Result;
            ViewBag.Title = "All Products";
            return View(products);
        }
        //[HttpPost]  
        public ActionResult Update(Models.Customer product)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/showroom/UpdateProduct", product);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllProducts");
        }
        public ActionResult Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/showroom/GetProduct?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Customer products = response.Content.ReadAsAsync<Models.Customer>().Result;
            ViewBag.Title = "All Products";
            return View(products);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Customer product)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PostResponse("api/showroom/InsertProduct", product);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllProducts");
        }
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/showroom/DeleteProduct?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllProducts");
        }


    }

    public class Post
    {
        public string User { get; set; }
        public string Id { get; set; }
        public string Title{ get; set; }
        public string Body { get; set; }
    }
}