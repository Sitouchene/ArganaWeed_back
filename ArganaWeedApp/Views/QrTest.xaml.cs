using ArganaWeedApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using QRCoder;
using System.IO;

namespace ArganaWeedApp.Views;


public partial class QrTest : ContentPage
{
	public QrTest()
	{
		InitializeComponent();
	}

    private void OnGenerateQRCodeClicked(object sender, EventArgs e)
    {
        var text = inputEntry.Text;
        if (!string.IsNullOrEmpty(text))
        {
            // Utilisation de QRCoder pour g�n�rer le QR Code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImageAsByteArr = qrCode.GetGraphic(20);

            // Convertir byte array en ImageSource
            qrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeImageAsByteArr));
        }
    }

    private async void OnScanQRCodeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ScanPage(this));
    }

    // M�thode publique pour d�finir le texte de l'entr�e
    public void SetInputText(string text)
    {
        inputEntry.Text = text;
    }
}