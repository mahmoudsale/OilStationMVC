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
    public class CustomerController : Controller
    {
        CustomerRepository repository = new CustomerRepository();
        public async Task<ActionResult> Index()
        {
            List<Customer> lst = await repository.ListAsync();
            return View(lst);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var customer = new Customer
                    {
                        Name = model.Name,
                        Adress = model.Adress,
                        Desc = model.Desc,
                        Email = model.Email,
                        Fax = model.Fax,
                        Telephone = model.Telephone,
                        CustomerTypeId =int.Parse( model.CustomerTypeId)
                    };
                    await repository.AddAsync(customer);
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

        [HttpGet]
        public async Task<ActionResult> Edit(int? Id)
        {
            if (Id != null)
            {
                var model = await repository.FindByIdAsync(Id);
                if(model==null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                var customer = new CustomerViewModel
                {
                    Name = model.Name,
                    Adress = model.Adress,
                    Desc = model.Desc,
                    Email = model.Email,
                    Fax = model.Fax,
                    Telephone = model.Telephone,
                    CustomerTypeId =  model.CustomerTypeId.ToString()
                };
                return View(customer);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = new Customer
                    {
                        Name = model.Name,
                        Adress = model.Adress,
                        Desc = model.Desc,
                        Email = model.Email,
                        Fax = model.Fax,
                        Telephone = model.Telephone,
                        CustomerTypeId = int.Parse(model.CustomerTypeId)
                    };
                    await repository.UpdateAsync(Id, customer);
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
            var model = repository.FindByIdAsync(Id);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            await repository.DeleteByIdAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}