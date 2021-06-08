using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace OilStationMVC.Controllers.WebAPI
{
    public class CustController : ApiController
    {
        ////Get ALl
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {
        //        DataBase.DB db = new DataBase.DB();
        //        DataTable dt = db.GetDataTable("select * from customer");
        //        List<Models.Customer> lst = Common.Mapper.ToList<Models.Customer>(dt);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            var message = Request.CreateResponse(HttpStatusCode.OK, lst);

        //            return message;
        //        }
        //        else
        //        {
        //            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent,"No Data Found");
        //            return message;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //        return message;
        //    }


        //}

        //Get Data For Only Authorized Users
        //[AuthenticationAndAuthorization.BasicAuthentication]
        //[Authorize(Roles = "Admin")]
        public HttpResponseMessage Get()
        {
            try
            {
                DataBase.DB db = new DataBase.DB();
                DataTable dt = db.GetDataTable("select * from customer");
                List<Models.Customer> lst = Common.Mapper.ToList<Models.Customer>(dt);
                if (lst != null && lst.Count > 0)
                {
                    var message = Request.CreateResponse(HttpStatusCode.OK, lst);

                    return message;
                }
                else
                {
                    var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Data Found");
                    return message;
                }

            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return message;
            }


        }
        //[Authorize]
        //public HttpResponseMessage Get()
        //{
        //    try
        //    {
        //        DataBase.DB db = new DataBase.DB();
        //        DataTable dt = db.GetDataTable("select * from customer");
        //        List<Models.Customer> lst = Common.Mapper.ToList<Models.Customer>(dt);
        //        if (lst != null && lst.Count > 0)
        //        {
        //            var message = Request.CreateResponse(HttpStatusCode.OK, lst);

        //            return message;
        //        }
        //        else
        //        {
        //            var message = Request.CreateErrorResponse(HttpStatusCode.NoContent, "No Data Found");
        //            return message;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //        return message;
        //    }


        //}
        //Get By Id
        public HttpResponseMessage Get(int id)
        {
            try
            {
                DataBase.DB db = new DataBase.DB();
                DataTable dt = db.GetDataTable("select * from customer where nId=" + id + "");
                List<Models.Customer> lst = Common.Mapper.ToList<Models.Customer>(dt);
                if (lst != null && lst.Count > 0)
                {
                    var message = Request.CreateResponse(HttpStatusCode.OK, lst);

                    return message;
                }
                else
                {
                    var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found with Id" + id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return message;
            }

        }
        public HttpResponseMessage Post([FromBody]Models.Customer customer)
        {
            try
            {
                //save operation
                DataBase.DB db = new DataBase.DB();
                db.ExecutSQL("insert into customer(nId,sName) values(" + customer.nId + ",'" + customer.sName + "')");
                var message = Request.CreateResponse(HttpStatusCode.Created, customer);
                message.Headers.Location = new Uri(Request.RequestUri + "/" + customer.nId);
                return message;
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return message;
            }
        }
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCus(int id)
        {
            try
            {
                DataBase.DB db = new DataBase.DB();
                DataTable dt = db.GetDataTable("select * from customer where nId=" + id + "");
                List<Models.Customer> lst = Common.Mapper.ToList<Models.Customer>(dt);
                if (lst != null && lst.Count > 0)
                {
                    string sql = "delete from customer where nId=" + id + "";
                    db.ExecutSQL(sql);
                    var message = Request.CreateResponse(HttpStatusCode.OK);
                    return message;
                }
                else
                {
                    var message = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found with Id" + id.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return message;
            }
            
        }

        public List<Models.Customer> List()
        {
            List<Models.Customer> AreaTransport = new List<Models.Customer>();
            DataBase.DB db = new DataBase.DB();
            DataTable dt = db.GetDataTable("select * from customer ");
            List<Models.Customer> list = Common.Mapper.ToList<Models.Customer>(dt);
            return list;
        }
        [HttpGet]
        public JsonResult<List<Models.Customer>> GetAllProducts()
        {
            return Json(List());
        }
        [HttpGet]
        public JsonResult<Models.Customer> GetProduct(int id)
        {


            return Json(List().Find(x => x.nId.Equals(id)));
        }
        [HttpPost]
        public bool InsertProduct(Models.Customer product)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //EntityMapper<Models.Product, DataAccessLayer.Product> mapObj = new EntityMapper<Models.Product, DataAccessLayer.Product>();
                //DataAccessLayer.Product productObj = new DataAccessLayer.Product();
                //productObj = mapObj.Translate(product);
                //status = DAL.InsertProduct(productObj);
            }
            return status;

        }
        [HttpPut]
        public bool UpdateProduct(Models.Customer product)
        {
            //EntityMapper<Models.Product, DataAccessLayer.Product> mapObj = new EntityMapper<Models.Product, DataAccessLayer.Product>();
            //DataAccessLayer.Product productObj = new DataAccessLayer.Product();
            //productObj = mapObj.Translate(product);
            //var status = DAL.UpdateProduct(productObj);
            //return status;
            return true;

        }
        [HttpDelete]
        public bool DeleteProduct(int id)
        {
            //var status = DAL.DeleteProduct(id);
            //return status;
            return true;
        }
    }
}
