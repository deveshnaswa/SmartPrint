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
    public class UserDocsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: UserDocs
        public ActionResult Index()
        {
            return View(db.UserDocs.ToList());
        }

        // GET: UserDocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocs userDocs = db.UserDocs.Find(id);
            if (userDocs == null)
            {
                return HttpNotFound();
            }
            return View(userDocs);
        }

        // GET: UserDocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocId,DocName,DocType,DocExt,DocFileName,gDocFilePath,UserId,DocCreateDate,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] UserDocs userDocs)
        {
            if (ModelState.IsValid)
            {
                db.UserDocs.Add(userDocs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDocs);
        }

        // GET: UserDocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocs userDocs = db.UserDocs.Find(id);
            if (userDocs == null)
            {
                return HttpNotFound();
            }
            return View(userDocs);
        }

        // POST: UserDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocId,DocName,DocType,DocExt,DocFileName,gDocFilePath,UserId,DocCreateDate,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] UserDocs userDocs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDocs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDocs);
        }

        // GET: UserDocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDocs userDocs = db.UserDocs.Find(id);
            if (userDocs == null)
            {
                return HttpNotFound();
            }
            return View(userDocs);
        }

        // POST: UserDocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                UserDocs userDocs = db.UserDocs.Find(id);
                userDocs.RowStatus = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(userDocs).State = EntityState.Modified;
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
