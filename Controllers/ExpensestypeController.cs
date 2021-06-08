using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class ExpensestypeController : Controller
    {
        ExpenseTypeRepository repository = new ExpenseTypeRepository();
        public async Task<ActionResult> Index()
        {
            List<ExpenseType> lst = await repository.ListAsync();
            return View(lst);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ExpenseType model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.AddAsync(model);
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
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, ExpenseType model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateAsync(Id, model);
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