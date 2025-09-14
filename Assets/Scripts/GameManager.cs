using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int greenIndex = 0;

    // Allow both real Buildings and VirtualBuildings
    public List<int> ecoGains = new List<int>();

    private float timer = 0f;

    public GameObject unlockChoicePanel;
    public int unlockThreshold = 50;
    private bool unlockTriggered = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            timer = 0f;

            foreach (var gain in ecoGains)
            {
                greenIndex += gain;
            }

            UIManager.Instance.UpdateGreenIndexUI(greenIndex);

            if (!unlockTriggered && greenIndex >= unlockThreshold)
            {
                unlockTriggered = true;
                UnlockChoice();
            }
        }
    }

    public void AddBuilding(Building building)
    {
        ecoGains.Add(building.ecoGain);
    }

    public void AddVirtualBuilding(int ecoGain)
    {
        ecoGains.Add(ecoGain);
    }

    public bool CanAfford(int cost) => greenIndex >= cost;

    public void Spend(int cost)
    {
        greenIndex -= cost;
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);
    }

    private void UnlockChoice()
    {
        if (unlockChoicePanel != null)
            unlockChoicePanel.SetActive(true);
    }

    // 🆕 Called when player picks the eco situation
    public void OnEcoSituationChosen()
    {
        GameManager.Instance.greenIndex -= 100;
        AddVirtualBuilding(1); // +1 GI/sec forever
        if (unlockChoicePanel != null)
            unlockChoicePanel.SetActive(false);
    }

    public void OnNonEcoSituationChosen()
    {
        greenIndex -= 50; 
        AddVirtualBuilding(-1); 
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);

        if (unlockChoicePanel != null)
            unlockChoicePanel.SetActive(false);
    }
}
