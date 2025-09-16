using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomMenuManager : MonoBehaviour
{
    public void OnBackClicked()
    {
        SceneManager.LoadScene("Samplescene"); // back to main menu
    }
}