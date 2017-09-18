using SmartPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Controllers
{
    public class DocumentController : Controller
    {

        private MainDbContext db = new MainDbContext();
        /// <summary>
        /// The library.
        /// </summary>
        private readonly ILibrary _library = new Library(MvcApplication.GetDocumentsDirectory());
        /// <summary>
        /// The names of the documents within the library
        /// </summary>
        private string[] _documents;

        /// <summary>
        /// This private method populates a string array with the library documents name.
        /// </summary>
        private void PopulateLibrary()
        {
            _documents = new string[_library.GetDocumentCount()];
            for (int i = 0; i < _library.GetDocumentCount(); i++)
            {
                _documents[i] = _library.GetDocumentName(i);
            }
        }

        /// <summary>
        /// The Index method implements the controller entry point. 
        /// The library is populated with the documents and the names of the documents within the list are sent
        /// to the View via the ViewBag.
        /// 
        /// Please note the demonstration is limited to a fixed list. The list is populated on startup. Any modification
        /// within the library will not impact the list of documents displayed within the view.
        /// </summary>
        // GET: Default// GET: Document
        public ActionResult Index(int ? UserDocId)
        {
           

            if (UserDocId == null)
            {
               // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                UserDocId = 2;
            }
            UserDocs userDocs= db.UserDocs.Find(UserDocId);
            //PopulateLibrary();
            ViewBag.Library = _documents;
            if (userDocs != null)
            {
                ViewBag.Document = new Document(userDocs.DocFileName, userDocs.DocFilePath);
            }
            return View();
            //return View("/Views/Document/DocumentPreview.cshtml");
        }

        /// <summary>
        /// The Index method with two parameters implements the HTTP POST action.
        /// The HTTP POST occurs when a document has been clicked for display (through a form post).
        /// 
        /// The ViewBag is updated with the list of documents (unchanged) and the document to be displayed.
        /// </summary>
        /// <param name="id">The document id that has been selected for display.</param>
        /// <param name="hdnScrollPos">The vertical scrollbar position to restore.</param>
        [HttpPost]
        public ActionResult Index(int id, int hdnScrollPos)
        {
            PopulateLibrary();
            ViewBag.ScrollPos = hdnScrollPos;
            ViewBag.Library = _documents;
            ViewBag.Document = new Document(_library.GetDocumentName(id), _library.GetDocumentPath(id));
            return View();
        }


    }
}