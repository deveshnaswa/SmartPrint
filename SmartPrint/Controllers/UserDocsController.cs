using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SmartPrint.Controllers
{
    public class UserDocsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: UserDocs
        public ActionResult Index()
        {

            ViewBag.DocTypeId = new SelectList(db.DocTypes, "DocTypeId", "DocType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserId");
            return View(db.UserDocs.ToList());
        }
        /*
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UserFileUploads"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
        */

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
            ViewBag.DocTypeId = new SelectList(db.DocTypes, "DocTypeId", "DocType");
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName");
            ViewBag.UserId = new SelectList(db.Users,"UserId","FName"+ " " +"LName");
           // ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName");

            return View();
        }

        // POST: UserDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDocs userDocs, HttpPostedFileBase file)
        {

            /*
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".pdf", ".zip", ".rar" };
                var checkextension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(checkextension))
                {
                    TempData["notice"] = "Select pdf or zip or rar less than 20Μ";
                }

                foreach (var itm in allowedExtensions)
                {
                    if (itm.Contains(checkextension))
                    {
                        db.announcement.Add(announcement);
                        dbo.SaveChanges();
                    }
                }

                if (file != null && file.ContentLength > 0)
                {
                    foreach (var itm in allowedExtensions)
                    {
                        if (itm.Contains(checkextension))
                        {
                            var extension = Path.GetExtension(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/AnnFiles/" + "announcement_" + announcement.anak_ID + extension));

                            //save File
                            file.SaveAs(path);

                            //prepere announcement
                            announcement.file = @"announcement_" + announcement.anak_ID + extension;


                            //Code for Save data to announcement.

                            db.SaveChanges();
                            TempData["notice"] = "OK! the file is uploaded";
                        }
                    }

                }
            }
        }
        */
            if (ModelState.IsValid)
            {

               
                if (file.ContentLength > 0)
                {
                    string _FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//Path.GetFileName(file.FileName);
                    string _FileExt = Path.GetExtension(file.FileName);
                    int _FileSize = file.ContentLength;

                    string _path = Path.Combine(Server.MapPath("~/UserFileUploads"), _FileName);

                        file.SaveAs(_path);
                    userDocs.DocName = userDocs.DocName;
                        userDocs.DocFileName = _FileName;
                        userDocs.DocCreatedDate = DateTime.Now;
                        userDocs.UserId= int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
                        userDocs.DocTypeId = userDocs.DocTypeId;
                        userDocs.DocExt = _FileExt;
                        userDocs.DocFilePath = _path;
                }

               // string path = Path.Combine(Server.MapPath("~/UserFileUploads"), Path.GetFileName(file.FileName));
                //file.SaveAs(path);
                
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
           // ViewBag.UserTypeId = new SelectList(db.UserTypes, "UserTypeId", "UserType", users.UserTypeId);
            ViewBag.StatusId = new SelectList(db.RStatus, "StatusId", "StatusName", userDocs.StatusId);
           // ViewBag.UStatusId = new SelectList(db.UStatus, "UStatusId", "UStatusName", users.UStatusId);

            return View(userDocs);
        }

        // POST: UserDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocId,DocName,DocTypeId,DocExt,DocFileName,DocFilePath,UserId,DocCreatedDate,EditedBy,EditedOn,StatusId",Exclude = "AddedBy,AddedOn")] UserDocs userDocs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDocs).State = EntityState.Modified;
                db.Entry(userDocs).Property(uco => uco.AddedBy).IsModified = false;
                db.Entry(userDocs).Property(uco => uco.AddedOn).IsModified = false;
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
                userDocs.StatusId = 0; // on delete setting up the row status column to 0 for softdelete. 1 is active
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
