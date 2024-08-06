using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;

namespace ArganaWeedApp.Services
{
    public class PdfService
    {
        public byte[] GeneratePdf(string title, string content)
        {
            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var graphics = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 20, XFontStyle.Bold); // Utilisation de Bold pour le titre
                var contentFont = new XFont("Verdana", 12, XFontStyle.Regular); // Utilisation de Regular pour le contenu

                graphics.DrawString(title, font, XBrushes.Black, new XRect(0, 0, page.Width, 50), XStringFormats.TopCenter);
                graphics.DrawString(content, contentFont, XBrushes.Black, new XRect(40, 100, page.Width - 80, page.Height - 140), XStringFormats.TopLeft);

                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] GeneratePdf(string slug, string content, byte[] qrCodeImage)
        {
            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var graphics = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 28, XFontStyle.Bold); // Utilisation de Bold pour le titre
                var contentFont = new XFont("Verdana", 12, XFontStyle.Regular); // Utilisation de Regular pour le contenu
                var smallFont = new XFont("Verdana", 8, XFontStyle.Regular); // Utilisation de Regular pour le texte en dessous

                // Taille du QR code en cm
                double qrCodeSizeCm = 3.0;
                double qrCodeSizeInch = qrCodeSizeCm / 2.54;
                double qrCodeSizePoints = qrCodeSizeInch * 72;

                // Calcul des positions
                double qrCodeX = 40;
                double qrCodeY = 40;
                double textAboveQrX = qrCodeX;
                double textAboveQrY = qrCodeY - 20;
                double textBelowQrX = qrCodeX;
                double textBelowQrY = qrCodeY + qrCodeSizePoints + 5;

                // Dessiner le slug au-dessus du QR code
                graphics.DrawString(slug, contentFont, XBrushes.Black, new XRect(textAboveQrX, textAboveQrY, qrCodeSizePoints, 20), XStringFormats.Center);

                // Dessiner le QR code
                using (var qrImage = XImage.FromStream(() => new MemoryStream(qrCodeImage)))
                {
                    graphics.DrawImage(qrImage, qrCodeX, qrCodeY, qrCodeSizePoints, qrCodeSizePoints);
                }

                // Dessiner le texte en dessous du QR code
                graphics.DrawString("© ArganaWeed", smallFont, XBrushes.Black, new XRect(textBelowQrX, textBelowQrY, qrCodeSizePoints, 20), XStringFormats.Center);

                graphics.DrawString(content, contentFont, XBrushes.Black, new XRect(40, textBelowQrY + 40, page.Width - 80, page.Height - textBelowQrY - 100), XStringFormats.TopLeft);

                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return stream.ToArray();
                }
            }
        }



        /*
        public byte[] GeneratePdf(string title, string content, byte[] qrCodeImage)
        {
            using (var document = new PdfDocument())
            {
                var page = document.AddPage();
                var graphics = XGraphics.FromPdfPage(page);
                var font = new XFont("Verdana", 20, XFontStyle.Bold); // Utilisation de Bold pour le titre
                var contentFont = new XFont("Verdana", 12, XFontStyle.Regular); // Utilisation de Regular pour le contenu

                using (var qrImage = XImage.FromStream(() => new MemoryStream(qrCodeImage)))
                {
                    graphics.DrawImage(qrImage, 40, 40, 100, 100);
                }

                graphics.DrawString(title, font, XBrushes.Black, new XRect(0, 150, page.Width, 50), XStringFormats.TopCenter);
                graphics.DrawString(content, contentFont, XBrushes.Black, new XRect(40, 200, page.Width - 80, page.Height - 240), XStringFormats.TopLeft);

                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    return stream.ToArray();
                }
            }
        }*/
    }
}
