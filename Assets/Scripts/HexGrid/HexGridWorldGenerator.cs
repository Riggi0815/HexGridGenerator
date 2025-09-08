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

    private List<List<HexTileInfo>> hexGridList = new List<List<HexTileInfo>>();
    public List<List<HexTileInfo>> HexGridList => hexGridList;

    List<HexTileInfo> hexTileInfoList = new List<HexTileInfo>();
    public List<HexTileInfo> HexTileInfoList => hexTileInfoList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GenerateStartGrid(HexColorManager hexColorManager)
    {
        
        //TODO: If needed Spawn the Hex Grids with a pool for better Performance
        for (int i = 0; i < 5; i++)
        {
            GenerateNewHexGrid(hexColorManager);
        }
    }

    private void GenerateNewHexGrid(HexColorManager hexColorManager)
    {
        hexGridTilesList.Clear();
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
        Debug.Log("Next Grid Position: " + nextGridPosition.name);
        for (int i = 1; i < hexGrid.transform.childCount; i++)
        {
            Transform child = hexGrid.transform.GetChild(i);
            child.name = gridNumber + "_" + child.name; // Prefixing child names with "1_"
            var newCoord = new Vector3Int(gridNumber, child.GetComponent<HexTile>().HexCoordinates.y, child.GetComponent<HexTile>().HexCoordinates.z);
            child.GetComponent<HexTile>().SetHexCoordinates(newCoord.x, newCoord.y, newCoord.z);
            HexTileInfo hexTileInfo = new HexTileInfo(child.name, child.GetComponent<HexTile>().HexCoordinates, child.position, child.GetComponent<Renderer>());
            hexGridTilesList.Add(hexTileInfo);
        }
        hexTileInfoList.AddRange(hexColorManager.SetInitialGridColors(hexGridTilesList, gridNumber));

        gridNumber++;
    }
}


