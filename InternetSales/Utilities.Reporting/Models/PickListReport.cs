using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.NETCore;
using Utilities.Barcodes;
using Utilities.Reporting.Components;
using Utilities.Reporting.DataSets;

namespace Utilities.Reporting.Models
{
    public class PickListReport : Report
    {
        public List<PickItem> PickItems { get; set; } = new List<PickItem>();

        public DataTable PickItemTable
        {
            get
            {
                var table = new PickListDataSet.PickItemDataTable();
                foreach (var i in PickItems)
                {
                    var row = table.NewRow();
                    row["Line"] = i.Line;
                    row["SKU"] = i.SKU;
                    row["QRCode"] = QRCode.StringToQRCode(i.SKU);
                    row["Barcode"] = Code128.StringToBarcode(i.SKU);
                    row["PickQty"] = i.PickQty;
                    row["ProductName"] = i.ProductName;
                    row["OrderCount"] = i.OrderCount;
                    row["Bins"] = i.Bins;
                    table.Rows.Add(row);
                }
                return table;
            }

        }

        public LocalReport GetReportQR()
        {
            var report = new LocalReport();

            report.EnableExternalImages = true;
            report.ReportEmbeddedResource = "Utilities.Reporting.Reports.PickListQRCode_8.5x11.rdlc";
            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("PickItemTable", PickItemTable));
            report.Refresh();
            return report;
        }

        public LocalReport GetReport()
        {
            var report = new LocalReport();

            report.EnableExternalImages = true;
            report.ReportEmbeddedResource = "Utilities.Reporting.Reports.PickList_8.5x11.rdlc";
            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("PickItemTable", PickItemTable));
            report.Refresh();
            return report;
        }

        public void Print(int copies = 1)
        {
            AutoPrint.PrintLocalReport(GetReport(), $"Picklist", PrinterName, (short)copies);
        }

        public void PrintQR(int copies = 1)
        {
            AutoPrint.PrintLocalReport(GetReportQR(), $"Picklist", PrinterName, (short)copies);
        }

        public byte[] GetBytes()
        {
            return Tool.ToBytesImage(GetReport());
        }

        public byte[] GetBytesQR()
        {
            return Tool.ToBytesImage(GetReportQR());
        }
    }
}
