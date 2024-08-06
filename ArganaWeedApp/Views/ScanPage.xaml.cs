using Microsoft.Maui.Controls;
using System;
using System.IO;
using ZXing;
using ZXing.Net.Maui;
using QRCoder;
using ZXing.QrCode.Internal;

namespace ArganaWeedApp.Views;

public partial class ScanPage : ContentPage
{
    private readonly QrTest _qrTest;
    public ScanPage(QrTest qrTest)
	{
		InitializeComponent();
        _qrTest = qrTest;
    }
    private async void OnCapturePhotoClicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.CapturePhotoAsync();
        if (photo != null)
        {
            var stream = await photo.OpenReadAsync();
            capturedImage.Source = ImageSource.FromStream(() => stream);
            var result = DecodeQRFromStream(stream);
            if (!string.IsNullOrEmpty(result))
            {
                resultLabel.Text = result;
                _qrTest.SetInputText(result); // Utilisez la méthode publique
                await Navigation.PopAsync();
            }
            else
            {
                resultLabel.Text = "Aucun QR code détecté.";
            }
        }
    }

    private async void OnPickPhotoClicked(object sender, EventArgs e)
    {
        var photo = await MediaPicker.PickPhotoAsync();
        if (photo != null)
        {
            var stream = await photo.OpenReadAsync();
            capturedImage.Source = ImageSource.FromStream(() => stream);
            var result = DecodeQRFromStream(stream);
            if (!string.IsNullOrEmpty(result))
            {
                resultLabel.Text = result;
                _qrTest.SetInputText(result); // Utilisez la méthode publique
                await Navigation.PopAsync();
            }
            else
            {
                resultLabel.Text = "Aucun QR code détecté.";
            }
        }
    }

    private string DecodeQRFromStream(Stream stream)
    {
        /*
        var reader = new BarcodeReaderGeneric();
        var bitmap = new System.Drawing.Bitmap(stream);
        var luminanceSource = new ZXing.BitmapLuminanceSource(bitmap);
        var binarizer = new ZXing.Common.HybridBinarizer(luminanceSource);
        var binaryBitmap = new ZXing.BinaryBitmap(binarizer);
        var result = reader.Decode(binaryBitmap);
        return result?.Text;*/
        return "Fake QR";
       

    }
}