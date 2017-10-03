using PdfiumViewer;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Management;

namespace SmartPrint.Helpers
{
    public class PrintHelper
    {
        public void PrintFileAsPdf(string path,string pdfFilePath)
        {
            PrintFile(path, "Microsoft Print to PDF", pdfFilePath);
        }

        public void PrintFile(string path, string printerName, string outputFileName = "")
        {
            var printerDetails = GetPrinterDetails(printerName);

            string flagNoSplashScreen = "/s /o";
            string flagOpenMinimized = "/h";
            var flagPrintFileToPrinter = string.Format("/t \"{0}\"", printerName);
            if (outputFileName != "")
            {
                flagPrintFileToPrinter = string.Format("/t \"{0}\" \"{1}\"", outputFileName, printerName);
            }
            
            var args = string.Format("{0} {1} {2}",
                       flagNoSplashScreen, flagOpenMinimized, flagPrintFileToPrinter);

            if (printerDetails != null)
            {
                Process printJob = new Process();
                printJob.StartInfo.FileName = path;
                printJob.StartInfo.UseShellExecute = true;
                printJob.StartInfo.Verb = "printto";
                printJob.StartInfo.CreateNoWindow = true;
                printJob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //printJob.StartInfo.Arguments = "\"" + printerDetails.Path + "\"";// + " " + printerExtraParameters;
                printJob.StartInfo.Arguments = args;// + " " + printerExtraParameters;
                printJob.StartInfo.WorkingDirectory = Path.GetDirectoryName(path);
                printJob.Start();
                
            }
            
        }

        public ManagementObject GetPrinterDetails(string PrinterSelected)
        {
            ManagementObject result = null;
            ObjectQuery oquery =
                new ObjectQuery("SELECT * FROM Win32_Printer WHERE Name = '" +
                                                  PrinterSelected.Replace("\\", "\\\\") + "'");

            ManagementObjectSearcher mosearcher =
                new ManagementObjectSearcher(oquery);

            ManagementObjectCollection printers = mosearcher.Get();
            foreach (ManagementObject printer in printers)
            {
                result = printer;
                break;
            }
            return result;
        }



        public string GetPrintFileTotalPages(PrintFileSettings settings)
        {
            string totalPageCount = "";
            try
            {


                var document = PdfDocument.Load(settings.FilePath);
                    totalPageCount = document.PageCount.ToString();


                return totalPageCount;
            }
            catch (Exception ex)
            {
                return "error";
            }
        }


        public string PrintFile(PrintFileSettings settings)
        {
            string MyJobId = "";
            try
            {
                if (settings.IsDuplex)
                {
                    settings.Duplex = Duplex.Default;
                }
               

                // Create the printer settings for our printer
                var printerSettings = new PrinterSettings
                {
                    PrinterName = settings.PrinterName,
                    Copies = settings.Copies,
                   
                    FromPage = settings.StartPage,
                    ToPage = settings.EndPage
                    
                };
                
                // Create our page settings for the paper size selected
                var pageSettings = new PageSettings(printerSettings)
                {
                    Margins = new Margins(0, 0, 0, 0),
                };
                foreach (PaperSize paperSize in printerSettings.PaperSizes)
                {
                    if (paperSize.PaperName == settings.PaperName)
                    {
                        pageSettings.PaperSize = paperSize;
                        break;
                    }
                }

                // Now print the PDF document
                using (var document = PdfDocument.Load(settings.FilePath))
                {
                    using (var printDocument = document.CreatePrintDocument())
                    {
                        //printDocument.BeginPrint += new PrintEventHandler(oyo);

                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.DocumentName = Path.GetFileName(settings.FilePath);
                        printDocument.PrintController = new StandardPrintController();
                        //printDocument.
                        printDocument.EndPrint += PrintDocument_EndPrint;
                        if (printDocument.PrinterSettings.SupportsColor)
                        {
                            if (settings.IsDuplex)
                            {
                                printDocument.PrinterSettings.Duplex = Duplex.Vertical;
                            }


                            printDocument.DefaultPageSettings.Color = settings.IsColored;
                        }
                        printDocument.Print();

                     string   fname = Path.GetFileName(settings.FilePath);
                        //match document name and printer name within last 2 Sec and update the job id
                       MyJobId=  setRefJobId(fname,printerSettings.PrinterName);
                        
                    }
                }

                
                return MyJobId;
            }
            catch(Exception ex)
            {
                return "error";
            }
        }

        private void PrintDocument_EndPrint(object sender, PrintEventArgs e)
        {
            string abd = "cdf";
            //-		sender	{[PrintDocument 190d82a7-58d4-444e-9336-051bc6487258.pdf]}	object {PdfiumViewer.PdfPrintDocument}

        }

        private string setRefJobId(string fileName,string printerName )
        {

            string JobRefId = "";
            StringCollection printJobCollection = new StringCollection();

            // string searchQuery = "SELECT * FROM Win32_PrintJob";
            string searchQuery = "SELECT * FROM Win32_PrintJob WHERE DriverName='" + printerName+ "' and Document = '" + fileName + "'";

            ManagementObjectSearcher searchPrintJobs =new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                // System.String jobName = prntJob.Properties["JobStatus"].Value.ToString();
                System.String PrinterName = prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]
                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = PrinterName.Split(splitArr)[0];
                //string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                  /*  var jobStatus = string.Empty;
                    if (prntJob.Properties["JobStatus"] != null)
                    {
                        jobStatus = prntJob.Properties["JobStatus"].Value.ToString();
                    }
                    */
                    JobRefId = prntJob.Properties["JobId"].Value.ToString();

                    // update the printjobs table and update the status of the print job



                   // printJobCollection.Add(jobName);
                }
            }
            return JobRefId;
        }

        private void HandleEvent(object sender, EventArrivedEventArgs e)
        {
           
        }

    }
}