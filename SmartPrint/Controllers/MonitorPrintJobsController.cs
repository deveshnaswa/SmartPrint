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
            // get all from printjobs table
         

            var results = db.PrintJobs.Where(o => (o.JobStatusId== 0))
                                        .Where(o => (o.PrintJobQueueRefId!= 0));

            foreach (var Job in results)
            {
                var printerName = Job.PrinterName;
                var printjobRefid = Job.PrintJobQueueRefId;
                var printJobId = Job.JobId;
                var documentName = Job.DocFileNameOnServer;

                // get status from print job queue win32_pintjobs
                var jobQueueStatus = GetPrintJobsCollection(printerName,printjobRefid,documentName);


                using (var db = new MainDbContext())
                {
                    var result = db.PrintJobs.SingleOrDefault(b => b.JobId== printJobId);
                    if (result != null)
                    {
                        result.JobError= "Some new value";
                        result.JobStatusId= 0;
                        db.SaveChanges();
                    }
                }

                ////MainDbContext db= new MainDbContext();
                //PrintJobs uPrintjobs=
                //    db.PrintJobs.Single(c => c.JobId == printJobId);

                //uPrintjobs.JobError = jobQueueStatus.ToString();

                
                //// Submit the changes to the database.
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e);
                //    // Provide for exceptions.
                //}
            }
            return View(db.PrintJobs.ToList());
        }





        public static string GetPrintJobsCollection(string printerName,int jobRefId,string fileName)
        {
           //StringCollection printJobCollection = new StringCollection();
            var jobStatus = string.Empty;
            string searchQuery = "SELECT * FROM Win32_PrintJob WHERE JobId='" + jobRefId + "' and Document = '"+fileName+"'";

           ManagementObjectSearcher searchPrintJobs =
                new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                var printerJob = string.Empty;

                // check criteria first : if the printJob property [Name] is not null

                if (prntJob.Properties["Name"].Value != null)
                {
                    printerJob = prntJob.Properties["Name"].Value.ToString();
                    char[] splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    string prnterName = printerJob.Split(splitArr)[0];

                    // check second criteria and match if PrintJob=>Printer =JobQueue=>Printer
                    if (String.Compare(prnterName, printerName, true) == 0)
                    {
                        
                       
                        if (prntJob.Properties["JobStatus"].Value != null)
                        {
                            jobStatus = prntJob.Properties["JobStatus"].Value.ToString();
                        }
                    }

                }
            }
            return jobStatus;
        }
    }
}