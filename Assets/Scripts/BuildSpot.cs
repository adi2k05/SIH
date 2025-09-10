using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public GameObject ecoPrefab;       // e.g., Solar Panel
    public GameObject nonEcoPrefab;    // e.g., Coal Plant
    public int ecoGreenPoints = 10;
    public int nonEcoGreenPoints = -10;

    private bool built = false;
    public GameObject choicePanel;

    public Vector3 buildOffset = new Vector3(0, 0.5f, 2f); // tweak these values in Inspector

    public void BuildEco(){
        if (built) return;
    Instantiate(ecoPrefab, transform.position + buildOffset, Quaternion.identity);
    GameManager.Instance.UpdateGreenIndex(ecoGreenPoints);
    built = true;
    Destroy(gameObject);
    }

    public void BuildNonEco()
    {
        if (built) return;
        Instantiate(nonEcoPrefab, transform.position + buildOffset, Quaternion.identity);
        GameManager.Instance.UpdateGreenIndex(nonEcoGreenPoints);
        built = true;
        Destroy(gameObject);
        }


    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        UIManager.Instance.ShowChoices(this, choicePanel);
    }
}
}
