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
    public class DocTypes : ITrackable
    {
        [Key]
        public int DocTypeId { get; set; }
        [DisplayName("Document Type")]
        public string DocType { get; set; }

        [DisplayName("Document Extension")]
        public string DocExt { get; set; }
        [DisplayName("Icon")]
        public string DocIcon { get; set; }
        [DisplayName("Is Active")]
        public int IsActive { get; set; }

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
        // public virtual RecordStatus RecordStatus { get; set; }
    }
}