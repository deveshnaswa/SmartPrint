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
    public class UserTypes : ITrackable
    {
        
        [Key]
        public int UserTypeId { get; set; }

        [DisplayName("User Type")]
        [Required]
        public string UserType { get; set; }

        [DisplayName("Added By")]
        public int AddedBy { get; set; }

        [DisplayName("Added On")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }

        [DisplayName("Edited By")]
        public int EditedBy { get; set; }

        [DisplayName("Edited On")]
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        public DateTime EditedOn { get; set; }

        [DisplayName("Record Status")]
        public int StatusId { get; set; }

        [DisplayName("Record Status")]
        public virtual RStatus RStatus { get; set; }


       

    }
}
 