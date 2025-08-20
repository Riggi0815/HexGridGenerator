using UnityEngine;

public class HexGridWorldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject hexGridPrefab;
    private int gridNumber = 1;
    private Transform nextGridPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GenerateStartGrid()
    {
        for (int i = 0; i < 5; i++)
        {
            GenerateNewHexGrid();
            gridNumber++;
        }
    }

    private void GenerateNewHexGrid()
    {
        // Instantiate the hex grid prefab
        GameObject hexGrid = Instantiate(hexGridPrefab, gameObject.transform);
        if (gridNumber == 1)
        {
            hexGrid.transform.position = Vector3.zero;
        }
        else
        {
            hexGrid.transform.position = nextGridPosition.position;
        }
        hexGrid.name = "HexGrid" + gridNumber;
        nextGridPosition = hexGrid.transform.GetChild(0); // Assuming the first child is the spawn position
        for (int i = 1; i < hexGrid.transform.childCount; i++)
        {
            Transform child = hexGrid.transform.GetChild(i);
            child.name = i +"_" + child.name; // Prefixing child names with "1_"
        }
    }
}


