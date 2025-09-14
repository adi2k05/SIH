using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text greenIndexText;
    public TMP_Text messageText; // assign in Inspector

    private BuildSpot currentSpot;
    private GameObject activePanel;


    private void Awake()
    {
        Instance = this;
    }

    public void ShowChoices(BuildSpot spot, GameObject panel)
    {
        currentSpot = spot;
        activePanel = panel;
        panel.SetActive(true);
    }

    public void OnEcoClicked()
    {
        currentSpot.BuildEco();
        if (activePanel != null) activePanel.SetActive(false);
    }

    public void OnNonEcoClicked()
    {
        currentSpot.BuildNonEco();
        if (activePanel != null) activePanel.SetActive(false);
    }

    public void UpdateGreenIndexUI(int value)
    {
        greenIndexText.text = "Green Index: " + value;
    }

    // 🆕 Show a temporary message
    public void ShowMessage(string msg, float duration = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine(msg, duration));
    }

    private IEnumerator ShowMessageRoutine(string msg, float duration)
    {
        messageText.text = msg;
        yield return new WaitForSeconds(duration);
        messageText.text = "";
    }
    public void OnSituationEcoClicked()
    {
        GameManager.Instance.OnEcoSituationChosen();
    }
    
    public void OnSituationNonEcoClicked()
{
    GameManager.Instance.OnNonEcoSituationChosen();
}


}

