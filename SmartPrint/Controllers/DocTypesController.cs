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
    public class DocTypesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: DocTypes
        public ActionResult Index()
        {
            return View(db.DocTypes.ToList());
        }

        // GET: DocTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocTypes docTypes = db.DocTypes.Find(id);
            if (docTypes == null)
            {
                return HttpNotFound();
            }
            return View(docTypes);
        }

        // GET: DocTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocTypeId,DocType,DocExt,DocIcon,IsActive,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] DocTypes docTypes)
        {
            if (ModelState.IsValid)
            {
                db.DocTypes.Add(docTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(docTypes);
        }

        // GET: DocTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocTypes docTypes = db.DocTypes.Find(id);
            if (docTypes == null)
            {
                return HttpNotFound();
            }
            return View(docTypes);
        }

        // POST: DocTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocTypeId,DocType,DocExt,DocIcon,IsActive,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] DocTypes docTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(docTypes);
        }

        // GET: DocTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocTypes docTypes = db.DocTypes.Find(id);
            if (docTypes == null)
            {
                return HttpNotFound();
            }
            return View(docTypes);
        }

        // POST: DocTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocTypes docTypes = db.DocTypes.Find(id);
            db.DocTypes.Remove(docTypes);
            db.SaveChanges();
            return RedirectToAction("Index");
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
