using System;
using System.Collections.Generic;
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

        public string DocName { get; set; }
        public string DocType { get; set; }
        public string DocExt { get; set; }
        public string DocFileName { get; set; }
        public string DocFilePath { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime DocCreatedDate { get; set; }
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime AddedOn { get; set; }
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime2")]
        public DateTime EditedOn { get; set; }
        public int StatusId { get; set; }
        public virtual RStatus RStatus { get; set; }
        // public virtual RecordStatus RecordStatus { get; set; }
    }
}