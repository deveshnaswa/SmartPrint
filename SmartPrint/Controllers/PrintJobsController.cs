using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Management;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using SmartPrint.CustomLibaries;
using System.Collections.Generic;
using System.Web;
using SmartPrint.Helpers;

namespace SmartPrint.Controllers
{
    public class PrintJobsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: PrintJobs
        public ActionResult Index()
        {
            return View(db.PrintJobs.ToList());
        }

        // GET: PrintJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = db.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            return View(printJobs);
        }

        // GET: PrintJobs/Create
        public ActionResult Create(int? id)
        {

            PrintJobs model = new PrintJobs();

            UserDocs userDocsToPrint = db.UserDocs.Find(id);

            //ViewBag.UserDocsToPrint = userDocsToPrint;
            //model.JobId = userDocsToPrint
            model.UserId = int.Parse(User.Identity.GetUserId());
            model.DocId = userDocsToPrint.DocId;
            model.DocName = userDocsToPrint.DocName;
            model.DocTypeId = userDocsToPrint.DocTypeId;
            model.DocExt = userDocsToPrint.DocExt;
            model.DocFileNameOnServer =userDocsToPrint.DocFileName;
            model.DocFilePath = userDocsToPrint.DocFilePath;
            ViewBag.Document = new Document(userDocsToPrint.DocFileName, userDocsToPrint.DocFilePath);

            /*model.DocTotalPages = "";
            model.PrintCostId = "";
            model.MonoPages = "";
            model.ColorPages = "";
            model.IsColor = "";
            model.IsDuplex = "";
            model.IsCollate = "";
            model.UnitCost = "";
            model.MonoUnitCost = "";
            model.ColorUnitCost = "";
            model.UnitItem = "";
            model.JobRemarks = "";
            model.PagesFrom = "";
            model.PagesTo = "";
            model.NumCopies = "";
            model.TotalPageCount = "";
            model.TotalPageCost = "";
            model.CreditUsed = "";
            model.JobError = "";
            model.JobErrorRemarks = "";
            model.PrinterName = "";
            model.PrinterPath = "";
            model.JobStatusId = "";
            model.AddedBy = "";
            model.AddedOn = "";
            model.EditedBy = "";
            model.EditedOn = "";
            model.StatusId = "";
            */


            ViewBag.PrinterName = new SelectList(Printer.GetPrinterList(),"Value","Text");
            
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");

            ViewBag.PrintCostId= new SelectList(db.PrintCosts, "PrintCostId", "Name");
            ViewBag.UserDocsId = userDocsToPrint.DocId;

            // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");
            // ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName");

           
            return View(model);
        }

        // POST: PrintJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] PrintJobs printJobs)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PrintJobs.Add(printJobs);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(printJobs);
        //}
        public ActionResult Create([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] PrintJobs printJob)
        {
            if (ModelState.IsValid)
            {
                //print
                var printSettings = new PrintFileSettings()
                {
                    Copies = Convert.ToInt16(printJob.NumCopies),
                    StartPage = printJob.PagesFrom,
                    EndPage = printJob.PagesTo,
                    FilePath = printJob.DocFilePath,
                    PrinterName = printJob.PrinterName,
                    IsDuplex = printJob.IsDuplex,
                    IsColored = printJob.IsColor
                };
                var printHelper = new PrintHelper();
                printHelper.PrintFile(printSettings);

                db.PrintJobs.Add(printJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printJob);
        }
        // GET: PrintJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = db.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName", printJobs.StatusId);
            //ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName", users.UStatusId);
            return View(printJobs);
        }

        // POST: PrintJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,EditedBy,EditedOn,StatusId",Exclude = "AddedBy,AddedOn")] PrintJobs printJobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printJobs).State = EntityState.Modified;
                db.Entry(printJobs).Property(uco => uco.AddedBy).IsModified = false;
                db.Entry(printJobs).Property(uco => uco.AddedOn).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printJobs);
        }

        // GET: PrintJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = db.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            return View(printJobs);
        }

        // POST: PrintJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                PrintJobs printJobs = db.PrintJobs.Find(id);
                printJobs.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(printJobs).State = EntityState.Modified;
                //db.Users.Remove(users);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }

           
        }

        public ActionResult GetPrintCosts(int PrintCostId)
        {
            var printCost = db.PrintCosts.Where(c => c.PrintCostId== PrintCostId);
            
            return Json(printCost, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPrinterProperties(string PrinterSelected)
        {
            var printerprops = Printer.GetPrinterPropertiesList(PrinterSelected);
            return   Json(printerprops, JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }




        ///
        /// 
        /// 
        /// Work for document preview goes here
        /// 
        /// 
        /// 
        /// 
        //private MainDbContext db = new MainDbContext();
        /// <summary>
        /// The library.
        /// </summary>
        private readonly ILibrary _library = new Library(MvcApplication.GetDocumentsDirectory());
        /// <summary>
        /// The names of the documents within the library
        /// </summary>
        private string[] _documents;

        /// <summary>
        /// This private method populates a string array with the library documents name.
        /// </summary>
        private void PopulateLibrary()
        {
            _documents = new string[_library.GetDocumentCount()];
            for (int i = 0; i < _library.GetDocumentCount(); i++)
            {
                _documents[i] = _library.GetDocumentName(i);
            }
        }

        /// <summary>
        /// The Index method implements the controller entry point. 
        /// The library is populated with the documents and the names of the documents within the list are sent
        /// to the View via the ViewBag.
        /// 
        /// Please note the demonstration is limited to a fixed list. The list is populated on startup. Any modification
        /// within the library will not impact the list of documents displayed within the view.
        /// </summary>
        // GET: Default// GET: Document
        public ActionResult DocView(int? UserDocId)
        {


            if (UserDocId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocs userDocs = db.UserDocs.Find(UserDocId);
            //PopulateLibrary();
            ViewBag.Library = _documents;
            if (userDocs != null)
            {
                ViewBag.Document = new Document(userDocs.DocFileName, userDocs.DocFilePath);
            }
            return View();
        }

        /// <summary>
        /// The Index method with two parameters implements the HTTP POST action.
        /// The HTTP POST occurs when a document has been clicked for display (through a form post).
        /// 
        /// The ViewBag is updated with the list of documents (unchanged) and the document to be displayed.
        /// </summary>
        /// <param name="id">The document id that has been selected for display.</param>
        /// <param name="hdnScrollPos">The vertical scrollbar position to restore.</param>
        [HttpPost]
        public ActionResult DocView(int id, int hdnScrollPos)
        {
            PopulateLibrary();
            ViewBag.ScrollPos = hdnScrollPos;
            ViewBag.Library = _documents;
            ViewBag.Document = new Document(_library.GetDocumentName(id), _library.GetDocumentPath(id));
            return View();
        }

    }
}
