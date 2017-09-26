using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SmartPrint.Common.Enums
{
    public enum TransactionStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Deleted")]
        Deleted = 2
    }


    public enum TransactionType
    {
        [Description("Credit")]
        Credit = 1,
        [Description("Debit")]
        Debit= 2
    }


}