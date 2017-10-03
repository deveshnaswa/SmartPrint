using SmartPrint.Common.Filters;
using SmartPrint.CustomLibraries;
using SmartPrint.Helpers.User;
using SmartPrint.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Web.Mvc;

namespace SmartPrint.Controllers
{
    public class UsersController : Controller
    {
        MainDbContext _dbContext;
        UserHelper _userHelper;

        public UsersController():this(new MainDbContext())   
        {

        }
        public UsersController(MainDbContext db):this(db,new UserHelper(db))
        {

        }

        public UsersController(MainDbContext db, UserHelper userHelper)
        {
            _dbContext = db;
            _userHelper = userHelper;
        }

        public ActionResult Index()
        {
            return View(_dbContext.Users.ToList());
        }
        public ActionResult IndexSearch(string id)
        {
            var result = _userHelper.SearchUsers(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchUsers(string query)
        {
            var result = _userHelper.SearchUsers(query).Select(x => new Autocomplete() {Id = x.UserId, Name = x.Name });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = _dbContext.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        [AdminAuthorizationFilter]
        public ActionResult Create()
        {
            ViewBag.UserTypeId = new SelectList(MemoryCache.Default.Get(Common.Constants.UserTypeListName) as Dictionary<int, string>, "Key", "Value");
            ViewBag.StatusId = new SelectList(MemoryCache.Default.Get(Common.Constants.RecordStatusListName) as Dictionary<int, string>, "Key", "Value");
            ViewBag.UStatusId = new SelectList(MemoryCache.Default.Get(Common.Constants.RecordStatusListName) as Dictionary<int, string>, "Key", "Value");

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FName,LName,UserEmail,UserPass,UserTypeId,UserCode,UserPhone,UStatusId,AddedBy,AddedOn,EditedBy,EditedOn,StatusId")] Users users)
        {
            if (ModelState.IsValid)
            {
                var encryptedPassword = CustomEnrypt.Encrypt(users.UserPass);
                users.UserPass = encryptedPassword;

                //users.RowStatus = 1;
                _dbContext.Users.Add(users);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = _dbContext.Users.Find(id);

            if (users == null)
            {
                return HttpNotFound();
            }
            var decryptPassword = CustomDecrypt.Decrypt(users.UserPass);
            users.UserPass = decryptPassword;
            ViewBag.UserTypeId = new SelectList(_dbContext.UserTypes, "UserTypeId", "UserType",users.UserTypeId);
            ViewBag.StatusId = new SelectList(MemoryCache.Default.Get(Common.Constants.RecordStatusListName) as Dictionary<int, string>, "Key", "Value",users.StatusId);
            ViewBag.UStatusId = new SelectList(MemoryCache.Default.Get(Common.Constants.RecordStatusListName) as Dictionary<int, string>, "Key", "Value", users.UStatusId);
           
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FName,LName,UserEmail,UserPass,UserTypeId,UserCode,UserPhone,UStatusId,EditedBy,EditedOn,StatusId", Exclude = "AddedBy,AddedOn")] Users users)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(users).State = EntityState.Modified;
                var encryptedPassword = CustomEnrypt.Encrypt(users.UserPass);
                users.UserPass = encryptedPassword;
                _dbContext.Entry(users).Property(uco => uco.AddedBy).IsModified = false;
                _dbContext.Entry(users).Property(uco => uco.AddedOn).IsModified = false;
                
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = _dbContext.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                Users users = _dbContext.Users.Find(id);
                users.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
                _dbContext.Entry(users).State = EntityState.Modified;
                //db.Users.Remove(users);
                _dbContext.SaveChanges();
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
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
