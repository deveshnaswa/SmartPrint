using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
           // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");
           // ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName");

            return View();
        }

        // POST: DocTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocTypeId,DocType,DocExt,DocIcon,IsActive,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] DocTypes docTypes)
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

            //ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName", docTypes.StatusId);
           // ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName", users.UStatusId);
            return View(docTypes);
        }

        // POST: DocTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocTypeId,DocType,DocExt,DocIcon,IsActive,EditedBy,EditedOn,StatusId",Exclude = "AddedBy,AddedOn")] DocTypes docTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(docTypes).State = EntityState.Modified;
                db.Entry(docTypes).Property(uco => uco.AddedBy).IsModified = false;
                db.Entry(docTypes).Property(uco => uco.AddedOn).IsModified = false;

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

            try
            {
                DocTypes docTypes = db.DocTypes.Find(id);
                docTypes.StatusId= 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(docTypes).State = EntityState.Modified;
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
