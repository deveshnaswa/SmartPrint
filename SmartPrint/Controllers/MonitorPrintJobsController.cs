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
using SmartPrint.Common.Enums;

namespace SmartPrint.Controllers
{
    public class MonitorPrintJobsController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: PrintJobs
        public ActionResult Index()
        {
            var results = db.PrintJobs.Where(o => (o.JobStatusId == (int)PrintJobStatus.Processing));//.Where(o => (o.PrintJobQueueRefId!= 0));

            foreach (var Job in results)
            {
                var printerName = Job.PrinterName;
                var printjobRefid = Job.PrintJobQueueRefId;
                var printJobId = Job.JobId;
                var documentName = Job.DocFileNameOnServer;

                // get status from print job queue win32_pintjobs
                var jobQueueStatus = GetPrintJobStatus(printerName,printjobRefid,documentName);


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





        public static string GetPrintJobStatus(string printerName,int jobRefId,string fileName)
        {
           //StringCollection printJobCollection = new StringCollection();
            var jobStatus = string.Empty;
            var jobName = $"{printerName}, {jobRefId}";
            //string searchQuery = "SELECT * FROM Win32_PrintJob WHERE JobId='" + jobRefId + "' and Document = '"+fileName+"'";
            string searchQuery = $"SELECT * FROM Win32_PrintJob WHERE Name = '{jobName}'" ;
            var searchPrintJobs = new ManagementObjectSearcher(searchQuery);
            var prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                var printerJob = string.Empty;
                jobStatus = prntJob.Properties["JobStatus"].Value.ToString();
            }
            return jobStatus;
        }

        public ActionResult GetAllJobs()
        {
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            var searchPrintJobs = new ManagementObjectSearcher(searchQuery);
            var prntJobCollection = searchPrintJobs.Get();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}