using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;
using System.Web.Mvc;

namespace SmartPrint.CustomLibaries
{
    public class Printer
    {
        public static SelectList  GetPrinterList()
        {
            List<SelectListItem> myPrinters = new List<SelectListItem>();
            System.Management.ObjectQuery oquery =
                new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

            System.Management.ManagementObjectSearcher mosearcher =
                new System.Management.ManagementObjectSearcher(oquery);

            System.Management.ManagementObjectCollection printers = mosearcher.Get();

            
            foreach (ManagementObject printer in printers)
            {
                var emptyItem = new SelectListItem()
                {
                    Value = printer["Name"].ToString(),
                    Text = printer["Name"].ToString()
                };

                myPrinters.Add(emptyItem);
            }

            SelectList objselectlist = new SelectList(myPrinters,"Value","Text");
            return objselectlist;

        }

        
    }
}