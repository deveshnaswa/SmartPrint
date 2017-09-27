using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class TTypes
    {
        [Key]
        public int TxnTypeId{ get; set; }

        public string TxnTypeName { get; set; }
    }
}