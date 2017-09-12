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
    public class PrintCostsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: PrintCosts
        public ActionResult Index()
        {
            return View(db.PrintCosts.ToList());
        }

        // GET: PrintCosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintCosts printCosts = db.PrintCosts.Find(id);
            if (printCosts == null)
            {
                return HttpNotFound();
            }
            return View(printCosts);
        }

        // GET: PrintCosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrintCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrintCostId,Name,Width,Height,MonoCostPerPage,ColorCostPerPage,IsActive,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] PrintCosts printCosts)
        {
            if (ModelState.IsValid)
            {
                db.PrintCosts.Add(printCosts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(printCosts);
        }

        // GET: PrintCosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintCosts printCosts = db.PrintCosts.Find(id);
            if (printCosts == null)
            {
                return HttpNotFound();
            }
            return View(printCosts);
        }

        // POST: PrintCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrintCostId,Name,Width,Height,MonoCostPerPage,ColorCostPerPage,IsActive,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] PrintCosts printCosts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(printCosts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(printCosts);
        }

        // GET: PrintCosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrintCosts printCosts = db.PrintCosts.Find(id);
            if (printCosts == null)
            {
                return HttpNotFound();
            }
            return View(printCosts);
        }

        // POST: PrintCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                PrintCosts printCosts = db.PrintCosts.Find(id);
                printCosts.RowStatus = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(printCosts).State = EntityState.Modified;
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
