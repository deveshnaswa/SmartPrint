using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class RStatus
    {
        [Key]
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}