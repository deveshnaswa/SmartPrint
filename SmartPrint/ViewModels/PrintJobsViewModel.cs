using SmartPrint.Helpers.User;
using SmartPrint.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using SmartPrint.Helpers;
using SmartPrint.Common.Enums;

namespace SmartPrint.ViewModels
{
    public class PrintJobsViewModel
    {
        public PrintJobsViewModel()
        {

        }
        public PrintJobsViewModel(PrintJobs data, UserHelper userHelper)
        {
            JobId = data.JobId;
            UserId = data.UserId;
            NameOfUser = userHelper.GetNameOfUser(UserId);
            DocumentId = data.DocId;
            DocumentName = data.DocName;
            DocumentTotalPages = data.DocTotalPages;
            PrintcostId = data.PrintcostId;
            MonoPages = data.MonoPages;
            ColorPages = data.ColorPages;
            IsColor = data.IsColor;
            IsDuplex = data.IsDuplex;
            IsCollate = data.IsCollate;
            UnitCost = data.UnitCost;
            MonoUnitcost = data.MonoUnitcost;
            ColorUnitcost = data.ColorUnitcost;
            UnitItem = data.UnitItem;
            JobRemarks = data.JobRemarks;
            PagesFrom = data.PagesFrom;
            PagesTo = data.PagesTo;
            NumCopies = data.NumCopies;
            TotalPageCount = data.TotalPageCount;
            TotalPageCost = data.TotalPageCost;
            JobError = data.JobError;
            JobErrorRemarks = data.JobErrorRemarks;
            PrinterName = data.PrinterName;
            Status = data.StatusId;

        }
        [Key]
        public int JobId { get; set; }
        public int UserId { get; set; }
        public int DocumentId { get; set; }
        [DisplayName("Document")]
        public string DocumentName { get; set; }
        public int DocumentTotalPages { get; set; }
        [DisplayName("Printing Type")]
        public int PrintcostId { get; set; }
        [DisplayName("Mono Color Pages")]
        public int MonoPages { get; set; }
        [DisplayName("Color Pages")]
        public int ColorPages { get; set; }
        [DisplayName("Color")]
        public bool IsColor { get; set; }
        [DisplayName("Duplex")]
        public bool IsDuplex { get; set; }
        [DisplayName("Collate")]
        public bool IsCollate { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MonoUnitcost { get; set; }
        public decimal ColorUnitcost { get; set; }
        public int UnitItem { get; set; }
        public string JobRemarks { get; set; }
        [DisplayName("Page From")]
        public int PagesFrom { get; set; }
        [DisplayName("Page To")]
        public int PagesTo { get; set; }
        [DisplayName("Copies")]
        public int NumCopies { get; set; }
        [DisplayName("Total Pages")]
        public int TotalPageCount { get; set; }
        [DisplayName("Cost")]

        public decimal TotalPageCost { get; set; }
        public string JobError { get; set; }
        public string JobErrorRemarks { get; set; }
        [Required(ErrorMessage = "Please select a printer")]
        [DisplayName("Printer")]
        public string PrinterName { get; set; }
        [DisplayName("Status")]
        public int Status { get; set; }
        [DisplayName("User Name")]
        public string NameOfUser { get; set; }

        public PrintJobs GetDbObjectToCreate(UserDocs documentToPrint, int referenceJobId, int userId)
        {
            var now = DateTimeHelper.GetTimeStamp();
            var dataToReturn = new PrintJobs()
            {
                JobId = JobId,
                UserId = UserId,
                DocId = DocumentId,
                DocName = DocumentName,
                DocTotalPages = DocumentTotalPages,
                PrintcostId = PrintcostId,
                MonoPages = MonoPages,
                ColorPages = ColorPages,
                IsColor = IsColor,
                IsDuplex = IsDuplex,
                IsCollate = IsCollate,
                UnitCost = UnitCost,
                MonoUnitcost = MonoUnitcost,
                ColorUnitcost = ColorUnitcost,
                UnitItem = UnitItem,
                JobRemarks = JobRemarks,
                PagesFrom = PagesFrom,
                PagesTo = PagesTo,
                NumCopies = NumCopies,
                TotalPageCount = TotalPageCount,
                TotalPageCost = TotalPageCost,
                JobError = JobError,
                JobErrorRemarks = JobErrorRemarks,
                PrinterName = PrinterName,
                AddedBy = userId,
                EditedBy = userId,
                AddedOn = now,
                EditedOn = now,
                StatusId = (int)RecordStatus.Active,
                CreditUsed = TotalPageCost,
                DocExt = documentToPrint.DocExt,
                DocFileNameOnServer = documentToPrint.DocFileName,
                DocFilePath = documentToPrint.DocFilePath,
                DocTypeId = documentToPrint.DocTypeId,
                JobStatusId = (int)PrintJobStatus.Processing,
                PrintJobQueueRefId = referenceJobId,
                PrinterPath = PrinterName
            };
            return dataToReturn;
        }
    }
}