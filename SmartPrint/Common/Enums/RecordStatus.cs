using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SmartPrint.Common.Enums
{
    public enum RecordStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Deleted")]
        Deleted = 2
    }
}