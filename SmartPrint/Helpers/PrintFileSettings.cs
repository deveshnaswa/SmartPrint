namespace SmartPrint.Helpers
{
    public class PrintFileSettings
    {
        public PrintFileSettings()
        {
            Copies = 1;
            PaperName = "A4";
            StartPage = 1;
        }
        public string FilePath { get; set; }
        public string PrinterName { get; set; }
        public short Copies { get; set; }
        public bool IsColored { get; set; }
        public bool IsDuplex { get; set; }
        public string PaperName { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
       public System.Drawing.Printing.Duplex Duplex { get; set; }

    }
}