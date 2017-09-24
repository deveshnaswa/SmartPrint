using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Management;
using System.Web.Mvc;

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



        public static SelectList GetPrinterList()
        {
            List<SelectListItem> myPrinters = new List<SelectListItem>();
            System.Management.ObjectQuery oquery =
                new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

            System.Management.ManagementObjectSearcher mosearcher =
                new System.Management.ManagementObjectSearcher(oquery);

            System.Management.ManagementObjectCollection printers = mosearcher.Get();

            // the code below tries to keep the setting for keepig the printjobs active in the print queue.

            /*
            foreach (ManagementObject printer in printers)
            {
                PropertyDataCollection printerProperties = printer.Properties;
                foreach (PropertyData property in printerProperties)
                {
                    if (property.Name == "KeepPrintedJobs")
                    {
                        printerProperties[property.Name].Value = true;
                    }
                }
                printer.Put();
            }

            */
            foreach (ManagementObject printer in printers)
            {
                /*var state = "(Available Online)";
                if (printer["WorkOffline"].ToString().ToLower().Equals("true"))
                {
                    state = "(Available Offline)";
                }
                */
                var emptyItem = new SelectListItem()
                {
                    Value = printer["Name"].ToString(),
                    Text = printer["Name"].ToString()
                    //+" "+ state
                    
                };

                myPrinters.Add(emptyItem);
            }

            SelectList objselectlist = new SelectList(myPrinters, "Value", "Text");
            return objselectlist;

        }


        public static List<PrinterDetails> GetPrinterPropertiesList(string PrinterSelected)
        {
            List<SelectListItem> myPrinters = new List<SelectListItem>();
            System.Management.ObjectQuery oquery =
                new System.Management.ObjectQuery("SELECT * FROM Win32_Printer WHERE Name = '" +
                                                  PrinterSelected.Replace("\\", "\\\\") + "'");

            System.Management.ManagementObjectSearcher mosearcher =
                new System.Management.ManagementObjectSearcher(oquery);

            System.Management.ManagementObjectCollection printers = mosearcher.Get();

            var result = new List<PrinterDetails>();
            foreach (ManagementObject printer in printers)
            {
                var thisPrinter = new Printer(printer);
                var printerToSend = new PrinterDetails(thisPrinter);
                result.Add(printerToSend);
            }

            return result;
        }


        public static StringCollection GetPrintJobsCollection(string printerName, int JobRefId, string fileName,int PrintJobId)
        {
            StringCollection printJobCollection = new StringCollection();
            // string searchQuery = "SELECT * FROM Win32_PrintJob";
            string searchQuery = "SELECT JobStatus,Name FROM Win32_PrintJob WHERE JobId='" + JobRefId + "' and Document = '" + fileName + "'";

            //string searchQuery = "SELECT * FROM Win32_PrintJob WHERE jobid=7";
            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs =
                new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
               // System.String jobName = prntJob.Properties["JobStatus"].Value.ToString();
                System.String PrinterName= prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]
                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = PrinterName.Split(splitArr)[0];
                //string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    System.String jobName = prntJob.Properties["JobStatus"].Value.ToString();

                    // update the printjobs table and update the status of the print job



                    printJobCollection.Add(jobName);
                }
            }
            return printJobCollection;
        }

        /*
        public static SelectList FetchPrinterProps(string PrinterSelected)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");
            ManagementObjectCollection collection = searcher.Get();

            var items = new SelectList <Printer>();
            foreach (ManagementObject obj in collection)
            {
                var item = new Printer();
                item.Attributes = (uint?)obj["Attributes"];
                item.Availability = (ushort?)obj["Availability"];
                item.AvailableJobSheets = (string[])obj["AvailableJobSheets"];
                item.AveragePagesPerMinute = (uint?)obj["AveragePagesPerMinute"];
                item.Capabilities = (ushort[])obj["Capabilities"];
                item.CapabilityDescriptions = (string[])obj["CapabilityDescriptions"];
                item.Caption = (string)obj["Caption"];
                item.CharSetsSupported = (string[])obj["CharSetsSupported"];
                item.Comment = (string)obj["Comment"];
                item.ConfigManagerErrorCode = (uint?)obj["ConfigManagerErrorCode"];
                item.ConfigManagerUserConfig = (bool?)obj["ConfigManagerUserConfig"];
                item.CreationClassName = (string)obj["CreationClassName"];
                item.CurrentCapabilities = (ushort[])obj["CurrentCapabilities"];
                item.CurrentCharSet = (string)obj["CurrentCharSet"];
                item.CurrentLanguage = (ushort?)obj["CurrentLanguage"];
                item.CurrentMimeType = (string)obj["CurrentMimeType"];
                item.CurrentNaturalLanguage = (string)obj["CurrentNaturalLanguage"];
                item.CurrentPaperType = (string)obj["CurrentPaperType"];
                item.Default = (bool?)obj["Default"];
                item.DefaultCapabilities = (ushort[])obj["DefaultCapabilities"];
                item.DefaultCopies = (uint?)obj["DefaultCopies"];
                item.DefaultLanguage = (ushort?)obj["DefaultLanguage"];
                item.DefaultMimeType = (string)obj["DefaultMimeType"];
                item.DefaultNumberUp = (uint?)obj["DefaultNumberUp"];
                item.DefaultPaperType = (string)obj["DefaultPaperType"];
                item.DefaultPriority = (uint?)obj["DefaultPriority"];
                item.Description = (string)obj["Description"];
                item.DetectedErrorState = (ushort?)obj["DetectedErrorState"];
                item.DeviceID = (string)obj["DeviceID"];
                item.Direct = (bool?)obj["Direct"];
                item.DoCompleteFirst = (bool?)obj["DoCompleteFirst"];
                item.DriverName = (string)obj["DriverName"];
                item.EnableBIDI = (bool?)obj["EnableBIDI"];
                item.EnableDevQueryPrint = (bool?)obj["EnableDevQueryPrint"];
                item.ErrorCleared = (bool?)obj["ErrorCleared"];
                item.ErrorDescription = (string)obj["ErrorDescription"];
                item.ErrorInformation = (string[])obj["ErrorInformation"];
                item.ExtendedDetectedErrorState = (ushort?)obj["ExtendedDetectedErrorState"];
                item.ExtendedPrinterStatus = (ushort?)obj["ExtendedPrinterStatus"];
                item.Hidden = (bool?)obj["Hidden"];
                item.HorizontalResolution = (uint?)obj["HorizontalResolution"];
                item.InstallDate = (DateTime?)obj["InstallDate"];
                item.JobCountSinceLastReset = (uint?)obj["JobCountSinceLastReset"];
                item.KeepPrintedJobs = (bool?)obj["KeepPrintedJobs"];
                item.LanguagesSupported = (ushort[])obj["LanguagesSupported"];
                item.LastErrorCode = (uint?)obj["LastErrorCode"];
                item.Local = (bool?)obj["Local"];
                item.Location = (string)obj["Location"];
                item.MarkingTechnology = (ushort?)obj["MarkingTechnology"];
                item.MaxCopies = (uint?)obj["MaxCopies"];
                item.MaxNumberUp = (uint?)obj["MaxNumberUp"];
                item.MaxSizeSupported = (uint?)obj["MaxSizeSupported"];
                item.MimeTypesSupported = (string[])obj["MimeTypesSupported"];
                item.Name = (string)obj["Name"];
                item.NaturalLanguagesSupported = (string[])obj["NaturalLanguagesSupported"];
                item.Network = (bool?)obj["Network"];
                item.PaperSizesSupported = (ushort[])obj["PaperSizesSupported"];
                item.PaperTypesAvailable = (string[])obj["PaperTypesAvailable"];
                item.Parameters = (string)obj["Parameters"];
                item.PNPDeviceID = (string)obj["PNPDeviceID"];
                item.PortName = (string)obj["PortName"];
                item.PowerManagementCapabilities = (ushort[])obj["PowerManagementCapabilities"];
                item.PowerManagementSupported = (bool?)obj["PowerManagementSupported"];
                item.PrinterPaperNames = (string[])obj["PrinterPaperNames"];
                item.PrinterState = (uint?)obj["PrinterState"];
                item.PrinterStatus = (ushort?)obj["PrinterStatus"];
                item.PrintJobDataType = (string)obj["PrintJobDataType"];
                item.PrintProcessor = (string)obj["PrintProcessor"];
                item.Priority = (uint?)obj["Priority"];
                item.Published = (bool?)obj["Published"];
                item.Queued = (bool?)obj["Queued"];
                item.RawOnly = (bool?)obj["RawOnly"];
                item.SeparatorFile = (string)obj["SeparatorFile"];
                item.ServerName = (string)obj["ServerName"];
                item.Shared = (bool?)obj["Shared"];
                item.ShareName = (string)obj["ShareName"];
                item.SpoolEnabled = (bool?)obj["SpoolEnabled"];
                item.StartTime = (DateTime?)obj["StartTime"];
                item.Status = (string)obj["Status"];
                item.StatusInfo = (ushort?)obj["StatusInfo"];
                item.SystemCreationClassName = (string)obj["SystemCreationClassName"];
                item.SystemName = (string)obj["SystemName"];
                item.TimeOfLastReset = (DateTime?)obj["TimeOfLastReset"];
                item.UntilTime = (DateTime?)obj["UntilTime"];
                item.VerticalResolution = (uint?)obj["VerticalResolution"];
                item.WorkOffline = (bool?)obj["WorkOffline"];

                items.Add(item);
            }
        }
        */
    }
}