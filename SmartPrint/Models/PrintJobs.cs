using System;
using System.Collections.Generic;
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
        public string DocName { get; set; }
        public int DocType { get; set; }
        public string DocExt { get; set; }
        public string DocFileNameOnServer { get; set; }
        public string DocFilePath { get; set; }
        public int DocTotalPages { get; set; }
        public int PrintcostId { get; set; }
        public int MonoPages { get; set; }
        public int ColorPages { get; set; }
        public int IsColor { get; set; }
        public int IsDuplex { get; set; }
        public int IsCollate { get; set; }
        public decimal UnitCost { get; set; }
        public decimal MonoUnitcost { get; set; }
        public decimal ColorUnitcost { get; set; }
        public int UnitItem { get; set; }
        public string JobRemarks { get; set; }
        public int PagesFrom { get; set; }
        public int PagesTo { get; set; }
        public int NumCopies { get; set; }
        public int TotalPageCount { get; set; }
        public decimal TotalPageCost { get; set; }
        public decimal CreditUsed { get; set; }
        public string JobError { get; set; }
        public string JobErrorRemarks { get; set; }
        public string PrinterName { get; set; }
        public string PrinterPath { get; set; }
        public int JobStatus { get; set; }
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }
        public int RowStatus { get; set; }

        //public virtual RecordStatus RecordStatus { get; set; }
    }
}