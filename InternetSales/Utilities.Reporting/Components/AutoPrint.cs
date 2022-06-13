using System.Drawing.Printing;
using Microsoft.Reporting.NETCore;

namespace Utilities.Reporting.Components
{
    public static class AutoPrint
    {
        public static void PrintLocalReport(LocalReport report, string documentName, string printerName, short numberOfCopies)
        {
            var autoprintme = new AutoPrintCls(report);
            var setting = new PageSettings();
            var originalDefaultPrinterName = setting.PrinterSettings.PrinterName;
            SystemPrinter.SetDefaultPrinter(printerName);
            autoprintme.PrinterSettings.Copies = numberOfCopies;
            autoprintme.PrintController = new StandardPrintController();
            autoprintme.DocumentName = documentName;
            autoprintme.Print();
            SystemPrinter.SetDefaultPrinter(originalDefaultPrinterName);
        }
    }
}
