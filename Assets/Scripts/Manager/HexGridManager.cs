using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField] private HexGridWorldGenerator hexGridWorldGenerator;
    [SerializeField] private HexColorManager hexColorManager;
    [SerializeField] private List<HexTileInfo> hexTileInfoList = new List<HexTileInfo>();

    public void Initialize()
    {
        hexGridWorldGenerator = Instantiate(hexGridWorldGenerator, gameObject.transform);
        hexColorManager = Instantiate(hexColorManager, gameObject.transform);
    }

    public void SpawnHexGrid()
    {
        hexTileInfoList = hexGridWorldGenerator.GenerateStartGrid();
        hexColorManager.SetInitialColors(hexTileInfoList);
    }

    public HexTileInfo GetHexTileFromHexCoords(int q, int r)
    {
        HexTileInfo foundTile = hexTileInfoList.Find(tile => tile.hexCoordinates.x == q && tile.hexCoordinates.y == r);
        if (foundTile != null)
        {
            Debug.Log($"Hex found at coordinates ({q}, {r}): {foundTile.name}");
        }
        else
        {
            Debug.LogError($"No hex found at coordinates ({q}, {r})");
        }
        return foundTile;
    }
}