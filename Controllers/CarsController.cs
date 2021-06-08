using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    [mAuthorize]
    public class CarsController : Controller
    {
        CarRepository repository = new CarRepository(); 
        public async Task<ActionResult> Index()
        {
            List<Car> lst = await repository.ListAsync();
            return View(lst);
        }

        [HttpGet]
        [mAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [mAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Create(Car model)
        {

            if (ModelState.IsValid)
            {  
                if (await repository.AddAsync(model))
                {
                    await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.New, model));
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
                    return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Car  With Id : {Id} NotFound" });
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, Models.Car model)
        {
            var car = await repository.FindByIdAsync(Id);
            if (car == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Car With Id : {Id} NotFound" });
            }
            if (ModelState.IsValid)
            {
                if (await repository.UpdateAsync(Id, model))
                {
                    await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.Edit, model));
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
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
            Car model =await repository.FindByIdAsync(Id);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Car With Id : {Id} NotFound" });
            }
            if(await repository.DeleteByIdAsync(Id))
            {
                await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.Delete, model));
            }
            return RedirectToAction(nameof(Index));
        } 
    }
}