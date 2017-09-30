using System.ComponentModel;

namespace SmartPrint.Common.Enums
{
    public enum TransactionType
    {
        [Description("Credit")]
        Credit = 1,
        [Description("Debit")]
        Debit = 2
    }

}