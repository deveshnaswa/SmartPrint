using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Runtime.Caching;
using SmartPrint.Helpers.User;
using SmartPrint.ViewModels;
using SmartPrint.Common.Enums;
using SmartPrint.Controllers.Base;
using System.Linq.Expressions;
using SmartPrint.Helpers;

namespace SmartPrint.Controllers
{
    public class UserTxnsController : SmartPrintBaseController
    {
        public UserTxnsController() : this(new MainDbContext())
        {

        }
        public UserTxnsController(MainDbContext dbContext)
        {
            DbContext = dbContext;
        }
        private MainDbContext DbContext;

        // GET: UserTxns
        public ActionResult Index(string userName = "", int pageNo = 1, int pageSize = 10)
        {
            IEnumerable<UserTxns> resultsToConsider = DbContext.UserTxns;
            var userHelper = new UserHelper(DbContext);
            if (userName.Trim() != string.Empty)
            {
                var userIds = userHelper.GetAllUsers().Where(x => x.SearchName.Contains(userName.ToLower())).Select(x => x.UserId).ToList();
                resultsToConsider = resultsToConsider.Where(x => userIds.Contains(x.UserId));
            }
            var resultFromDb = resultsToConsider.OrderByDescending(x => x.TxnDateTime).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            var resultToSend = resultFromDb.Select(x => new UserTransactionViewModel(x, userHelper,EnumInfo.GetList<TransactionType>()));
            return View(resultToSend);
        }

        
        // GET: UserTxns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserTxns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTransactionViewModel userTransaction)
        {
            if (ModelState.IsValid)
            {
                userTransaction.TransactionTypeId = (int)TransactionType.Credit;
                userTransaction.TransactionStatus = (int)TransactionStatus.Success;
                userTransaction.TransactionBalance = CalTxnBal(userTransaction.UserId, userTransaction.TransactionAmount, userTransaction.TransactionTypeId);
                var dbObjectToSave = userTransaction.GetDbObjectToCreate(GetLoggedInUserId());
                DbContext.UserTxns.Add(dbObjectToSave);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            var userHelper = new UserHelper(DbContext);
            ViewBag.Users = new SelectList(userHelper.GetAllUsers(), "UserId", "Name");
            return View(userTransaction);
        }

        public ActionResult GetTxnBalance(int userId)
        {

            var printcosts = DbContext.UserTxns
                .Where(tx => tx.UserId == userId)   // Filter
                .OrderByDescending(tx => tx.TxnId) // prioritet is still here - order by it
                .FirstOrDefault();  // Now grab the transaction balance
            return Json(printcosts, JsonRequestBehavior.AllowGet);
        }

        private decimal CalTxnBal(int userId,decimal txnAmt,int txnType)
        {
            decimal jTxnBalance=0;
            var result = DbContext.UserTxns
                .Where(tx => tx.UserId == userId)   // Filter
                .OrderByDescending(tx => tx.TxnId) // prioritet is still here - order by it
                .FirstOrDefault();  // Now grab the transaction balance

            if (result != null)
            {
                jTxnBalance = result.TxnBalance;
            }

            if (txnType == (int)TransactionType.Debit)
            {
                jTxnBalance = jTxnBalance - txnAmt;
            }
            else
            {
                jTxnBalance = jTxnBalance + txnAmt;
            }

            return jTxnBalance;
            
        }

        // GET: UserTxns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userTransactionToEdit = DbContext.UserTxns.Find(id);
            if (userTransactionToEdit == null)
            {
                return HttpNotFound();
            }

            var userHelper = new UserHelper(DbContext);
            var viewModel = new UserTransactionViewModel(userTransactionToEdit, userHelper, EnumInfo.GetList<TransactionType>());
            return View(viewModel);
        }

        // POST: UserTxns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTransactionViewModel transactionDetails)
        {
            if (ModelState.IsValid)
            {
                var userTransactionToEdit = DbContext.UserTxns.Find(transactionDetails.TransactionId);
                if (userTransactionToEdit == null)
                {
                    return HttpNotFound();
                }

                transactionDetails.ChangeDbObjectForUpdate(userTransactionToEdit,GetLoggedInUserId(),DateTimeHelper.GetTimeStamp());
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            { 
                return View(transactionDetails);
            }
        }

        // GET: UserTxns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTxns userTxns = DbContext.UserTxns.Find(id);
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
                UserTxns userTxns = DbContext.UserTxns.Find(id);
                userTxns.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                DbContext.Entry(userTxns).State = EntityState.Modified;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
