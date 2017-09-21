using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Paper Width")]
        public Decimal Width { get; set; }
        [DisplayName("Paper Height")]
        public Decimal Height { get; set; }
        [DisplayName("Mono Ink Cost Per Page")]
        public Decimal MonoCostPerPage { get; set; }
        [DisplayName("Color Ink Cost Per Page")]
        public Decimal ColorCostPerPage { get; set; }
        [DisplayName("Is Active")]
        public int IsActive { get; set; }
        [DisplayName("Added By")]
        public int AddedBy { get; set; }
        [DisplayName("Added By")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        [DisplayName("Edited By")]
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        [DisplayName("Edited On")]
        public DateTime EditedOn { get; set; }
        [DisplayName("Record Status")]
        public int StatusId { get; set; }
        [DisplayName("Record Status")]
        public virtual RStatus RStatus { get; set; }
        // public virtual RecordStatus RecordStatus { get; set; }
    }
}