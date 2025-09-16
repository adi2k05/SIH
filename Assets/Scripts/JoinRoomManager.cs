using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class JoinRoomManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField joinCodeInput;
    public TMP_Text feedbackText;

    public void OnJoinClicked()
    {
        string code = joinCodeInput.text;

        if (string.IsNullOrEmpty(code))
        {
            ShowMessage("⚠ Please enter a code!");
            return;
        }

        // Later: Connect to Photon/Firebase here
        ShowMessage("✅ Joined Room with code: " + code);
    }

    public void OnBackClicked()
    {
        SceneManager.LoadScene("MainMenu"); // back to main menu
    }

    private void ShowMessage(string msg)
    {
        if (feedbackText != null)
            feedbackText.text = msg;
    }
}