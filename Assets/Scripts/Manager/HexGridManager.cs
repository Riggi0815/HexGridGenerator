using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField] private HexGridWorldGenerator hexGridWorldGenerator;
    [SerializeField] private HexColorManager hexColorManager;
    [SerializeField] private List<HexTileInfo> hexTileInfoList = new List<HexTileInfo>();

    [SerializeField]private Vector3Int currentHexTileCoords;
    public Vector3Int CurrentHexTileCoords
    {
        get { return currentHexTileCoords; }
        set { currentHexTileCoords = value; }
    }

    public void Initialize()
    {
        Debug.Log("Create GridManager and HexColorManager");
        hexGridWorldGenerator = Instantiate(hexGridWorldGenerator, gameObject.transform);
        hexColorManager = Instantiate(hexColorManager, gameObject.transform);

    }

    public void SpawnHexGrid()
    {
        hexGridWorldGenerator.GenerateStartGrid(hexColorManager);
        hexTileInfoList = hexGridWorldGenerator.HexTileInfoList;
    }

    public HexTileInfo GetHexTileFromHexCoords(int gridNumber, int q, int r)
    {
        HexTileInfo foundTile = hexTileInfoList.Find(tile => tile.hexCoordinates.x == gridNumber && tile.hexCoordinates.y == q && tile.hexCoordinates.z == r);
        if (foundTile != null)
        {
            Debug.Log($"Hex found at coordinates ({gridNumber}, {q}, {r}): {foundTile.name}");
            currentHexTileCoords = new Vector3Int(foundTile.hexCoordinates.x, foundTile.hexCoordinates.y, foundTile.hexCoordinates.z);
        }
        else
        {
            Debug.LogError($"No hex found at coordinates ({gridNumber}, {q}, {r})");
            foundTile = null;
        }
        return foundTile;
    }
}