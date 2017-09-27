using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class UserTxns: ITrackable
    {
        [Key]
        public int TxnId { get; set; }

        [DisplayName("User Name")]
        public int UserId { get; set; }

    
        [DisplayName("Transaction Type")]
        public int TxnTypeId { get; set; }
        [DisplayName("Transaction Type")]
        public virtual TTypes TTypes {get;set;}

        [DisplayName("Transaction Amount")]
        public decimal TxnAmount { get; set; }

        [DisplayName("Transaction Date Time")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime TxnDateTime { get; set; }

        [DisplayName("Transaction Balance Amount")]
        public decimal TxnBalance { get; set; }

        [DisplayName("Reference Print Job")]
        public int TxnRefJobId { get; set; }

        [DisplayName("Transaction Status")]
        public int TxnStatusId { get; set; }

        [DisplayName("Transaction Status")]
        public virtual TxnStatus TxnStatus { get; set; }
       
        [DisplayName("Added By")]
        public int AddedBy { get; set; }

        [DisplayName("Added On")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }

        [DisplayName("Edited By")]
        public int EditedBy { get; set; }

        [DisplayName("Edited On")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }

        [DisplayName("Record Status")]
        public int StatusId { get; set; }

        [DisplayName("Record Status")]
        public virtual RStatus RStatus { get; set; }
        
    }
}