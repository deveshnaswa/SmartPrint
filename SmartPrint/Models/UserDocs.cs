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
    public class UserDocs : ITrackable
    {
        [Key]
        public int DocId { get; set; }
        [DisplayName("Document Name")]

        public string DocName { get; set; }
        [DisplayName("Document Type")]
        public int DocTypeId { get; set; }

        public virtual DocTypes DocTypes { get; set; }
        [DisplayName("Extension Type")]
        public string DocExt { get; set; }
        [DisplayName("Document File Name")]
        public string DocFileName { get; set; }
        [DisplayName("Document File Path")]
        public string DocFilePath { get; set; }
        [DisplayName("Document Owner")]
        public int UserId { get; set; }

        public virtual Users Users { get; set; }
        [DisplayName("Uploaded Date Time")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime DocCreatedDate { get; set; }
        [DisplayName("Added By")]
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Added On")]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        [DisplayName("Edited By")]
        public int EditedBy { get; set; }
        [DisplayName("Edited On")]
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }
        [DisplayName("Row Status")]
        public int StatusId { get; set; }
        [DisplayName("Row Status")]
        public virtual RStatus RStatus { get; set; }
        // public virtual RecordStatus RecordStatus { get; set; }
    }
}