using SmartPrint.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using SmartPrint.Controllers.Base;
using SmartPrint.Helpers.User;
using SmartPrint.ViewModels;

namespace SmartPrint.Controllers
{
    public class UserDocsController : SmartPrintBaseController
    {
        MainDbContext DbContext;
        UserHelper _userHelper;

        public UserDocsController() : this(new MainDbContext())
        {

        }
        public UserDocsController(MainDbContext dbContext):this(dbContext, new UserHelper(dbContext))
        {

        }
        public UserDocsController(MainDbContext dbContext, UserHelper userHelper)
        {
            DbContext = dbContext;
            _userHelper = userHelper;
        }
        
        // GET: UserDocs
        public ActionResult Index(string SearchTerm = "", int pageNo = 1, int pageSize = 10)
        {
            IEnumerable<UserDocs> resultsToConsider = DbContext.UserDocs;
            if (!IsUserAdmin())
            {
                resultsToConsider = resultsToConsider.Where(x => x.UserId == GetLoggedInUserId());
            }
            else
            {
                if (SearchTerm.Trim() != string.Empty && IsUserAdmin())
                {
                    var userIds = _userHelper.GetAllUsers().Where(x => x.SearchName.Contains(SearchTerm.ToLower())).Select(x => x.UserId).ToList();
                    resultsToConsider = resultsToConsider.Where(x => userIds.Contains(x.UserId));
                }
            }
            var resultFromDb = resultsToConsider.OrderByDescending(x => x.DocCreatedDate).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            var resultToSend = resultFromDb.Select(x => new UserDocsViewModel(x, _userHelper));
            ViewBag.SearchTerm = SearchTerm;
            return View(resultToSend);
        }
        [HttpPost]
        public ActionResult Index(string SearchTerm = "")
        {
            return Index(SearchTerm, 1, 10);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDocsViewModel userDocs)
        {
            var allErrors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            var isModelPartiallyValid = true;
            if (allErrors.Count > 0)
            {
                isModelPartiallyValid = allErrors.Count == 1 && allErrors[0] == "Only pdf file is allowed.";
            }
            //userDocs.PostedFile = PostedFile;
            if (ModelState.IsValid || isModelPartiallyValid)
            {
                if (userDocs.PostedFile.ContentLength > 0)
                {
                    userDocs.DocumentExtension = Path.GetExtension(userDocs.PostedFile.FileName);
                    userDocs.DocumentFileName = $"{Guid.NewGuid().ToString()}{userDocs.DocumentExtension}";
                    userDocs.DocumentFilePath = Path.Combine(Server.MapPath("~/UserFileUploads"), userDocs.DocumentFileName);
                    userDocs.PostedFile.SaveAs(userDocs.DocumentFilePath);
                    
                    var dbObjectToCreate = userDocs.GetDbObjectToCreate(GetLoggedInUserId());

                    DbContext.UserDocs.Add(dbObjectToCreate);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(userDocs);
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
