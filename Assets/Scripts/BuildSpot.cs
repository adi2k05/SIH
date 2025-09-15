using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public enum BuildMode { ChoicePanel, AutoEco, AutoNonEco }
    [Header("Build Mode")]
    public BuildMode buildMode = BuildMode.ChoicePanel;

    public GameObject ecoPrefab;
    public GameObject nonEcoPrefab;
    public int ecoCost = 20;
    public int nonEcoCost = 10;

    public GameObject choicePanel; // only used if mode = ChoicePanel

    private bool built = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !built)
        {
            switch (buildMode)
            {
                case BuildMode.ChoicePanel:
                    UIManager.Instance.ShowChoices(this, choicePanel);
                    break;

                case BuildMode.AutoEco:
                    BuildEco();
                    break;

                case BuildMode.AutoNonEco:
                    BuildNonEco();
                    break;
            }
        }
    }

    public void BuildEco()
{
    if (built) return;

    if (GameManager.Instance.CanAfford(ecoCost))
    {
        GameManager.Instance.Spend(ecoCost);

        // 👉 Spawn a little in front of the build spot
        Vector3 spawnPos = transform.position + transform.forward * 5f + Vector3.up * 0.5f;

        GameObject newBuilding = Instantiate(
            ecoPrefab,
            spawnPos,
            Quaternion.identity
        );

        built = true;

        Building building = newBuilding.GetComponent<Building>();
        if (building != null) GameManager.Instance.AddBuilding(building);

        Destroy(gameObject);
    }
    else
    {
        UIManager.Instance.ShowMessage("Not enough Green Index for Eco building!");
    }
}


public void BuildNonEco()
{
    if (built) return;

    if (GameManager.Instance.CanAfford(nonEcoCost))
    {
        GameManager.Instance.Spend(nonEcoCost);

        // Get player's forward position
        Vector3 spawnPos = transform.position + transform.forward * 5f + Vector3.up * 0.5f;

        GameObject newBuilding = Instantiate(
            nonEcoPrefab,
            spawnPos + Vector3.up * 0.5f,
            Quaternion.identity
        );
        built = true;

        Building building = newBuilding.GetComponent<Building>();
        if (building != null) GameManager.Instance.AddBuilding(building);

        Destroy(gameObject);
    }
    else
    {
        UIManager.Instance.ShowMessage("Not enough Green Index for Non-Eco building!");
    }
}

}
