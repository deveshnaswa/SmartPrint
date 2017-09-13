using System;
using System.Collections.Generic;
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
        public int UserId { get; set; }
        public string TxnType { get; set; }
        public decimal TxnAmount { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime TxnDateTime { get; set; }
        public decimal TxnBalance { get; set; }
        public int TxnRefJobId { get; set; }
        public int TxnStatus { get; set; }
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }
        public int StatusId { get; set; }

        public virtual RStatus RStatus { get; set; }
        
    }
}