using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class UStatus
    {
        [Key]
        public int UStatusId { get; set; }
        public string UStatusName { get; set; }
    }
}