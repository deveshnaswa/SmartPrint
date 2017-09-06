using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class Configuration
    {

        /*
         [ConfigID]  INT           IDENTITY (1, 1) NOT NULL,
         [Attribute] NVARCHAR (50) NOT NULL,
         [Data]      NVARCHAR (50) NOT NULL,
         */

        [Key]
        public int ConfigId { get; set; }
        public string Attribute { get; set; }
        public string data { get; set; }

    }
}