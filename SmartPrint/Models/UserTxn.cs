using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Models
{

    /*
     *   
    [TxnID]       INT           IDENTITY (0, 1) NOT NULL,
    [UserId]      INT           NOT NULL,
    [TxnType]     NVARCHAR (50) NOT NULL,
    [TxnAmount]   DECIMAL (18)  NOT NULL,
    [TxnDateTime] DATETIME      NOT NULL,
    [TxnBalance]  DECIMAL (18)  NOT NULL,
    [TxnRefJobId] INT           NOT NULL,
    [TxnStatus]   INT           NOT NULL,
    [AddedBy]     INT           NOT NULL,
    [AddedOn]     DATETIME      NULL,
    [EditedBy]    INT           NOT NULL,
    [EditedOn]    DATETIME      NULL,
    [RowStatus]   INT           NOT NULL,
    */
    public class UserTxn
    {
        [Key]
        public int TxnId { get; set; }

        public int UserId{ get; set; }

        [Required]
        public string TxnType { get; set; }

        [Required]
        public decimal TxnAmount { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime TxnDateTime { get; set; }

        public decimal TxnBalance{ get; set; }

        public int TxnRefJobId { get; set; }

        public int TxnStatus { get; set; }

        public int AddedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }

        public int EditedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EditedOn { get; set; }

        public int RowStatus { get; set; }

    }
}
 