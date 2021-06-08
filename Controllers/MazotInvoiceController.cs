using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class MazotInvoiceController : Controller
    {
        MazotInvoiceRepository repository = new MazotInvoiceRepository();

        ChargeAreaRepository ChargeAreaRepository = new ChargeAreaRepository();

        CustomerRepository CustomerRepository = new CustomerRepository();

        AreaTransportReository areaTransportRepositort = new AreaTransportReository();

        public ActionResult Index(string JournalType)
        {
            ViewBag.JournalType = JournalType;
            return View();
        }

        public async Task<ActionResult> LoadDataAsync(string JournalType)
        {
            // Initialization.   
            JsonResult result = new JsonResult();
            try
            {
                // Initialization.   
                string search = Request.Form.GetValues("search[value]")[0];
                string draw = Request.Form.GetValues("draw")[0];
                string order = Request.Form.GetValues("order[0][column]")[0];
                string orderDir = Request.Form.GetValues("order[0][dir]")[0];
                int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
                int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

                // Loading.   

                IEnumerable<MazotInvoice> data = await repository.ListAsync(JournalType);

                // Total record count.   
                int totalRecords = data.Count();

                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search   
                    data = data.Where(p => p.ChargeCustomer.Name.ToString().ToLower().Contains(search.ToLower())
                        //|| p.Desc.ToLower().Contains(search.ToLower()) 
                        //|| p.DepositNo.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.Amount.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.dDate.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.Id.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.CustodyType.TypeName.ToString().ToLower().Contains(search.ToLower())
                        ).ToList();
                }

                // Sorting.   
                //data = this.SortByColumnWithOrder(order, orderDir, data);

                // Filter record count.   
                int recFilter = data.Count();

                // Apply pagination.   
                data = data.Skip(startRec).Take(pageSize).ToList();

                // Loading drop down lists.   
                result = this.Json(new { draw = Convert.ToInt32(draw), recordsTotal = totalRecords, recordsFiltered = recFilter, data = data }, JsonRequestBehavior.AllowGet);
                //result= Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                // Info   
                Console.Write(ex);
            }
            // Return info.   
            return result;
        }

        private List<MazotInvoice> SortByColumnWithOrder(string order, string orderDir, IEnumerable<MazotInvoice> data)
        {
            // Initialization.   
            List<MazotInvoice> lst = new List<MazotInvoice>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Id).ToList() : data.OrderBy(p => p.Id).ToList();
                        break;
                    case "1":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                    case "2":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                    case "3":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                    case "4":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                    case "5":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                    case "6":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                    default:
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Customer.Name).ToList() : data.OrderBy(p => p.Customer.Name).ToList();
                        break;
                }
            }
            catch (Exception ex)
            {
                // info.   
                Console.Write(ex);
            }
            // info.   
            return lst;
        }

        public ActionResult Create(string JournalType)
        {
            ViewBag.JournalType = JournalType;
            return View();
        }

        public ActionResult CreateInvoice()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(MazotInvoice model)
        {
            if (ModelState.IsValid)
            {
                if (await repository.AddAsync(model))
                {
                    return RedirectToAction("Index", new { JournalType = model.JournalType });
                }
                else
                {
                    ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View(model);
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> Details(int? id, string JournalType)
        //{
        //    var Invoice = await repository.ListByInvoiceNoAsync(id.ToString(), JournalType);
        //    if (Invoice.Any())
        //    {
        //        List<MazotInvoiceViewModel> lst = new List<MazotInvoiceViewModel>();
        //        foreach (var item in Invoice)
        //        {
        //            MazotInvoiceViewModel model = new MazotInvoiceViewModel
        //            {
        //                Id = item.Id,
        //                InnvoiceNo = item.InnvoiceNo,
        //                JournalType = item.JournalType,
        //                dDate = item.dDate,
        //                CompanyId = item.CompanyId,
        //                ChargeAreaId = item.ChargeAreaId,
        //                ChargeCustomerId = item.ChargeCustomerId,
        //                CarId = item.CarId,
        //                DriverId = item.DriverId,
        //                UnitPrice = item.UnitPrice,
        //                Qty = item.Qty,
        //                QtyPrice = item.QtyPrice,
        //                Tax = item.Tax,
        //                Fee = item.Fee,
        //                TotalPrice = item.TotalPrice,
        //                Desc = item.Desc,
        //                CustomerId = item.CustomerId,
        //                CustomerUnitPrice = item.CustomerUnitPrice,
        //                CustomerQty = item.CustomerQty,
        //                CustomerQtyPrice = item.CustomerQtyPrice,
        //                CustomerFee = item.CustomerFee,
        //                CustomerTotalPrice = item.CustomerTotalPrice,
        //                AreaTransportId = item.AreaTransportId,
        //                AreaTransportUnit = item.AreaTransportUnit,
        //                AreaTransportAmount = item.AreaTransportAmount,
        //                Company = item.Company,
        //                ChargeArea = item.ChargeArea,
        //                ChargeCustomer = item.ChargeCustomer,
        //                Customer = item.Customer,
        //                Car = item.Car,
        //                Driver = item.Driver,
        //                AreaTransport = item.AreaTransport,

        //            };
        //            lst.Add(model);
        //        }
        //        return View(lst);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}

        [HttpGet]
        public async Task<ActionResult> Edit(int? id, string JournalType)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var Invoice = await repository.FindByIdAsync(id.ToString(), JournalType);
            if (Invoice == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(Invoice);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, MazotInvoice model)
        {
            if (ModelState.IsValid)
            {
                if (await repository.UpdateAsync(id, model))
                {
                    return RedirectToAction("Index", new { JournalType = model.JournalType });
                }
                else
                {
                    ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View(model);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(int Id, string JournalType)
        {
            var model = repository.FindByIdAsync(Id, JournalType);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            if (await repository.DeleteByIdAsync(Id, JournalType))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
        #region helper java script file
        public async Task<JsonResult> ChargeAreaUnitriceAsync(int ChargeAreaId)
        {
            var chargeArea = await ChargeAreaRepository.FindByIdAsync(ChargeAreaId);
            return Json(chargeArea, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetMazotCustomerUnitriceAsync(int CustomerId)
        {
            var customer = await CustomerRepository.FindByIdAsync(CustomerId);
            return Json(customer, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> AreaTransportUnitriceAsync(int AreaTransportId)
        {
            var areaTransport = await areaTransportRepositort.FindByIdAsync(AreaTransportId);
            return Json(areaTransport, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}