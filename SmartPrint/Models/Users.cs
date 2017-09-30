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

    public class Users : ITrackable
    {
        [Key]
        public int UserId { get; set; }
        [DisplayName("First Name")]
        public string FName { get; set; }
        [DisplayName("Last Name")]
        public string LName { get; set; }

        [Required]
        [DisplayName("Email: (Username)")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string UserPass { get; set; }


        [HiddenInput(DisplayValue = false)]

        public int UserTypeId { get; set; }
        [DisplayName("Role")]
        public virtual UserTypes UserTypes{ get; set; }

        [DisplayName("User Code")]
        public string UserCode { get; set; }

        [DisplayName("Phone #")]
        public string UserPhone { get; set; }

        [DisplayName("Status")]
        public int UStatusId { get; set; }
        //public virtual UStatus UStatus { get; set; }

        [DisplayName("Added By")]
        public int AddedBy { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Added On")]
        [Column(TypeName = "DateTime2")]
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
        //public virtual RecordStatus RecordStatus{ get; set; }

    }
}