using QRCoder;
using System.IO;
using ZXing;
using ZXing.Common;

namespace ArganaWeedApp.Services
{
    public class QRCodeService
    {
        public byte[] GenerateQRCode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Le texte ne peut pas être vide.", nameof(text));
            }

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                return qrCode.GetGraphic(20);
            }
        }

        /*
        public string DecodeQRCode(Stream qrCodeStream)
        {
            var reader = new BarcodeReaderGeneric
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions
                {
                    TryHarder = true
                }
            };

            using (var bitmap = new System.Drawing.Bitmap(qrCodeStream))
            {
                var luminanceSource = new ZXing.BitmapLuminanceSource(bitmap);
                var binarizer = new HybridBinarizer(luminanceSource);
                var binaryBitmap = new BinaryBitmap(binarizer);
                var result = reader.Decode(binaryBitmap);

                return result?.Text;
            }
        }*/
    }
}
