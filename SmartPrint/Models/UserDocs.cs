using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartPrint.Models
{
    public class UserDocs
    {
        /*
    [DocId]          INT           IDENTITY (0, 1) NOT NULL,
    [DocName]        NVARCHAR (50) NULL,
    [DocType]        NVARCHAR (50) NULL,
    [DocExt]         NVARCHAR (50) NULL,
    [DocFileName]    NVARCHAR (50) NULL,
    [DocFilePath]    NVARCHAR (50) NULL,
    [UserId]         INT           NOT NULL,
    [DocCreatedDate] DATETIME      NOT NULL,
    [AddedBy]        INT           NOT NULL,
    [AddedOn]        DATETIME      NULL,
    [EditedBy]       INT           NOT NULL,
    [EditedOn]       DATETIME      NULL,
    [RowStatus]      INT           NOT NULL
         * */
        [Key]
        public int DocId { get; set; }

        public string DocName { get; set; }

        public string DocType { get; set; }

        public string DocExt { get; set; }



    }
}