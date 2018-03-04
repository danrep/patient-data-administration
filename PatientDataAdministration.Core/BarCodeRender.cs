using System;
using System.Drawing.Imaging;
using System.IO;
using ZXing;
using ZXing.Common;

namespace PatientDataAdministration.Core
{
    public static class BarCodeRender
    {
        public static string GeneratePdf417BarCode(string dataString)
        {
            var data = new byte[0];

            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.PDF_417,
                Options = new EncodingOptions { Width = 200, Height = 50, Margin = 5 }
            };
            var imgBitmap = writer.Write(dataString);
            using (var stream = new MemoryStream())
            {
                imgBitmap.Save(stream, ImageFormat.Png);
                data = stream.ToArray();
            }

            return Convert.ToBase64String(data);
        }

        public static string ComputePdf417Data(string pdf417Data)
        {
            return string.Empty;
        }
    }
}