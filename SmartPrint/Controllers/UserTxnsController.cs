using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SmartPrint.Controllers
{
    public class UserTxnsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: UserTxns
        public ActionResult Index()
        {
            return View(db.UserTxns.ToList());
        }

        // GET: UserTxns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = db.UserTxns.Find(id);
            if (userTxns == null)
            {
                return HttpNotFound();
            }
            return View(userTxns);
        }

        // GET: UserTxns/Create
        public ActionResult Create()
        {
            ViewBag.Users= new SelectList(db.Users, "UserId", "FName" );
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");
            //ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName");

            return View();
        }

        // POST: UserTxns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TxnId,UserId,TxnTypeId,TxnAmount,TxnDateTime,TxnBalance,TxnRefJobId,TxnStatus,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] UserTxns userTxns)
        {
            if (ModelState.IsValid)
            {
                db.UserTxns.Add(userTxns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTxns);
        }

        // GET: UserTxns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = db.UserTxns.Find(id);
            if (userTxns == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName",userTxns.StatusId);
            //ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName");

            return View(userTxns);
        }

        // POST: UserTxns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TxnId,UserId,TxnTypeId,TxnAmount,TxnDateTime,TxnBalance,TxnRefJobId,TxnStatus,EditedBy,EditedOn,StatusId",Exclude = "AddedBy,AddedOn")] UserTxns userTxns)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userTxns).State = EntityState.Modified;
                db.Entry(userTxns).Property(uco => uco.AddedBy).IsModified = false;
                db.Entry(userTxns).Property(uco => uco.AddedOn).IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTxns);
        }

        // GET: UserTxns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = db.UserTxns.Find(id);
            if (userTxns == null)
            {
                return HttpNotFound();
            }
            return View(userTxns);
        }

        // POST: UserTxns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                UserTxns userTxns = db.UserTxns.Find(id);
                userTxns.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                db.Entry(userTxns).State = EntityState.Modified;
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
