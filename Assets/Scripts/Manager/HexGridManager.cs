using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField] private HexGridWorldGenerator hexGridWorldGenerator;
    [SerializeField] private HexColorManager hexColorManager;
    [SerializeField] private List<HexTileInfo> hexTileInfoList = new List<HexTileInfo>();

    private Vector2Int currentHexTileCoords;
    public Vector2Int CurrentHexTileCoords
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
        hexGridWorldGenerator.GenerateStartGrid();
        hexTileInfoList = hexGridWorldGenerator.HexTileInfoList;
        hexColorManager.SetInitialColors(hexTileInfoList);
    }

    public HexTileInfo GetHexTileFromHexCoords(int q, int r)
    {
        HexTileInfo foundTile = hexTileInfoList.Find(tile => tile.hexCoordinates.x == q && tile.hexCoordinates.y == r);
        if (foundTile != null)
        {
            Debug.Log($"Hex found at coordinates ({q}, {r}): {foundTile.name}");
            currentHexTileCoords = new Vector2Int(q, r);
        }
        else
        {
            Debug.LogError($"No hex found at coordinates ({q}, {r})");
        }
        return foundTile;
    }
}