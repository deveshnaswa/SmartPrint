using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SmartPrint.CustomLibaries;
using System.Collections.Generic;
using SmartPrint.Common.Enums;
using SmartPrint.Helpers;
using System.Runtime.Caching;
using SmartPrint.Helpers.User;
using SmartPrint.ViewModels;
using System.Web;
using SmartPrint.Controllers.Base;

namespace SmartPrint.Controllers
{
    public class PrintJobsController : SmartPrintBaseController
    {
        MainDbContext DbContext;
        UserHelper _userHelper;

        public PrintJobsController() : this(new MainDbContext())
        {

        }
        public PrintJobsController(MainDbContext dbContext):this(dbContext, new UserHelper(dbContext))
        {

        }
        public PrintJobsController(MainDbContext dbContext, UserHelper userHelper)
        {
            DbContext = dbContext;
            _userHelper = userHelper;
        }

        // GET: PrintJobs
        public ActionResult Index()
        {
            return View(DbContext.PrintJobs.ToList());
        }

        // GET: PrintJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = DbContext.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            return View(printJobs);
        }

        // GET: PrintJobs/Create
        public ActionResult Create(int? id)
        {
            
            UserDocs userDocsToPrint = DbContext.UserDocs.FirstOrDefault(x=>x.DocId == id);
            if (userDocsToPrint == null)
            {
                return HttpNotFound();
            }
            var model = new PrintJobsViewModel();
            var printCost = DbContext.PrintCosts.FirstOrDefault();
            model.DocumentId = userDocsToPrint.DocId;
            model.DocumentName = userDocsToPrint.DocName;
            model.ColorUnitcost = printCost.ColorCostPerPage;
            model.MonoUnitcost = printCost.MonoCostPerPage;

            var totalNoOfPages = getTotalPagesByDoc(userDocsToPrint.DocFilePath);
            model.PagesFrom = 1;
            model.PagesTo = totalNoOfPages;
            model.TotalPageCount = totalNoOfPages;
            model.TotalPageCost = totalNoOfPages * model.MonoUnitcost;
            model.NumCopies = 1;

            ViewBag.TotalPages = totalNoOfPages;
            ViewBag.DocumentPath = $"\\{GetUploadFolderName()}\\{userDocsToPrint.DocFileName}";
            ViewBag.MimeMappings = MimeMapping.GetMimeMapping(userDocsToPrint.DocName);
            ViewBag.PrinterList = MemoryCache.Default.Get(Common.Constants.PrinterListName) as Dictionary<string, PrinterDetails>;

            return View(model);
        }

        
        // POST: PrintJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PrintJobQueueRefId,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] PrintJobs printJobs)
        public ActionResult Create(PrintJobsViewModel printJob)
        {
            if (ModelState.IsValid)
            {
                var IsUserHasSufficentCreditForPrintJob = false;
                var loggedinUserId = GetLoggedInUserId();
                var lastTransaction = DbContext.UserTxns
                    .Where(tx => tx.UserId == loggedinUserId && tx.StatusId != (int)RecordStatus.Deleted)   // Filter
                    .OrderByDescending(tx => tx.TxnId) // prioritet is still here - order by it
                    .FirstOrDefault();  // Now grab the transaction balance

                if (lastTransaction != null)
                {
                    var printCost = DbContext.PrintCosts.FirstOrDefault();
                    var totalPageToPrint = Math.Abs(printJob.PagesTo - printJob.PagesFrom) + 1;
                    var currentJobCost = printJob.TotalPageCost;
                    if (printJob.IsColor)
                    {
                        currentJobCost = totalPageToPrint * printCost.ColorCostPerPage * printJob.NumCopies;
                    }
                    else
                    {
                        currentJobCost = totalPageToPrint * printCost.MonoCostPerPage * printJob.NumCopies;
                    }
                    IsUserHasSufficentCreditForPrintJob = lastTransaction.TxnBalance >= currentJobCost;
                    if (IsUserHasSufficentCreditForPrintJob)
                    {
                        var now = DateTimeHelper.GetTimeStamp();
                        UserTxns userTransaction = new UserTxns();
                        //return success
                        //update database for printjob tabale.
                        // update user transaction table.
                        userTransaction.UserId = GetLoggedInUserId();
                        userTransaction.TxnTypeId = (int) TransactionType.Debit;
                        userTransaction.TxnAmount = currentJobCost;
                        userTransaction.TxnBalance = lastTransaction.TxnBalance - currentJobCost;
                        //userTransaction.TxnRefJobId = jPrintJobRefId;
                        userTransaction.TxnStatusId = (int)TransactionStatus.Pending;
                        userTransaction.StatusId = (int)RecordStatus.Active;

                        var documentToPrint = DbContext.UserDocs.FirstOrDefault(x => x.DocId == printJob.DocumentId);
                        //send document to print ->job
                        //print
                        var printSettings = new PrintFileSettings()
                        {
                            Copies = (short)printJob.NumCopies,
                            StartPage = printJob.PagesFrom,
                            EndPage = printJob.PagesTo,
                            FilePath = documentToPrint.DocFilePath,
                            PrinterName = printJob.PrinterName,
                            IsDuplex = printJob.IsDuplex,
                            IsColored = printJob.IsColor
                        };
                        var printHelper = new PrintHelper();
                        var referenceJobId = Int32.Parse(printHelper.PrintFile(printSettings));

                        var printJobToAdd = printJob.GetDbObjectToCreate(documentToPrint,referenceJobId, GetLoggedInUserId());
                        //insert printjobs
                        //printJobs.TotalPageCount = jtotalpage;
                        DbContext.PrintJobs.Add(printJobToAdd);
                        DbContext.SaveChanges();
                        userTransaction.TxnRefJobId = printJobToAdd.JobId;
                        //// insert user transaction table
                        DbContext.UserTxns.Add(userTransaction);
                        DbContext.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Your credit balance is not sufficient. Please get it topped up.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Your credit balance is not sufficient. Please get it topped up.");
                }
            }
            if (!ModelState.IsValid)
            {
                UserDocs userDocsToPrint = DbContext.UserDocs.FirstOrDefault(x => x.DocId == printJob.DocumentId);
                if (userDocsToPrint == null)
                {
                    return HttpNotFound();
                }
                var printCost = DbContext.PrintCosts.FirstOrDefault();
                
                printJob.ColorUnitcost = printCost.ColorCostPerPage;
                printJob.MonoUnitcost = printCost.MonoCostPerPage;

                var totalNoOfPages = getTotalPagesByDoc(userDocsToPrint.DocFilePath);
                printJob.TotalPageCount = totalNoOfPages;
                
                ViewBag.TotalPages = totalNoOfPages;
                ViewBag.DocumentPath = $"\\{GetUploadFolderName()}\\{userDocsToPrint.DocFileName}";
                ViewBag.MimeMappings = MimeMapping.GetMimeMapping(userDocsToPrint.DocName);
                ViewBag.PrinterList = MemoryCache.Default.Get(Common.Constants.PrinterListName) as Dictionary<string, PrinterDetails>;
                return View(printJob);
            }
            return RedirectToAction("Index");
        }

        // GET: PrintJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintJobs printJobs = DbContext.PrintJobs.Find(id);
            if (printJobs == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            ViewBag.StatusId = new SelectList(MemoryCache.Default.Get(Common.Constants.RecordStatusListName) as Dictionary<int, string>, "Key", "Value",printJobs.StatusId);
            //ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName", users.UStatusId);
            return View(printJobs);
        }

        // POST: PrintJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,UserId,DocId,DocName,DocTypeId,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,PrintJobQueueRefId,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatusId,EditedBy,EditedOn,StatusId",Exclude = "AddedBy,AddedOn")] PrintJobs printJobs)
        {
            if (ModelState.IsValid)
            {
                DbContext.Entry(printJobs).State = EntityState.Modified;
                DbContext.Entry(printJobs).Property(uco => uco.AddedBy).IsModified = false;
                DbContext.Entry(printJobs).Property(uco => uco.AddedOn).IsModified = false;
                DbContext.SaveChanges();
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
            PrintJobs printJobs = DbContext.PrintJobs.Find(id);
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
                PrintJobs printJobs = DbContext.PrintJobs.Find(id);
                printJobs.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                DbContext.Entry(printJobs).State = EntityState.Modified;
                //db.Users.Remove(users);
                DbContext.SaveChanges();
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
            var printCost = DbContext.PrintCosts.Where(c => c.PrintCostId== PrintCostId);
            
            return Json(printCost, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetPrinterProperties(string PrinterSelected)
        {
            var printerList = MemoryCache.Default.Get(Common.Constants.PrinterListName) as Dictionary<string, PrinterDetails>;
            var printerprops = printerList[PrinterSelected];
           return Json(printerprops, JsonRequestBehavior.AllowGet);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
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

        public object Constatnts { get; private set; }

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
            UserDocs userDocs = DbContext.UserDocs.Find(UserDocId);
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

        public int getTotalPagesByDoc(string filename)
        {
            var printSettings = new PrintFileSettings()
            {
               FilePath = filename
            };
            var printHelper = new PrintHelper();

            return Int32.Parse(printHelper.GetPrintFileTotalPages(printSettings));
        }
    }
}
