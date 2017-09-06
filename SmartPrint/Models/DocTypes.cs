using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class DocTypes
    {
        /*
    [DocTypeId] INT           IDENTITY (0, 1) NOT NULL,
    [DocType]   NVARCHAR (50) NOT NULL,
    [DocExt]    NVARCHAR (50) NOT NULL,
    [DocIcon]   NVARCHAR (50) NULL,
    [AddedBy]   INT           NOT NULL,
    [AddedOn]   DATETIME      NULL,
    [EditedBy]  INT           NOT NULL,
    [EditedOn]  DATETIME      NULL,
    [RowStatus] INT           NOT NULL,
         */

         [Key]
         [Required]
        public int DocTypeId { get; set; }
        [Required]
        public string DocType { get; set; }
        [Required]
        public string DocExt { get; set; }
        public string DocIcon { get; set; }
        public int AddedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }
        [DataType(DataType.DateTime)] 
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EeditedOn { get; set; }
        public int RowStatus { get; set; }


    }
}