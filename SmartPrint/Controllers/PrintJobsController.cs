using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartPrint;
using SmartPrint.Models;

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
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrintJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,UserId,DocId,DocName,DocType,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatus,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] PrintJobs printJobs)
        {
            if (ModelState.IsValid)
            {
                db.PrintJobs.Add(printJobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printJobs);
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
            return View(printJobs);
        }

        // POST: PrintJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,UserId,DocId,DocName,DocType,DocExt,DocFileNameOnServer,DocFilePath,DocTotalPages,PrintcostId,MonoPages,ColorPages,IsColor,IsDuplex,IsCollate,UnitCost,MonoUnitcost,ColorUnitcost,UnitItem,JobRemarks,PagesFrom,PagesTo,NumCopies,TotalPageCount,TotalPageCost,CreditUsed,JobError,JobErrorRemarks,PrinterName,PrinterPath,JobStatus,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] PrintJobs printJobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printJobs).State = EntityState.Modified;
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
                printJobs.RowStatus = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
