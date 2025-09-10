using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int greenIndex = 0;
    public List<Building> buildings = new List<Building>();

    private float timer = 0f;

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
            foreach (var b in buildings)
            {
                greenIndex += b.ecoGain;
            }
            UIManager.Instance.UpdateGreenIndexUI(greenIndex);
        }
    }

    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    public bool CanAfford(int cost)
    {
        return greenIndex >= cost;
    }

    public void Spend(int cost)
    {
        greenIndex -= cost;
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);
    }
}
