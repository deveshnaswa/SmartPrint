using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartPrint.Models
{
    public class UserTypes : ITrackable
    {

        /*
         *   [UserTypeId] INT           IDENTITY (0, 1) NOT NULL,
    [UserType]   NVARCHAR (50) NOT NULL,
    [AddedBy]    INT           NOT NULL,
    [AddedOn]    DATETIME      NULL,
    [EditedBy]   INT           NOT NULL,
    [EditedOn]   DATETIME      NULL,
    [RowStatus]  INT           NOT NULL,
    */

        [Key]
        public int UserTypeId { get; set; }
        [Required]
        public string UserType { get; set; }
        public int AddedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }
        public int EditedBy { get; set; }
        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        public DateTime EditedOn { get; set; }
        public int RowStatus { get; set; }

    }
}
 