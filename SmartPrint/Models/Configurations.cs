using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class Configurations
    {
        [Key]
        public int ConfigId { get; set; }
        public string ConfigKey { get; set; }
        public string ConfigVal { get; set; }

    }
}