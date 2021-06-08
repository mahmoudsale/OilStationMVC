using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using OilStationMVC.ViewModels;
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
    public class ChargeAreaController : Controller
    {
        ChargeAreaRepository repository = new ChargeAreaRepository(); 
        public async Task<ActionResult> Index()
        {
            List<ChargeArea> lst = await repository.ListAsync();
            return View(lst);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ChargeAreaViewModel model)
        {

            if (ModelState.IsValid)
            {
                var chargeArea = new ChargeArea
                {
                    Name = model.Name,
                    Cost = model.Cost,
                    CompanyId = model.CompanyId
                };
                if (await repository.AddAsync(chargeArea))
                {
                    await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.New, chargeArea));
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
                if (model == null)
                {
                    return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Charge Area  With Id : {Id} NotFound" });
                }
                var chargeArea = new ChargeAreaViewModel
                {
                    Name = model.Name,
                    Cost = model.Cost,
                    CompanyId = model.CompanyId
                };
                return View(chargeArea);
            }
            else
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Charge Area  With Id : {Id} NotFound" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, ChargeAreaViewModel model)
        {
            var ChargeArea = repository.FindByIdAsync(Id);
            if (ChargeArea == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Charge Area  With Id : {Id} NotFound" });
            }
            if (ModelState.IsValid)
            {
                var chargeArea = new ChargeArea
                {
                    Name = model.Name,
                    Cost = model.Cost,
                    CompanyId = model.CompanyId
                };
                if (await repository.UpdateAsync(Id, chargeArea))
                {
                    await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.Edit, chargeArea));
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
            var model = await repository.FindByIdAsync(Id);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Charge Area  With Id : {Id} NotFound" });
            }
            if (await repository.DeleteByIdAsync(Id))
            {
                await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.Delete, model));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}