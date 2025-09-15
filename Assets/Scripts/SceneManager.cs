using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneManager : MonoBehaviour
{
    public GameObject LanguagePanel;
    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void play()
    {
        Load("SampleScene");
    }

    public void quit()
    {
        Debug.Log("Exited game");
        Application.Quit();
    }

    public void langactivate()
    {
        if (LanguagePanel != null)
        {
            LanguagePanel.SetActive(true);
        }
    }

    public void langdeactive()
    {
        if (LanguagePanel != null)
        {
            LanguagePanel.SetActive(false);
        }
    }
}
