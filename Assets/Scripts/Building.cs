// Building.cs
using UnityEngine;

public class Building : MonoBehaviour
{
    public int ecoGain = 2; // how much GI this building gives per second
}

// 🆕 Special lightweight building (not a MonoBehaviour)
[System.Serializable]
public class VirtualBuilding
{
    public int ecoGain;

    public VirtualBuilding(int gain)
    {
        ecoGain = gain;
    }
}
