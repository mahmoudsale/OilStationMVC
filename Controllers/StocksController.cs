using Microsoft.AspNet.SignalR;
using OilStationMVC.AuthenticationAndAuthorization;
using OilStationMVC.Hubs;
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
    public class StocksController : Controller
    {
        StockRepository repository = new StockRepository();
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var Stocks = await repository.ListAsync();
            return View(Stocks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Stock model)
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
                    return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Stock With Id : {Id} NotFound" });
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Stock With Id : {Id} NotFound" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int Id, Stock model)
        {
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
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            Stock model =await repository.FindByIdAsync(Id);
            if (model == null)
            {
                return RedirectToAction("NotFound", "Error", new { ErrorMessage = $"Stock With Id : {Id} NotFound" });
            }
            if(await repository.DeleteByIdAsync(Id))
            {
                await NotificationRepository.AddAsync(NotificationRepository.GenerateNotificationModel(ModelType.Edit, model));
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<JsonResult> Stock(int StockId)
        {
            Stock stock = await repository.FindByIdAsync(StockId);
            return Json(stock, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ParseToExcel(HttpPostedFileBase[] filesUpload)
        {
            //decimal currentFile = 1.0M;
            //int fileProgress = 0;
            //int maxCount = filesUpload.Count();

            //// Initialize Hub context
            //var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            //hubContext.Clients.All.sendMessage("Initalizing...", fileProgress);

            //try
            //{
            //    // Map server path for temporary file placement (Generate new serialized path for each instance)
            //    var tempGenFolderName = DateTime.Now.ToString("yyyyMMdd_HHmmss"); //SubstringExtensions.GenerateRandomString(10, false);
            //    var tempPath = Server.MapPath("~/" + tempGenFolderName + "/");

            //    // Create Temporary Serialized Sub-Directory
            //    FileInfo thisFilePath = new FileInfo(tempPath);
            //    thisFilePath.Directory.Create();

            //    // Iterate through PostedFileBase collection
            //    foreach (HttpPostedFileBase file in filesUpload)
            //    {
            //        // Does this iteration of file have content?
            //        if (file.ContentLength > 0)
            //        {
            //            fileProgress = Convert.ToInt32(Math.Round(currentFile / maxCount * 100, 0));

            //            // Indicate file is being uploaded
            //            hubContext.Clients.All.sendMessage("Uploading " + Path.GetFileName(file.FileName), fileProgress);

            //            file.SaveAs(Path.Combine(thisFilePath.FullName, file.FileName));
            //            currentFile++;
            //        }
            //    }

            //    // Initialize new ClosedXML/Excel workbook
            //    //var hl7Workbook = new XLWorkbook();

            //    // Restart progress
            //    currentFile = 1.0M;
            //    maxCount = Directory.GetFiles(tempPath).Count();

            //    // Iterate through the files saved in the Temporary File Path
            //    foreach (var file in Directory.EnumerateFiles(tempPath))
            //    {
            //        var fileNameTmp = Path.GetFileName(file);

            //        fileProgress = Convert.ToInt32(Math.Round(currentFile / maxCount * 100, 0));

            //        // Update status
            //        hubContext.Clients.All.sendMessage("Parsing " + Path.GetFileName(file), fileProgress);

            //        // Initialize string to capture text from file
            //        string fileDataString = string.Empty;

            //        // Use new Streamreader instance to read text
            //        using (StreamReader sr = new StreamReader(file))
            //        {
            //            fileDataString = sr.ReadToEnd();
            //        }

            //        // Do more work with the file, adding file contents to a spreadsheet...
            //        currentFile++;
            //    }


            //    // Delete temporary file 
            //    thisFilePath.Directory.Delete();

            //}
            //catch (Exception ex)
            //{
            //    ViewBag.TaskMessage =
            //        "<div style=\"margin-left:15px;margin-right:15px\" class=\"alert alert-danger\">"
            //        + "<i class=\"fa fa-exclamation-circle\"></i> "
            //        + "An error occurred during the process...<br />"
            //        + "-" + ex.Message.ToString()
            //        + "</div>"
            //        ;
            //}

            return View();
        }
    }


}