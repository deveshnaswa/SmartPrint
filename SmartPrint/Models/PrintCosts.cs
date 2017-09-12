using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Models
{
    public class PrintCosts: ITrackable
    {
        [Key]
        public int PrintCostId { get; set; }
        public string Name { get; set; }
        public Decimal Width { get; set; }
        public Decimal Height { get; set; }
        public Decimal MonoCostPerPage { get; set; }
        public Decimal ColorCostPerPage { get; set; }
        public int IsActive { get; set; }
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }
        public int RowStatus { get; set; }
    }
}