using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using System.Collections;

public class QRScanner : MonoBehaviour
{
    [Header("UI References")]
    public RawImage cameraView;      // Preview of camera
    public Text resultText;          // Where scanned QR result will appear
    public GameObject qrScannerPanel; // Panel containing camera, scan & back buttons
    public GameObject joinRoomPanel;  // Original join room panel

    private WebCamTexture camTexture;
    private bool isScanning = false;

    // Call this when Scan QR button is pressed
    public void OpenScanner()
    {
        // Switch panels
        if (joinRoomPanel != null) joinRoomPanel.SetActive(false);
        if (qrScannerPanel != null) qrScannerPanel.SetActive(true);

        StartQRScan();
    }

    public void StartQRScan()
    {
        if (isScanning) return;
        StartCoroutine(StartCamera());
    }

    private IEnumerator StartCamera()
    {
        // Open device camera
        camTexture = new WebCamTexture();
        if (cameraView != null)
            cameraView.texture = camTexture;

        camTexture.Play();
        isScanning = true;

        while (isScanning)
        {
            if (camTexture.width < 100) // camera not ready yet
            {
                yield return null;
                continue;
            }

            try
            {
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);

                if (result != null)
                {
                    if (resultText != null)
                        resultText.text = "QR: " + result.Text;

                    StopQRScan();
                    yield break; // stop coroutine after scanning
                }
            }
            catch
            {
                // ignore errors
            }
            yield return null;
        }
    }

    // Call this when Back button in QRScanner panel is pressed
    public void CloseScanner()
    {
        StopQRScan();

        // Switch panels back
        if (qrScannerPanel != null) qrScannerPanel.SetActive(false);
        if (joinRoomPanel != null) joinRoomPanel.SetActive(true);
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