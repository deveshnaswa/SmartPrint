using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Models
{
    public class PrintJobs : ITrackable
    {
        [Key]
        public int JobId { get; set; }


        public int UserId { get; set; }
        public int DocId { get; set; }

        [DisplayName("Document Name")]
        public string DocName { get; set; }
        public int DocTypeId { get; set; }
        public string DocExt { get; set; }
        public string DocFileNameOnServer { get; set; }
        public string DocFilePath { get; set; }
        public int DocTotalPages { get; set; }

        [DisplayName("Printing Type")]
        public int PrintcostId { get; set; }
        [DisplayName("Mono Color Pages")]
        public int MonoPages { get; set; }
        [DisplayName("Color Pages")]
        public int ColorPages { get; set; }
        [DisplayName("Color Printing")]
        public bool IsColor { get; set; }
        [DisplayName("Duplex Printing")]
        public bool IsDuplex { get; set; }
        [DisplayName("Collate printing")]
        public bool IsCollate { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MonoUnitcost { get; set; }
        public decimal ColorUnitcost { get; set; }
        public int UnitItem { get; set; }
        public string JobRemarks { get; set; }


        [DisplayName("Pages From")]
        public int PagesFrom { get; set; }


        [DisplayName("Pages To")]
        public int PagesTo { get; set; }

        [DisplayName("Copies To Print")]
        public int NumCopies { get; set; }

        [DisplayName("Total Page Count")]
        public int TotalPageCount { get; set; }

        [DisplayName("Total Printing Cost")]
        public decimal TotalPageCost { get; set; }

        [DisplayName("Credits Used")]
        public decimal CreditUsed { get; set; }
        public int PrintJobQueueRefId { get; set; }

        public string JobError { get; set; }
        public string JobErrorRemarks { get; set; }

        [DisplayName("Select Printer")]
        public string PrinterName { get; set; }
        public string PrinterPath { get; set; }
        public int JobStatusId { get; set; }
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }
        public int StatusId { get; set; }
        //public virtual RecordStatus RecordStatus { get; set; }
    }
}