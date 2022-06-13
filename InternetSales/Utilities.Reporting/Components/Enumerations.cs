using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Reporting.Components
{
    public enum Alignment { Left, Center, Right }
    public enum Size
    {
        [Description("3in x 1in")]
        Inch_3x1,
        [Description("2.25in x 1.25in")]
        Inch_2_25x1_25,
        [Description("2.25in x 1in")]
        Inch_2_25x1
    }
    public enum LabelType { Standard, Hybrid, Box }
}
