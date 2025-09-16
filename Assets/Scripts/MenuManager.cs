using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void OnCreateRoomClicked()
    {
        SceneManager.LoadScene("CreateRoomScene");
    }

    public void OnJoinRoomClicked()
    {
        SceneManager.LoadScene("JoinRoomScene");
    }
}