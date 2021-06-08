using Newtonsoft.Json;
using OilStationMVC.Models;
using OilStationMVC.Models.Repositories;
using OilStationMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OilStationMVC.Controllers
{
    public class StationInvoiceController : Controller
    {
        //[AuthenticationAndAuthorization.mAuthorize((int)Roles.Administrators)]
        InvoiceRepository repository = new InvoiceRepository();
        public async Task<ActionResult> Index()
        {
            var Invoices = await repository.ListAsync();

            return View(Invoices);
        }
        public ActionResult CreateInvoice()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateInvoice(InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                Invoice invoice = new Invoice
                {
                    InnvoiceNo = model.InnvoiceNo,
                    dDate = model.dDate,
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
                    CompanyId = model.CompanyId,
         
                    StockId = model.StockId,
                 
                    StationId = model.StationId,
                
                    CarId = model.CarId,
                
                    DriverId = model.DriverId,
                  
                    StockTransportUnit = model.StockTransportUnit,
                    StockTransportAmount = model.StockTransportAmount
                };
                await repository.AddAsync(invoice);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var Invoice = await repository.FindByIdAsync(id);

            if (Invoice == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            InvoiceViewModel model = new InvoiceViewModel();
            model.dDate = Invoice.dDate;
            model.InnvoiceNo = Invoice.InnvoiceNo;
            model.CompanyId = Invoice.CompanyId;
            model.StockId = Invoice.StockId;
            model.StationId = Invoice.StationId;
            model.DriverId = Invoice.DriverId;
            model.SolarQTY = Invoice.SolarQTY;
            model.Oil80QTY = Invoice.Oil80QTY;
            model.Oil92QTY = Invoice.Oil92QTY;
            model.Oil95QTY = Invoice.Oil95QTY;
            model.CarId = Invoice.CarId;
            model.SolarUnitPrice = Invoice.SolarUnitPrice;
            model.Oil80UnitPrice = Invoice.Oil80UnitPrice;
            model.Oil92UnitPrice = Invoice.Oil92UnitPrice;
            model.Oil95UnitPrice = Invoice.Oil95UnitPrice;
            model.SolarTotalPrice = Invoice.SolarTotalPrice;
            model.Oil80TotalPrice = Invoice.Oil80TotalPrice;
            model.Oil92TotalPrice = Invoice.Oil92TotalPrice;
            model.Oil95TotalPrice = Invoice.Oil95TotalPrice;
            model.TotalPrice = Invoice.TotalPrice;
            model.Desc = Invoice.Desc;
            model.StockTransportUnit = Invoice.StockTransportUnit;
            model.StockTransportAmount = Invoice.StockTransportAmount;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, InvoiceViewModel entity)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Invoice Invoice = new Invoice();
                    Invoice.dDate = entity.dDate;
                    Invoice.InnvoiceNo = entity.InnvoiceNo;
                    Invoice.CompanyId = entity.CompanyId;
                    Invoice.StockId = entity.StockId;
                    Invoice.StationId = entity.StationId;
                    Invoice.DriverId = entity.DriverId;
                    Invoice.SolarQTY = entity.SolarQTY;
                    Invoice.Oil80QTY = entity.Oil80QTY;
                    Invoice.Oil92QTY = entity.Oil92QTY;
                    Invoice.Oil95QTY = entity.Oil95QTY;
                    Invoice.CarId = entity.CarId;
                    Invoice.SolarUnitPrice = entity.SolarUnitPrice;
                    Invoice.Oil80UnitPrice = entity.Oil80UnitPrice;
                    Invoice.Oil92UnitPrice = entity.Oil92UnitPrice;
                    Invoice.Oil95UnitPrice = entity.Oil95UnitPrice;
                    Invoice.SolarTotalPrice = entity.SolarTotalPrice;
                    Invoice.Oil80TotalPrice = entity.Oil80TotalPrice;
                    Invoice.Oil92TotalPrice = entity.Oil92TotalPrice;
                    Invoice.Oil95TotalPrice = entity.Oil95TotalPrice;
                    Invoice.TotalPrice = entity.TotalPrice;
                    Invoice.Desc = entity.Desc;

                    await repository.UpdateAsync(id, Invoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", "Please Verify All Input as Data Types And Required inputs");
                return View();
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var Invoice = await repository.FindByIdAsync(id);
            if (Invoice == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            await repository.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound", "Error");
            }

            var Invoice = await repository.FindByIdAsync(id);
            if (Invoice == null)
            {
                return RedirectToAction("NotFound", "Error");
            }
            InvoiceViewModel model = new InvoiceViewModel();
            model.dDate = Invoice.dDate;
            model.InnvoiceNo = Invoice.InnvoiceNo;
            model.CompanyId = Invoice.CompanyId;
            model.StockId = Invoice.StockId;
            model.StationId = Invoice.StationId;
            model.DriverId = Invoice.DriverId;
            model.SolarQTY = Invoice.SolarQTY;
            model.Oil80QTY = Invoice.Oil80QTY;
            model.Oil92QTY = Invoice.Oil92QTY;
            model.Oil95QTY = Invoice.Oil95QTY;
            model.CarId = Invoice.CarId;
            model.SolarUnitPrice = Invoice.SolarUnitPrice;
            model.Oil80UnitPrice = Invoice.Oil80UnitPrice;
            model.Oil92UnitPrice = Invoice.Oil92UnitPrice;
            model.Oil95UnitPrice = Invoice.Oil95UnitPrice;
            model.SolarTotalPrice = Invoice.SolarTotalPrice;
            model.Oil80TotalPrice = Invoice.Oil80TotalPrice;
            model.Oil92TotalPrice = Invoice.Oil92TotalPrice;
            model.Oil95TotalPrice = Invoice.Oil95TotalPrice;
            model.TotalPrice = Invoice.TotalPrice;
            model.StockTransportAmount = Invoice.StockTransportAmount;
            model.Desc = Invoice.Desc;

            model.Company = Invoice.Company;
            model.Station = Invoice.Station;
            model.Stock = Invoice.Stock;
            model.Car = Invoice.Car;
            model.Driver = Invoice.Driver;

            //var report = new ViewAsPdf("Details")
            //{
            //    PageMargins = { Left = 5, Bottom = 5, Right = 5, Top = 5 },
            //    Model = model,


            //};
            return View(model);
        }

       


        public async Task<JsonResult> Stock(int StockId)
        {
            //Stock stock = await stockRepository.FindByIdAsync(StockId);
            return Json(new Stock());
        }

        public async Task<JsonResult> Stocktest(int StockId)
        {
            //List<Stock> stock = await stockRepository.ListAsync();
            return Json(new Stock());
        }
    }
}