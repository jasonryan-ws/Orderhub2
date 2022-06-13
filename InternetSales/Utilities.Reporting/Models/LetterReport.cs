using Microsoft.Reporting.NETCore;
using Utilities.Reporting.Components;

namespace Utilities.Reporting.Models
{
    public class LetterReport : Report
    {
        public string Greeting { get; set; }
        public string Body { get; set; }
        public string Closing { get; set; }
        public string Signature { get; set; }

        public LocalReport GetReport()
        {
            var report = new LocalReport();
            report.ReportEmbeddedResource = "Utilities.Reporting.Reports.Letter.rdlc";

            report.SetParameters(new ReportParameter("Greeting", Greeting));
            report.SetParameters(new ReportParameter("Body", Body));
            report.SetParameters(new ReportParameter("Closing", Closing));
            report.SetParameters(new ReportParameter("Signature", Signature));
            report.Refresh();
            return report;

        }

        public void Print(int copies = 1)
        {
            AutoPrint.PrintLocalReport(GetReport(), "Letter", PrinterName, (short)copies);
        }

        public  byte[] GetBytes()
        {
            return Tool.ToBytesImage(GetReport());
        }
    }
}
