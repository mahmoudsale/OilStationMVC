using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class InnvoiceCustomerController : Controller
    {
        CustomerInvoiceRepository repository = new CustomerInvoiceRepository();
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> LoadDataAsync()
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

                IEnumerable<CustomerInvoiceViewModel> data = await repository.ListAsync();

                // Total record count.   
                int totalRecords = data.Count();

                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search   
                    data = data.Where(p => p.Customer.Name.ToString().ToLower().Contains(search.ToLower())
                        //|| p.Desc.ToLower().Contains(search.ToLower()) 
                        //|| p.DepositNo.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.Amount.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.dDate.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.Id.ToString().ToLower().Contains(search.ToLower()) 
                        //|| p.CustodyType.TypeName.ToString().ToLower().Contains(search.ToLower())
                        ).ToList();
                }

                // Sorting.   
                data = this.SortByColumnWithOrder(order, orderDir, data);

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

        private List<CustomerInvoiceViewModel> SortByColumnWithOrder(string order, string orderDir, IEnumerable<CustomerInvoiceViewModel> data)
        {
            // Initialization.   
            List<CustomerInvoiceViewModel> lst = new List<CustomerInvoiceViewModel>();
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


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerInvoiceViewModel model)
        {

            if (ModelState.IsValid)
            {
                CustomerInvoice invoice = new CustomerInvoice
                {
                    dDate = model.dDate,
                    CustomerId = model.CustomerId,
                    SolarQTY = model.SolarQTY,
                    Oil80QTY = model.Oil80QTY,
                    Oil92QTY = model.Oil92QTY,
                    Oil95QTY = model.Oil95QTY,
                    SolarUnitPrice = model.SolarUnitPrice,
                    Oil80UnitPrice = model.Oil80UnitPrice,
                    Oil92UnitPrice = model.Oil92UnitPrice,
                    Oil95UnitPrice = model.Oil95UnitPrice,
                    SolarTotalPrice = model.SolarTotalPrice,
                    Oil80TotalPrice = model.Oil80TotalPrice,
                    Oil92TotalPrice = model.Oil92TotalPrice,
                    Oil95TotalPrice = model.Oil95TotalPrice,
                    TotalPrice = model.TotalPrice,
                    Desc = model.Desc,

                };
                if (await repository.AddAsync(invoice))
                {
                    await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.New, invoice));
                    return RedirectToAction(nameof(Index));
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

        [HttpGet]
        public async Task<ActionResult> Edit(int? Id)
        {
            if (Id != null)
            {
                var model = await repository.FindByIdAsync(Id);
                if(model==null)
                {
                    return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Invoice   With Id : {Id} NotFound" });
                }
                CustomerInvoiceViewModel invoice = new CustomerInvoiceViewModel
                {
                    dDate = model.dDate,
                    CustomerId = model.CustomerId,
                    SolarQTY = model.SolarQTY,
                    Oil80QTY = model.Oil80QTY,
                    Oil92QTY = model.Oil92QTY,
                    Oil95QTY = model.Oil95QTY,
                    SolarUnitPrice = model.SolarUnitPrice,
                    Oil80UnitPrice = model.Oil80UnitPrice,
                    Oil92UnitPrice = model.Oil92UnitPrice,
                    Oil95UnitPrice = model.Oil95UnitPrice,
                    SolarTotalPrice = model.SolarTotalPrice,
                    Oil80TotalPrice = model.Oil80TotalPrice,
                    Oil92TotalPrice = model.Oil92TotalPrice,
                    Oil95TotalPrice = model.Oil95TotalPrice,
                    TotalPrice = model.TotalPrice,
                    Desc = model.Desc,

                };

                return View(invoice);
            }
            else
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Invoice   With Id : {Id} NotFound" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, CustomerInvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerInvoice invoice = new CustomerInvoice
                {
                    dDate = model.dDate,
                    CustomerId = model.CustomerId,
                    SolarQTY = model.SolarQTY,
                    Oil80QTY = model.Oil80QTY,
                    Oil92QTY = model.Oil92QTY,
                    Oil95QTY = model.Oil95QTY,
                    SolarUnitPrice = model.SolarUnitPrice,
                    Oil80UnitPrice = model.Oil80UnitPrice,
                    Oil92UnitPrice = model.Oil92UnitPrice,
                    Oil95UnitPrice = model.Oil95UnitPrice,
                    SolarTotalPrice = model.SolarTotalPrice,
                    Oil80TotalPrice = model.Oil80TotalPrice,
                    Oil92TotalPrice = model.Oil92TotalPrice,
                    Oil95TotalPrice = model.Oil95TotalPrice,
                    TotalPrice = model.TotalPrice,
                    Desc = model.Desc,

                };
                if (await repository.UpdateAsync(Id, invoice))
                {
                    await NotificationRepository.AddAsync(new Models.Notification());
                    return RedirectToAction(nameof(Index));
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
        public async Task<ActionResult> Delete(int Id)
        {
            var model = repository.FindByIdAsync(Id);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Invoice   With Id : {Id} NotFound" });
            }
            if(await repository.DeleteByIdAsync(Id))
            {
               await NotificationRepository.AddAsync(new Models.Notification());
            }
            return RedirectToAction(nameof(Index));
        }
    }
}