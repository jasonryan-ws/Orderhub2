using Microsoft.Reporting.NETCore;
using Utilities.Barcodes;
using Utilities.Reporting.Components;

namespace Utilities.Reporting.Models
{
    public class ProductLabelReport : Report
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string AdditionalInfo { get; set; }
        public Alignment Alignment { get; set; }
        public Size LabelSize { get; set; }
        public LabelType LabelType { get; set; }

        public LocalReport GetReport()
        {
            var report = new LocalReport();
            report.ReportEmbeddedResource = "Utilities.Reporting.Reports.Letter.rdlc";
            if (LabelSize == Size.Inch_3x1)
            {
                if (LabelType == LabelType.Standard)
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Standard_3x1.rdlc";
                else if (LabelType == LabelType.Hybrid)
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Hybrid_3x1.rdlc";
                else if (LabelType == LabelType.Box)
                {
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Box_3x1.rdlc";
                    report.SetParameters(new ReportParameter("txtProductCodeBig", Tool.GetLastCharacters(ProductCode, 6)));
                }
            }
            else if (LabelSize == Size.Inch_2_25x1)
            {
                if (LabelType == LabelType.Standard)
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Standard_2.25x1.rdlc";
                else if (LabelType == LabelType.Hybrid)
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Hybrid_2.25x1.rdlc";
                else if (LabelType == LabelType.Box)
                {
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Box_2.25x1.rdlc";
                    report.SetParameters(new ReportParameter("txtProductCodeBig", Tool.GetLastCharacters(ProductCode, 6)));
                }
            }
            else if (LabelSize == Size.Inch_2_25x1_25)
            {
                if (LabelType == LabelType.Standard)
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Standard_2.25x1.25.rdlc";
                else if (LabelType == LabelType.Hybrid)
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Hybrid_2.25x1.25.rdlc";
                else if (LabelType == LabelType.Box)
                {
                    report.ReportEmbeddedResource = "Utilities.Reporting.Reports.ProductLabel_Box_2.25x1.25.rdlc";
                    report.SetParameters(new ReportParameter("txtProductCodeBig", Tool.GetLastCharacters(ProductCode, 6)));
                }
            }
            report.SetParameters(new ReportParameter("txtProductName", ProductName.Trim()));
            report.SetParameters(new ReportParameter("txtProductBarcode", Code128.StringToBarcode(ProductCode.Trim())));
            report.SetParameters(new ReportParameter("txtProductCode", ProductCode.Trim().ToUpper())); ;
            report.SetParameters(new ReportParameter("txtAdditionalInfo", string.IsNullOrWhiteSpace(AdditionalInfo) ? string.Empty : AdditionalInfo.Trim()));
            report.SetParameters(new ReportParameter("txtAlignment", Alignment.ToString()));
            report.Refresh();

            return report;

        }

        public void Print(int copies = 1)
        {
            var report = GetReport();
            AutoPrint.PrintLocalReport(report, "ProductLabel", PrinterName, (short)copies);
        }

        public byte[] GetBytes()
        {
            var report = GetReport();
            return Tool.ToBytesImage(report);
        }
    }
}
