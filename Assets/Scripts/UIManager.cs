using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text greenIndexText;

    // Allow multiple choice panels
    public List<GameObject> choicePanels = new List<GameObject>();

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
}
