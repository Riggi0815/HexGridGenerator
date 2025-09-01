using UnityEngine;

public class HexGenerator : MonoBehaviour
{

    public GameObject objectToSpawn; // The prefab you want to spawn
    public Transform spawnPosition; // Optional: where to spawn the object

    [Header("Hex Grid Settings")]
    public int gridWidth = 5;
    public int gridHeight = 5;

    // This method will be called by the editor button
    public void GenerateHexGrid()
    {
        if (objectToSpawn == null)
        {
            Debug.LogError("No prefab assigned to spawn!");
            return;
        }

        ClearAllChildren();
        GenerateRectangularGrid();
    }

    public void ClearAllChildren()
    {
        foreach (Transform child in spawnPosition)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    private void GenerateRectangularGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = CalculateHexPosition(x, y);
                var hexTile = Instantiate(objectToSpawn, position, Quaternion.identity, spawnPosition);
                hexTile.GetComponent<HexTile>().SetHexCoordinates(0, x, y);
                hexTile.name = $"HexTile_{x}_{y}"; // Optional: name the tile for easier identification
            }
        }
    }

    private Vector3 CalculateHexPosition(int x, int y)
    {
        if (x % 2 == 1)
        {
            float xOffset = (x * objectToSpawn.GetComponent<Renderer>().bounds.size.x) - (x * objectToSpawn.GetComponent<Renderer>().bounds.size.x / 4);
            float yOffset = (y * objectToSpawn.GetComponent<Renderer>().bounds.size.z) + (objectToSpawn.GetComponent<Renderer>().bounds.size.z / 2);
            return new Vector3(xOffset, 0, yOffset);
        }
        else
        {
            float xOffset = (x * objectToSpawn.GetComponent<Renderer>().bounds.size.x) - (x * objectToSpawn.GetComponent<Renderer>().bounds.size.x / 4);
            float yOffset = y * objectToSpawn.GetComponent<Renderer>().bounds.size.z;
            return new Vector3(xOffset, 0, yOffset);
        }
        
        
    }
}