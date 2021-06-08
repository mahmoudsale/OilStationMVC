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
    public class InnvoiceController : Controller
    {
        InvoiceRepository repository = new InvoiceRepository();
        public async Task<ActionResult> Index()
        {
            List<Invoice> lst = await repository.ListAsync();
            return View(lst);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(InvoiceViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Invoice invoice = new Invoice
                    {
                        Id = model.Id,
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
                    if (await repository.AddAsync(invoice))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(model);
                    }
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
                if (model == null)
                {
                    return RedirectToAction("NotFound", "Error");
                }
                InvoiceViewModel invoiceViewModel = new InvoiceViewModel
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
                return View(invoiceViewModel);
            }
            else
            {
                return RedirectToAction("NotFound", "Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Invoice invoice = new Invoice
                    {
                        Id = model.Id,
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
                    await repository.UpdateAsync(Id, invoice);
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

        //[HttpGet]
        //public async Task<ActionResult> Details(int Id)
        //{
        //    System.Data.DataSet ds = await repository.ReportAsync("vuw_CompanyInnvoices", " nId=" + Id + "");
        //    Session["Dataset"] = ds;
        //    Session["ReportName"] = "ComapnyInnvoice";
        //    return Redirect("~/Report/ReportForm.aspx");
        //}
    }
}