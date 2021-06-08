using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.CommonFolder;
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
    public class AreaTransportController : Controller
    {
        AreaTransportReository repository = new AreaTransportReository();  
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<AreaTransport> lst = await repository.ListAsync(); 
            return View(lst);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(AreaTransport model)
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
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? Id)
        {
            if (Id != null)
            {
                var model = await repository.FindByIdAsync(Id);
                if (model == null)
                {
                    return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Area Transport With Id : {Id} NotFound" });
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, AreaTransport model)
        {
            var areaTransport = await repository.FindByIdAsync(Id);
            if (areaTransport == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Area Transport With Id : {Id} NotFound" });
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
            AreaTransport model =await repository.FindByIdAsync(Id);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Area Transport With Id : {Id} NotFound" });
            }
            if (await repository.DeleteByIdAsync(Id))
            {
                await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.New, model));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}