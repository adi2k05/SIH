using UnityEngine;
using UnityEngine.UI;
using ZXing;   // ✅ QR decoding
using ZXing.Common;
using System.Collections;
using TMPro;
public class QRScanner : MonoBehaviour
{
    public RawImage cameraView;     // UI element to preview camera
    public TMP_Text resultText;         // UI element to show result
    private WebCamTexture camTexture;
    private bool isScanning = false;

    public void StartQRScan()
    {
        if (isScanning) return;
        StartCoroutine(StartCamera());
    }

    private IEnumerator StartCamera()
    {
        // Open device camera
        camTexture = new WebCamTexture();
        cameraView.texture = camTexture;
        camTexture.Play();

        isScanning = true;

        while (isScanning)
        {
            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(camTexture.GetPixels32(),
                                                  camTexture.width,
                                                  camTexture.height);

                if (result != null)
                {
                    resultText.text = "QR: " + result.Text;
                    StopQRScan();
                }
            }
            catch { }
            yield return null;
        }
    }

    public void StopQRScan()
    {
        isScanning = false;
        if (camTexture != null && camTexture.isPlaying)
        {
            camTexture.Stop();
        }
    }
}