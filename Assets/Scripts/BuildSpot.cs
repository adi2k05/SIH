using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public GameObject ecoPrefab;
    public GameObject nonEcoPrefab;
    public int ecoCost = 20;
    public int nonEcoCost = 10;

    public GameObject choicePanel; // assign per spot in Inspector

    private bool built = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !built)
        {
            UIManager.Instance.ShowChoices(this, choicePanel);
        }
    }

    public void BuildEco()
    {
        if (built) return;

        if (GameManager.Instance.CanAfford(ecoCost))
        {
            GameManager.Instance.Spend(ecoCost);
            GameObject newBuilding = Instantiate(
                ecoPrefab,
                transform.position + Vector3.up * 0.5f,
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
            GameObject newBuilding = Instantiate(
                nonEcoPrefab,
                transform.position + Vector3.up * 0.5f,
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
