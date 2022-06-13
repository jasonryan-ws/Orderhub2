using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.NETCore;
using Utilities.Reporting.Models;

namespace Utilities.Reporting.Components
{
    public static class Tool
    {
        public static byte[] ToBytesImage(LocalReport report)
        {
            try
            {
                var content = report.Render("IMAGE");
                var memstr = new MemoryStream(content);
                return memstr.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] ToBytesPDF(LocalReport report)
        {
            try
            {
                var content = report.Render(format: "PDF", deviceInfo: "");
                var memstr = new MemoryStream(content);
                return memstr.ToArray();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetLastCharacters(string text, int charCount = 5)
        {
            if (text.Length > charCount)
                return text.Substring(text.Length - charCount);
            else
                return text;
        }

        public static void SaveReport(LocalReport report, string path)
        {
            var extension = Path.GetExtension(path).ToLower();
            if (extension == ".pdf")
            {
                var bytes = report.Render("PDF");
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                var bytes = report.Render("IMAGE");
                using (var ms = new MemoryStream(bytes))
                {
                    var image = Image.FromStream(ms);
                    switch (extension)
                    {
                        case ".jpg":image.Save(path, ImageFormat.Jpeg);break;
                        case ".gif": image.Save(path, ImageFormat.Gif); break;
                        case ".bmp": image.Save(path, ImageFormat.Bmp); break;
                        default: image.Save(path, ImageFormat.Png); break;
                    } 
                }
            }
        }
    }
}
