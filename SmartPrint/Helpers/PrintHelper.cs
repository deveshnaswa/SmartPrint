using System.Diagnostics;
using System.Management;
using System.IO;
using System.Drawing.Printing;
using PdfiumViewer;
using System;

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

        public bool PrintFile(PrintFileSettings settings)
        {
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
                        printDocument.PrinterSettings = printerSettings;
                        printDocument.DefaultPageSettings = pageSettings;
                        printDocument.PrintController = new StandardPrintController();
                        if (printDocument.PrinterSettings.SupportsColor)
                        {
                            if (settings.IsColored)
                            {
                                printDocument.DefaultPageSettings.Color = true;
                            }
                            else
                            {
                                printDocument.DefaultPageSettings.Color = false;
                            }
                        }
                        printDocument.Print();
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}