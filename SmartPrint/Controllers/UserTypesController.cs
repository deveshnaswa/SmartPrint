﻿using System;
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
    public class UserTypesController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: UserTypes
        public ActionResult Index()
        {
            return View(db.UserTypes.ToList());
        }

        // GET: UserTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypes userTypes = db.UserTypes.Find(id);
            if (userTypes == null)
            {
                return HttpNotFound();
            }
            return View(userTypes);
        }

        // GET: UserTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserTypeId,UserType,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] UserTypes userTypes)
        {
            if (ModelState.IsValid)
            {
                db.UserTypes.Add(userTypes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTypes);
        }

        // GET: UserTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypes userTypes = db.UserTypes.Find(id);
            if (userTypes == null)
            {
                return HttpNotFound();
            }
            return View(userTypes);
        }

        // POST: UserTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserTypeId,UserType,AddedBy,AddedOn,EditedBy,EditedOn,RowStatus")] UserTypes userTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTypes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTypes);
        }

        // GET: UserTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypes userTypes = db.UserTypes.Find(id);
            if (userTypes == null)
            {
                return HttpNotFound();
            }
            return View(userTypes);
        }

        // POST: UserTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                UserTypes userTypes = db.UserTypes.Find(id);
                userTypes.RowStatus = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(userTypes).State = EntityState.Modified;
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
