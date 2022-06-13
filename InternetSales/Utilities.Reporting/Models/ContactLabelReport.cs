using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.NETCore;
using Utilities.Barcodes;
using Utilities.Reporting.Components;

namespace Utilities.Reporting.Models
{
    public class ContactLabelReport : Report
    {
        public string OrderNumber { get; set; }
        public string FullNameAndCompany { get; set; }
        public string FullContact { get; set; }
        public LocalReport GetReport()
        {
            var report = new LocalReport();
            report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ContactLabelQRCode_3x1.rdlc";
            report.SetParameters(new ReportParameter("txtOrderNumber", OrderNumber));
            report.SetParameters(new ReportParameter("txtCompleteQRCode", QRCode.StringToQRCode($"{"Order#: "}{OrderNumber}\n{FullNameAndCompany}\n{"Contact:\n"}{FullContact}")));
            report.SetParameters(new ReportParameter("txtName", FullNameAndCompany));
            report.SetParameters(new ReportParameter("txtPhoneEmail", FullContact)); ;
            report.Refresh();
            return report;
        }

        public void Print(int copies = 1)
        {
            var report = GetReport();
            AutoPrint.PrintLocalReport(report, $"ContactInfo-{OrderNumber}", PrinterName, (short)copies);
        }
        public byte[] GetBytes()
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
