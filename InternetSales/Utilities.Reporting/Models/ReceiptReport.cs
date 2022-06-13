using System.Collections.Generic;
using System.Data;
using Microsoft.Reporting.NETCore;
using Utilities.Barcodes;
using Utilities.Reporting.Components;
using Utilities.Reporting.DataSets;

namespace Utilities.Reporting.Models
{
    public class ReceiptReport : Report
    {
        public string CompanyLogoPath { get; set; }
        public string CompanyAddress { get; set; }
        public string HeadNote { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContactInfo { get; set; }
        public string ShipMethod { get; set; }
        public string UnitTotal { get; set; }
        public string Subtotal { get; set; }
        public string ShipCharge { get; set; }
        public string Tax { get; set; }
        public string Discount { get; set; }
        public string TotalAmount { get; set; }

        public string Comments { get; set; }
        public string Message { get; set; }
        public Alignment MessageAlignment { get; set; }
        public string FootNote1 { get; set; }
        public string FootNote2 { get; set; }
        public string FootNote3 { get; set; }
        public string FootNote4 { get; set; }
        public string DatePrinted { get; set; }

        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public List<Charge> Charges { get; set; } = new List<Charge>();

        public DataTable OrderLineTable
        {
            get
            {
                var table = new ReceiptDataSet.OrderLineDataTable();
                foreach (var i in OrderLines)
                {
                    var row = table.NewRow();
                    row["Quantity"] = i.Quantity;
                    row["ItemName"] = i.ItemSKUandName;
                    row["UnitPrice"] = i.UnitPrice;
                    row["ExtendedPrice"] = i.ExtendedPrice;
                    table.Rows.Add(row);
                }

                return table;
            }
        }

        public DataTable ChargeTable
        {
            get
            {
                var table = new ReceiptDataSet.ChargeDataTable();
                foreach (var c in Charges)
                {
                    var row = table.NewRow();
                    row["ChargeName"] = c.Name;
                    row["Amount"] = c.Amount;
                    table.Rows.Add(row);
                }

                return table;
            }
        }

        public LocalReport GetReport()
        {
            var report = new LocalReport();

            report.EnableExternalImages = true;
            report.ReportEmbeddedResource = "Utilities.Reporting.Reports.Receipt_3xx.rdlc";
            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource("OrderLineTable", OrderLineTable));
            report.DataSources.Add(new ReportDataSource("ChargeTable", ChargeTable));
            report.SetParameters(new ReportParameter("CompanyAddress", CompanyAddress));
            report.SetParameters(new ReportParameter("HeadNote", HeadNote));
            report.SetParameters(new ReportParameter("CompanyLogoImageURL", $@"file:\{CompanyLogoPath}"));
            report.SetParameters(new ReportParameter("OrderNumber", OrderNumber));
            report.SetParameters(new ReportParameter("OrderNumberBarcode", Code128.StringToBarcode(OrderNumber)));
            report.SetParameters(new ReportParameter("OrderDate", OrderDate));
            report.SetParameters(new ReportParameter("OrderCode", OrderCode));
            report.SetParameters(new ReportParameter("ShipName", CustomerName));
            report.SetParameters(new ReportParameter("ShipAddress", CustomerAddress));
            report.SetParameters(new ReportParameter("ShipContact", CustomerContactInfo));
            report.SetParameters(new ReportParameter("UnitTotal", UnitTotal.ToString()));
            report.SetParameters(new ReportParameter("ShipMethod", ShipMethod));
            report.SetParameters(new ReportParameter("Subtotal", Subtotal));
            report.SetParameters(new ReportParameter("OrderTotal", TotalAmount));
            report.SetParameters(new ReportParameter("Comments", Comments));
            report.SetParameters(new ReportParameter("SellerMessage", Message));
            report.SetParameters(new ReportParameter("SellerMessageAlignment", MessageAlignment.ToString()));
            report.SetParameters(new ReportParameter("FootNote1", FootNote1));
            report.SetParameters(new ReportParameter("FootNote2", FootNote2));
            report.SetParameters(new ReportParameter("FootNote3", FootNote3));
            report.SetParameters(new ReportParameter("FootNote4", FootNote4));
            report.SetParameters(new ReportParameter("DatePrinted", DatePrinted.ToString()));
            report.Refresh();

            return report;
        }

        public void Print(int copies = 1)
        {
            AutoPrint.PrintLocalReport(GetReport(), $"Receipt-{OrderNumber}", PrinterName, (short)copies);
        }

        public void Save(string path)
        {
            Tool.SaveReport(GetReport(), path);
        }

        public byte[] GetBytesImage()
        {
            return Tool.ToBytesImage(GetReport());
        }
    }
}
