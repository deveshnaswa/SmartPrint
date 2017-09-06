using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Models
{
    public class PrintJob
    {
        [Key]
        public int JobId { get; set; }

        public int UserId { get; set; } 
        public int DocId { get; set; } 
        public string DocName { get; set; }

        public string DocType { get; set; } 

       public string DocExt { get; set;  } 

        public string DocFileNameOnServer { get; set; }

        public string DocFilePath{get;set;} 

        public int DocTotalPages{get;set;} //       INT NOT NULL,
        public int PrintCostId{get;set;} // INT NOT NULL,
        public int MonoPages{get;set;} // INT NOT NULL,
        public int ColorPages{get;set;} // INT NOT NULL,
        public int IsColor{get;set;} // INT NOT NULL,
        public int IsDuplex{get;set;} // INT NOT NULL,
        public int IsCollate{get;set;} // INT NOT NULL,
        public decimal UnitCost{get;set;} //            DECIMAL(18)  NOT NULL,

        public decimal MonoUnitCost{get;set;} //        DECIMAL(18)  NOT NULL,

        public decimal ColorUnitCost{get;set;} 

        public int UnitItem{get;set;} 
        public string JobRemarks{get;set;}
        public int PagesFrom{get;set;} 
        public int PagesTo{get;set;} 
        public int NumCopies{get;set;} 
        public int TotalPageCount{get;set;} 
        public decimal TotalPageCost{get;set;}

        public decimal CreditUsed{get;set;} 
        public string JobError{get;set;} 
        public string JobErrorRemarks{get;set;}
        public string PrinterName{get;set;}
        public string PrinterPath{get;set;}
        public string JobStatus{get;set;}
        public int AddedBy{get;set;}

        [DataType(DataType.DateTime)] 
        public DateTime AddedOn{get;set;} 
        public int EditedBy{get;set;}
        [DataType(DataType.DateTime)]
        public DateTime EditedOn { get; set; }            
        public int  RowStatus { get; set; }

    }
}