using System;
using System.Management;

namespace SmartPrint.CustomLibaries
{
    public class PrintJobWatcher
    {
        //Actual code generated from WMI Code Generator


        public void WMIReceiveEvent()
        {
            try
            {
                WqlEventQuery query = new WqlEventQuery(
                    "SELECT * FROM __InstanceCreationEvent WITHIN 0.01 WHERE TargetInstance ISA 'Win32_PrintJob'");

                ManagementEventWatcher watcher = new ManagementEventWatcher(query);
                Console.WriteLine("Waiting for an event...");

                watcher.EventArrived +=
                    new EventArrivedEventHandler(
                        HandleEvent);

                // Start listening for events
                watcher.Start();

                // Do something while waiting for events
                System.Threading.Thread.Sleep(10000);

                // Stop listening for events
                watcher.Stop();
                return;
            }
            catch (ManagementException err)
            {
                Console.Write("An error occurred while trying to receive an event: " + err.Message);
            }
        }

        private void HandleEvent(object sender,
            EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent.Properties["TargetInstance"].Value;
            Console.WriteLine(e.NewEvent.GetPropertyValue("JobId"));
            Console.WriteLine(e.NewEvent.GetPropertyValue("JobStatus"));
            Console.WriteLine(e.NewEvent.GetPropertyValue("Name"));

            Console.WriteLine("__InstanceCreationEvent event occurred.");
        }

        public static void Main()
        {
            PrintJobWatcher receiveEvent = new PrintJobWatcher();
            return;
        }

        
    }
}

 