using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Unlockable
{
    public int threshold;              // GI needed
    public GameObject panel;           // Panel to show
    [HideInInspector] public bool triggered = false; // To make sure it shows once
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int greenIndex = 0;

    // Allow both real Buildings and VirtualBuildings
    public List<int> ecoGains = new List<int>();

    private float timer = 0f;

    [Header("Unlockable Situations")]
    public List<Unlockable> unlockables; // Assign in Inspector

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
                greenIndex += gain;

            UIManager.Instance.UpdateGreenIndexUI(greenIndex);

            // 🔓 Check each unlockable
            foreach (var u in unlockables)
            {
                if (!u.triggered && greenIndex >= u.threshold)
                {
                    u.triggered = true;
                    UnlockChoice(u);
                }
            }

            if (greenIndex < 0)
                GameOver();
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

    private void UnlockChoice(Unlockable u)
    {
        if (u.panel != null)
            u.panel.SetActive(true);
        else
            Debug.LogWarning("Unlock panel not assigned for threshold " + u.threshold);
    }

    // Example situation choices
    public void OnEcoSituationChosen()
    {
        greenIndex -= 50;
        AddVirtualBuilding(2); // +1 GI/sec forever
        CloseAllPanels();
    }

    public void OnNonEcoSituationChosen()
    {
        AddVirtualBuilding(-1); // -1 GI/sec forever
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);
        CloseAllPanels();
    }

    private void CloseAllPanels()
    {
        foreach (var u in unlockables)
        {
            if (u.panel != null) u.panel.SetActive(false);
        }
    }

    public void OnEcoSituationChosenStatic()
    {
        greenIndex += 20;
        CloseAllPanels();
    }

    public void OnNonEcoSituationChosenStatic()
    {
        greenIndex -= 20;
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);
        CloseAllPanels();
    }

    public void GameOver()
    {
        Debug.Log("Game Over! Green Index dropped below 0.");
        // You could load a Game Over scene here
    }
}
