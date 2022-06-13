using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utilities.Barcodes
{
    public static class QRCode
    {
        public static string StringToQRCode(string value)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(value, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            string paramValue;
            using (var b = qrCodeImage)
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Png);
                    paramValue = Convert.ToBase64String(ms.ToArray());
                }
            }

            return paramValue;
        }
    }
}
