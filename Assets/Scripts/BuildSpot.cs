using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public enum BuildMode { ChoicePanel, AutoEco, AutoNonEco }
    [Header("Build Mode")]
    public BuildMode buildMode = BuildMode.ChoicePanel;

    [Header("Prefabs")]
    public GameObject ecoPrefab;
    public GameObject nonEcoPrefab;
    public int ecoCost = 20;
    public int nonEcoCost = 10;

    [Header("UI")]
    public GameObject choicePanel; // only used if mode = ChoicePanel

    [Header("Spawn Settings")]
    public Transform spawnPoint; // assign in Inspector (empty child as reference)

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

            // Spawn at this BuildSpot’s defined transform
            GameObject newBuilding = Instantiate(
                ecoPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
            newBuilding.transform.localScale = spawnPoint.localScale;

            built = true;

            Building building = newBuilding.GetComponent<Building>();
            if (building != null) GameManager.Instance.AddBuilding(building);

            Destroy(gameObject);
        }
        else
        {
            UIManager.Instance.ShowMessage("Not enough Green Index for Eco building!", Color.red, 2f);
        }
    }

    public void BuildNonEco()
    {
        if (built) return;

        if (GameManager.Instance.CanAfford(nonEcoCost))
        {
            GameManager.Instance.Spend(nonEcoCost);

            GameObject newBuilding = Instantiate(
                nonEcoPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );
            newBuilding.transform.localScale = spawnPoint.localScale;

            built = true;

            Building building = newBuilding.GetComponent<Building>();
            if (building != null) GameManager.Instance.AddBuilding(building);

            Destroy(gameObject);
        }
        else
        {
            UIManager.Instance.ShowMessage("Not enough Green Index for Non-Eco building!", Color.red, 2f);
        }
    }
}
