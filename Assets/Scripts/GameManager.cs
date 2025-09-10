using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int greenIndex = 50; // starting value

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);
    }

    public void UpdateGreenIndex(int valueChange)
    {
        greenIndex += valueChange;
        UIManager.Instance.UpdateGreenIndexUI(greenIndex);
    }
}
