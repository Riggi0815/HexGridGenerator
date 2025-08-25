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


    List<HexTileInfo> hexTileInfoList = new List<HexTileInfo>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public List<HexTileInfo> GenerateStartGrid()
    {
        for (int i = 0; i < 5; i++)
        {
            GenerateNewHexGrid();
            gridNumber++;
        }
        return hexTileInfoList;
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
            HexTileInfo hexTileInfo = new HexTileInfo(child.name, child.GetComponent<HexTile>().HexCoordinates, child.position, child.GetComponent<Renderer>());
            hexTileInfoList.Add(hexTileInfo);
            child.name = gridNumber + "_" + child.name; // Prefixing child names with "1_"
        }
    }
}


