using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string UserPass { get; set; }


        [HiddenInput(DisplayValue = false)]

        public int UserTypeId { get; set; }

        public string UserCode { get; set; }

        public string UserPhone { get; set; }

        public int IsActive { get; set; }

        public int AddedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }

        public int EditedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EditedOn { get; set; }

        public int RowStatus { get; set; }

    }
}