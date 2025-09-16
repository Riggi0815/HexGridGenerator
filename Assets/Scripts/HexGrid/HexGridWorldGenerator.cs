using System;
using System.Collections.Generic;
using UnityEngine;

public class HexGridWorldGenerator : MonoBehaviour
{
    // Variables for Grid Generation
    [Header("Hex Grid Generation")]
    [SerializeField] private GameObject hexGridPrefab;
    private int gridNumber = 1;
    private Transform nextGridPosition;

    List<HexTileInfo> hexGridTilesList = new List<HexTileInfo>();

    public List<GameObject> hexGridObjectList = new List<GameObject>();
    private List<List<HexTileInfo>> hexGridList = new List<List<HexTileInfo>>();
    public List<List<HexTileInfo>> HexGridList => hexGridList;

    List<HexTileInfo> hexTileInfoList = new List<HexTileInfo>();
    public List<HexTileInfo> HexTileInfoList => hexTileInfoList;

    private HexColorManager hexColorManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GenerateStartGrid(HexColorManager hexColorManager)
    {
        this.hexColorManager = hexColorManager;

        //TODO: If needed Spawn the Hex Grids with a pool for better Performance
        for (int i = 0; i < 5; i++)
        {
            GenerateNewHexGrid(this.hexColorManager);
        }
        Debug.Log("Hex Generation Complete");
    }

    private void GenerateNewHexGrid(HexColorManager hexColorManager)
    {
        hexGridTilesList.Clear();
        // Instantiate the hex grid prefab
        GameObject hexGrid = Instantiate(hexGridPrefab, gameObject.transform);
        hexGridObjectList.Add(hexGrid);
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
        Debug.Log("Next Grid Position: " + nextGridPosition.name);
        for (int i = 1; i < hexGrid.transform.childCount; i++)
        {
            Transform child = hexGrid.transform.GetChild(i);
            child.name = gridNumber + "_" + child.name; // Prefixing child names with "1_"
            var newCoord = new Vector3Int(gridNumber, child.GetComponent<HexTile>().HexCoordinates.y, child.GetComponent<HexTile>().HexCoordinates.z);
            child.GetComponent<HexTile>().SetHexCoordinates(newCoord.x, newCoord.y, newCoord.z);
            HexTileInfo hexTileInfo = new HexTileInfo(child.name, child.GetComponent<HexTile>().HexCoordinates, child.position, child.GetComponent<Renderer>());
            hexGridTilesList.Add(hexTileInfo);
            Debug.Log("Hex Tile Added: " + child.name + " with coordinates " + newCoord);
        }
        hexTileInfoList.AddRange(hexColorManager.SetInitialGridColors(hexGridTilesList, gridNumber));
        hexGridList.Add(hexGridTilesList);

        gridNumber++;
    }

    public void SpawnAndDeleteGrid()
    {
        Debug.Log(hexColorManager.IsTransitioning);
        if (!hexColorManager.IsTransitioning)
        {
            Destroy(hexGridObjectList[0]);
            hexGridObjectList.RemoveAt(0);


            Debug.Log(hexGridList.Count);
            hexGridList.RemoveAt(0);

            hexColorManager.RemoveFirstGridFromList();

            Debug.Log(hexGridList.Count);
            hexTileInfoList.RemoveAll(tile => tile.hexCoordinates.x == gridNumber - 4);
            GenerateNewHexGrid(hexColorManager);
        }
        
    }
}


