using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class PrintCost
    {

        /*
    [PrintCostId]      INT          IDENTITY (1, 1) NOT NULL,
    [Name]             INT          NOT NULL,
    [Width]            DECIMAL (18) NOT NULL,
    [Height]           DECIMAL (18) NOT NULL,
    [MonoCostPerPage]  DECIMAL (18) NOT NULL,
    [ColorCostPerPage] DECIMAL (18) NOT NULL,
    [IsActive]         INT          NOT NULL,
    [AddedBy]          INT          NOT NULL,
    [AddedOn]          DATETIME     NULL,
    [EditedBy]         INT          NOT NULL,
    [EditedOn]         DATETIME     NULL,
    [RowStatus]        INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([PrintCostId] ASC)
         */

        [Key]
        [Required]
        public int PrintCostId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public decimal MonoCostPerPage { get; set; }

        public decimal ColorCostPerPage { get; set; }

        public int AddedBy { get; set; }

        public int IsActive { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedOn { get; set; }
        [DataType(DataType.DateTime)]
        public int EditedBy { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EeditedOn { get; set; }
        public int RowStatus { get; set; }
    }
}