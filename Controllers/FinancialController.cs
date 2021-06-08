using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Helper;
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
    public class FinancialController : Controller
    {
        FinancialRepository repository = new FinancialRepository(); 
        public  ActionResult  Index(string JournalType, int SubledgerTypeId)
        {
            ViewBag.JournalType = JournalType;
            ViewBag.SubledgerTypeId = SubledgerTypeId;
            //List<FinancialViewModel> lst = await repository.ListAsync(JournalType);
            //return View(lst);
            return View();
        }


        public async Task<ActionResult> LoadDataAsync2(string JournalType, string SubledgerTypeId)
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;



                ViewBag.JournalType = JournalType;
                ViewBag.SubledgerTypeId = SubledgerTypeId;
                IEnumerable<FinancialViewModel> Vouchers = await repository.ListAsync(JournalType);

                //Search
                if (!string.IsNullOrEmpty(searchValue.ToString()))
                {
                    //Vouchers = Vouchers.Where(m => m.Subledger.Name.Contains(searchValue)).ToList();
                }

                ////Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    Vouchers = Vouchers.OrderBy(sortColumn + " " + sortColumnDirection);
                //}


                //total number of rows count   
                recordsTotal = Vouchers.Count();
                //Paging   
                var data = Vouchers.Skip(skip).Take(pageSize).ToList();




                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<ActionResult> LoadDataAsync(string JournalType, string SubledgerTypeId)
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
                ViewBag.JournalType = JournalType;
                ViewBag.SubledgerTypeId = SubledgerTypeId;
                IEnumerable<FinancialViewModel> data = await repository.ListAsync(JournalType);
                
                // Total record count.   
                int totalRecords = data.Count();
                
                // Verification.   
                if (!string.IsNullOrEmpty(search) &&
                    !string.IsNullOrWhiteSpace(search))
                {
                    // Apply search   
                    data = data.Where(p => p.Subledger.Name.ToString().ToLower().Contains(search.ToLower())
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
                result = this.Json(new{draw = Convert.ToInt32(draw),recordsTotal = totalRecords,recordsFiltered = recFilter,data = data}, JsonRequestBehavior.AllowGet);
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

        private List<FinancialViewModel> SortByColumnWithOrder(string order, string orderDir, IEnumerable<FinancialViewModel> data)
        {
            // Initialization.   
            List<FinancialViewModel> lst = new List<FinancialViewModel>();
            try
            {
                // Sorting   
                switch (order)
                {
                    case "0":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Id).ToList() : data.OrderBy(p => p.Id).ToList();
                        break;
                    case "1":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
                        break;
                    case "2":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
                        break;
                    case "3":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
                        break;
                    case "4":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
                        break;
                    case "5":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
                        break;
                    case "6":
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
                        break;
                    default:
                        // Setting.   
                        lst = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? data.OrderByDescending(p => p.Subledger.Name).ToList() : data.OrderBy(p => p.Subledger.Name).ToList();
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
        public ActionResult Create(string JournalType, string SubledgerTypeId)
        {
            ViewBag.JournalType = JournalType;
            ViewBag.SubledgerTypeId = SubledgerTypeId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(FinancialViewModel model)
        {
            if (model.JournalType != 6)
            {
                ModelState.Remove("CustodyTypeId");
            }
            if (model.JournalType != 7 || model.JournalType != 11)
            {
                ModelState.Remove("DepositNo");
            }

            if (ModelState.IsValid)
            {
                var Financial = new Financial
                {
                    BranchId = model.BranchId,
                    JournalType = model.JournalType,
                    SubledgerTypeId = model.SubledgerTypeId,
                    SubledgerId = model.SubledgerId,
                    dDate = ObjectConverter.CDate(model.dDate),
                    CustodyTypeId = model.CustodyTypeId,
                    DepositNo = model.DepositNo,
                    Amount = model.Amount,
                    Desc = model.Desc
                };
                await repository.AddAsync(Financial);
                return RedirectToAction(nameof(Index), new { JournalType = model.JournalType, SubledgerTypeId = model.SubledgerTypeId });
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? Id,string JournalType)
        {
            if (Id != null)
            {
                var model = await repository.FindByIdAsync(Id, JournalType);
                if(model==null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                 
                return View(model);
            }
            else
            {

                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, FinancialViewModel model)
        {
            if (model.JournalType != 6)
            {
                ModelState.Remove("CustodyTypeId");
            }
            if (model.JournalType != 7 || model.JournalType != 11)
            {
                ModelState.Remove("DepositNo");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Financial = new Financial
                    {
                        BranchId = model.BranchId,
                        JournalType = model.JournalType,
                        SubledgerTypeId = model.SubledgerTypeId,
                        SubledgerId = model.SubledgerId,
                        dDate = ObjectConverter.CDate(model.dDate),
                        CustodyTypeId = model.CustodyTypeId,
                        DepositNo = model.DepositNo,
                        Amount = model.Amount,
                        Desc = model.Desc
                    };

                    await repository.UpdateAsync(Id, Financial);
                    return RedirectToAction(nameof(Index), new { JournalType = model.JournalType, SubledgerTypeId = model.SubledgerTypeId });
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = repository.FindByIdAsync(Id,5);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            await repository.DeleteByIdAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}