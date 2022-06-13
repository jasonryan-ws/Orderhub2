using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.NETCore;
using Utilities.Reporting.Components;

namespace Utilities.Reporting.Models
{
    public enum FileType { PDF, PNG }
    public class Report
    {
        public string PrinterName { get; set; }
        public Size Size { get; set; }
    }
}
