using SmartPrint.Common.Enums;
using SmartPrint.Helpers;
using SmartPrint.Helpers.User;
using SmartPrint.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SmartPrint.ViewModels
{
    public class UserDocsViewModel
    {
        public UserDocsViewModel()
        {

        }
        public UserDocsViewModel(UserDocs data, UserHelper userHelper)
        {
            DocumentId = data.DocId;
            DocumentName = data.DocName;
            DocumentTypeId = data.DocTypeId;
            DocumentExtension = data.DocExt;
            DocumentFileName = data.DocFileName;
            DocumentFilePath = data.DocFilePath;
            UserId = data.UserId;
            DocumentCreationDate = data.DocCreatedDate;
            DocumentStatus = data.StatusId;
            NameOfUser = userHelper.GetNameOfUser(UserId);
        }
        public UserDocs GetDbObjectToCreate(int userId)
        {
            var now = DateTimeHelper.GetTimeStamp();
            var dataToReturn = new UserDocs()
            {
                DocName = DocumentName,
                DocExt = DocumentExtension,
                DocFileName = DocumentFileName,
                DocFilePath = DocumentFilePath,
                UserId = userId,

                DocTypeId = (int)DocumentType.Pdf,
                DocCreatedDate = now,
                StatusId = (int)RecordStatus.Active,
                AddedOn = now,
                AddedBy = userId,
                EditedOn = now,
                EditedBy = userId
            };
            return dataToReturn;
        }
        
        [Key]
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Please specify a name for document")]
        [DisplayName("Document Name")]
        public string DocumentName { get; set; }

        [DisplayName("Document Type")]
        public int DocumentTypeId { get; set; }

        public string DocumentExtension { get; set; }

        [DisplayName("Document File Name")]
        public string DocumentFileName { get; set; }

        [DisplayName("Document File Path")]
        public string DocumentFilePath { get; set; }

        [DisplayName("Document Owner")]
        public int UserId { get; set; }

        [DisplayName("Creation Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DocumentCreationDate { get; set; }

        [DisplayName("Row Status")]
        public int DocumentStatus { get; set; }
        

        [DisplayName("User Name")]
        public string NameOfUser { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please select file.")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.pdf)$", ErrorMessage = "Only pdf file is allowed.")]
        [DisplayName("File")]
        public HttpPostedFileBase PostedFile { get; set; }
    }
}