using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Management;

namespace SmartPrint.CustomLibaries
{

    public class PrinterDetails
    {
        public bool IsDuplex { get; set; }
        public bool IsColored { get; set; }
        public string PrinterName { get; set; }

        public string PrinterPath { get; set; }

        public PrinterDetails(Printer nativePrinter)
        {
            var capabilities = nativePrinter.Capabilities.ToList();
            IsDuplex = capabilities.Contains(3);
            IsColored = capabilities.Contains(2);
            PrinterName = nativePrinter.Name;
            PrinterPath = nativePrinter.Location;

        }
    }


    public class Printer
    {

        public Printer(ManagementObject printer)
        {
            Capabilities = (ushort[]) printer["Capabilities"];
            Name = printer["Name"].ToString();
           Location= printer["Name"].ToString();
        }

        public uint? Attributes;
        public ushort? Availability;
        public string[] AvailableJobSheets;
        public uint? AveragePagesPerMinute;
        public ushort[] Capabilities;
        public string[] CapabilityDescriptions;
        public string Caption;
        public string[] CharSetsSupported;
        public string Comment;
        public uint? ConfigManagerErrorCode;
        public bool? ConfigManagerUserConfig;
        public string CreationClassName;
        public ushort[] CurrentCapabilities;
        public string CurrentCharSet;
        public ushort? CurrentLanguage;
        public string CurrentMimeType;
        public string CurrentNaturalLanguage;
        public string CurrentPaperType;
        public bool? Default;
        public ushort[] DefaultCapabilities;
        public uint? DefaultCopies;
        public ushort? DefaultLanguage;
        public string DefaultMimeType;
        public uint? DefaultNumberUp;
        public string DefaultPaperType;
        public uint? DefaultPriority;
        public string Description;
        public ushort? DetectedErrorState;
        public string DeviceID;
        public bool? Direct;
        public bool? DoCompleteFirst;
        public string DriverName;
        public bool? EnableBIDI;
        public bool? EnableDevQueryPrint;
        public bool? ErrorCleared;
        public string ErrorDescription;
        public string[] ErrorInformation;
        public ushort? ExtendedDetectedErrorState;
        public ushort? ExtendedPrinterStatus;
        public bool? Hidden;
        public uint? HorizontalResolution;
        public DateTime? InstallDate;
        public uint? JobCountSinceLastReset;
        public bool? KeepPrintedJobs;
        public ushort[] LanguagesSupported;
        public uint? LastErrorCode;
        public bool? Local;
        public string Location;
        public ushort? MarkingTechnology;
        public uint? MaxCopies;
        public uint? MaxNumberUp;
        public uint? MaxSizeSupported;
        public string[] MimeTypesSupported;
        public string Name;
        public string[] NaturalLanguagesSupported;
        public bool? Network;
        public ushort[] PaperSizesSupported;
        public string[] PaperTypesAvailable;
        public string Parameters;
        public string PNPDeviceID;
        public string PortName;
        public ushort[] PowerManagementCapabilities;
        public bool? PowerManagementSupported;
        public string[] PrinterPaperNames;
        public uint? PrinterState;
        public ushort? PrinterStatus;
        public string PrintJobDataType;
        public string PrintProcessor;
        public uint? Priority;
        public bool? Published;
        public bool? Queued;
        public bool? RawOnly;
        public string SeparatorFile;
        public string ServerName;
        public bool? Shared;
        public string ShareName;
        public bool? SpoolEnabled;
        public DateTime? StartTime;
        public string Status;
        public ushort? StatusInfo;
        public string SystemCreationClassName;
        public string SystemName;
        public DateTime? TimeOfLastReset;
        public DateTime? UntilTime;
        public uint? VerticalResolution;
        public bool? WorkOffline;
        
    }

    public class PrinterHelper
    {
        public static Dictionary<string, PrinterDetails> GetPrintersWithProperties()
        {
            var result = new Dictionary<string, PrinterDetails>();

            var oquery = new ObjectQuery("SELECT * FROM Win32_Printer");
            var mosearcher = new ManagementObjectSearcher(oquery);
            var printers = mosearcher.Get();

            foreach (ManagementObject printer in printers)
            {
                var thisPrinter = new Printer(printer);
                var printerToSend = new PrinterDetails(thisPrinter);
                result.Add(printer["Name"].ToString(),printerToSend);
            }

            return result;
        }

        public static StringCollection GetPrintJobsCollection(string printerName, int JobRefId, string fileName, int PrintJobId)
        {
            var printJobCollection = new StringCollection();
            string searchQuery = "SELECT JobStatus,Name FROM Win32_PrintJob WHERE JobId='" + JobRefId + "' and Document = '" + fileName + "'";

            var searchPrintJobs = new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                var PrinterName = prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]
                var splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = PrinterName.Split(splitArr)[0];
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    var jobName = prntJob.Properties["JobStatus"].Value.ToString();
                    printJobCollection.Add(jobName);
                }
            }
            return printJobCollection;
        }
    }

}