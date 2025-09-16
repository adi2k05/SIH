using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateRoomManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField roomCodeInput;
    public TMP_Text generatedCodeText;
    public Button generateCodeButton;
    public Button startButton;
    public Button backButton;

    private string roomCode;

    void Start()
    {
        generateCodeButton.onClick.AddListener(GenerateRoomCode);
        startButton.onClick.AddListener(StartContest);
        backButton.onClick.AddListener(BackToMenu);

        generatedCodeText.text = "Code: ---";
    }

    void GenerateRoomCode()
    {
        // If input field is empty, generate random code
        if (string.IsNullOrEmpty(roomCodeInput.text))
        {
            roomCode = Random.Range(1000, 9999).ToString();
        }
        else
        {
            roomCode = roomCodeInput.text;
        }

        generatedCodeText.text = "Code: " + roomCode;
        Debug.Log("Room created with code: " + roomCode);
    }

    void StartContest()
    {
        // For now just load tycoon gameplay scene (SampleScene)
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Contest started in room: " + roomCode);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("EntryScene"); // main menu
    }
}