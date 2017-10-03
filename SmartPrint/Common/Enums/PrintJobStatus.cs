using System.ComponentModel;

namespace SmartPrint.Common.Enums
{
    public enum PrintJobStatus
    {
        [Description("Processing")]
        Processing = 1,
        [Description("Failed")]
        Failed = 2,
        [Description("Succeeded")]
        Succeeded = 3
    }
    public enum PrintJobSystemStatus
    {
        [Description("Paused")]
        JOB_STATUS_PAUSED = 1,
        [Description("Error")]
        JOB_STATUS_ERROR = 2,
        [Description("Deleting")]
        JOB_STATUS_DELETING = 4,
        [Description("Spooling")]
        JOB_STATUS_SPOOLING = 8,
        [Description("Printing")]
        JOB_STATUS_PRINTING = 16,
        [Description("Offline")]
        JOB_STATUS_OFFLINE = 32,
        [Description("Paper Out")]
        JOB_STATUS_PAPEROUT = 64,
        [Description("Printed")]
        JOB_STATUS_PRINTED = 128,
        [Description("Deleted")]
        JOB_STATUS_DELETED = 256,
        [Description("Blocked")]
        JOB_STATUS_BLOCKED_DEVQ = 512,
        [Description("User Action Required")]
        JOB_STATUS_USER_INTERVENTION = 1024,
        [Description("Restart")]
        JOB_STATUS_RESTART = 2048
    }

}