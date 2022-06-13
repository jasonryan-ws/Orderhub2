using Microsoft.Reporting.NETCore;
using Utilities.Barcodes;
using Utilities.Reporting.Components;

namespace Utilities.Reporting.Models
{
    public class OrderTagReport : Report
    {
        public string OrderNumber { get; set; }
        public string FullNameAndCompany { get; set; }
        public string FullAddress { get; set; }
        public string SolidBoxText { get; set; }
        public string OutlinedBoxText { get; set; }

        public LocalReport GetReport()
        {
            var report = new LocalReport();
            if (Size == Size.Inch_2_25x1_25)
                report.ReportEmbeddedResource = "Utilities.Reporting.Reports.OrderLabelQRCode_2.25x1.25.rdlc";
            else if (Size == Size.Inch_2_25x1)
                report.ReportEmbeddedResource = "Utilities.Reporting.Reports.OrderLabelQRCode_2.25x1.rdlc";
            else
                report.ReportEmbeddedResource = "Utilities.Reporting.Reports.OrderLabelQRCode_3x1.rdlc";
            report.SetParameters(new ReportParameter("txtOrderNumber", OrderNumber));
            report.SetParameters(new ReportParameter("txtOrderNumberQRCode", QRCode.StringToQRCode(OrderNumber)));
            report.SetParameters(new ReportParameter("txtName", FullNameAndCompany.ToUpper()));
            report.SetParameters(new ReportParameter("txtLocation", FullAddress.ToUpper()));
            report.SetParameters(new ReportParameter("txtCodeInDefaultBox", OutlinedBoxText));
            report.SetParameters(new ReportParameter("txtCodeInSolidBox", SolidBoxText));

            report.Refresh();
            return report;
        }

        public void Print(int copies = 1)
        {
            var report = GetReport();
            AutoPrint.PrintLocalReport(report, $"OrderLabel-{OrderNumber}", PrinterName, (short)copies);
        }

        public byte[] GetBytesImage()
        {
            var report = GetReport();
            return Tool.ToBytesImage(report);
        }

        public void Save(string path)
        {
            Tool.SaveReport(GetReport(), path);
        }
    }
}
