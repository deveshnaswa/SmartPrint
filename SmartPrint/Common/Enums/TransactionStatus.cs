using System.ComponentModel;

namespace SmartPrint.Common.Enums
{
    public enum TransactionStatus
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Success")]
        Success = 2,
        [Description("Error")]
        Error = 3
    }

}