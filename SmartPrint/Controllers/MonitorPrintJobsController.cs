using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartPrint.Models;
using System.Data.Entity;
using System.Management;
using SmartPrint.CustomLibaries;

namespace SmartPrint.Controllers
{
    public class MonitorPrintJobsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: PrintJobs
        public ActionResult Index()
        {
            // get all 
            var result = db.PrintJobs.Where(x => x.StatusId == 1).ToList();

            foreach (var Job in result)
            {
                var printerName = Job.PrinterName;
                var printjobRefid = Job.PrintJobQueueRefId;
                var printJobId = Job.PrintJobQueueRefId;
                var documentName = Job.DocFileNameOnServer;

                // get status from print job queue win32_pintjobs
                var JobQueueStatus = Printer.GetPrintJobsCollection(printerName,printjobRefid,documentName,printJobId);
               
                //update database table printjobs with the status for that table
            }
            return View(db.PrintJobs.ToList());
        }





        public static string GetPrintJobsCollection(string printerName,int jobRefId,string fileName)
        {
            StringCollection printJobCollection = new StringCollection();

            string searchQuery = "SELECT * FROM Win32_PrintJob WHERE JobId='" + jobRefId + "' and Document = '"+fileName+"'";

            //string query = "SELECT * FROM Win32_PrintJob WHERE JobID='" + lstJobs.Text + "'";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs =
                new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]
                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    printJobCollection.Add(documentName);
                }
            }
            return "";
        }
    }
}