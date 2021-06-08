using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
  
    [mAuthorize]
    public class ProductController : Controller
    {
        ProductRepository repository = new ProductRepository();
        public async Task<ActionResult> Index()
        {
            return View(await repository.ListAsync());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Product model)
        {
            if (ModelState.IsValid)
            {

                await repository.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Verify ALl Paramters");
                return View(model);
            }
        }



        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("NotFound", "Error");
            }
            var product = await repository.FindByIdAsync(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("NotFound", "Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repository.UpdateAsync(id, model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await repository.ObjectExists(model.Id))
                    {
                        return RedirectToAction("NotFound", "Error");
                    }
                    else
                    {
                        throw new Exception("Product Not Found With Id : " + model.Id);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View();
            }

        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var product = await repository.FindByIdAsync(id);
            if (product == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            return View(product);
        }
        [AllowAnonymous]
        public async Task<JsonResult> ProductsAsync()
        {
            List<Product> lst = await repository.ListAsync();
            return Json(lst,JsonRequestBehavior.AllowGet);
        }
    }
}