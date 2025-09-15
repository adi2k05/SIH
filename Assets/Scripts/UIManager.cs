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

    // 🆕 Show a temporary message (with color support)
    public void ShowMessage(string msg, Color color, float duration = 2f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowMessageRoutine(msg, color, duration));
    }

    private IEnumerator ShowMessageRoutine(string msg, Color color, float duration)
    {
        messageText.text = msg;
        messageText.color = color;
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

    public void OnSituationEcoClickedStatic()
    {
        GameManager.Instance.OnEcoSituationChosenStatic();
        ShowMessage("+20 GI", Color.green, 2f); // ✅ green message
    }

    public void OnSituationNonEcoClickedStatic()
    {
        GameManager.Instance.OnNonEcoSituationChosenStatic();
        ShowMessage("-20 GI", Color.red, 2f); // ✅ red message
    }
}
