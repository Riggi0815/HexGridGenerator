using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexGridManager : MonoBehaviour
{
    [SerializeField] private HexGridWorldGenerator hexGridWorldGenerator;
    [SerializeField] private HexColorManager hexColorManager;
    [SerializeField] private List<HexTileInfo> hexTileInfoList;

    [SerializeField] private HexTileInfo currentHexTile;
    public HexTileInfo CurrentHexTile
    {
        get { return currentHexTile; }
        set { currentHexTile = value; }
    }

    public HexColorManager Initialize()
    {
        Debug.Log("Create GridManager and HexColorManager");
        hexGridWorldGenerator = Instantiate(hexGridWorldGenerator, gameObject.transform);
        hexColorManager = Instantiate(hexColorManager, gameObject.transform);

        return hexColorManager;
    }

    public void SpawnHexGrid()
    {
        hexGridWorldGenerator.GenerateStartGrid(hexColorManager);
        hexTileInfoList = hexGridWorldGenerator.HexTileInfoList;
        StartColorCycle();
    }

    public HexTileInfo GetHexTileFromHexCoords(int gridNumber, int q, int r)
    {
        HexTileInfo foundTile = hexTileInfoList.Find(tile => tile.hexCoordinates.x == gridNumber && tile.hexCoordinates.y == q && tile.hexCoordinates.z == r);
        if (foundTile != null)
        {
            //Debug.Log($"Hex found at coordinates ({gridNumber}, {q}, {r}): {foundTile.name}");
            currentHexTile = foundTile;
        }
        else
        {
            Debug.LogError($"No hex found at coordinates ({gridNumber}, {q}, {r})");
        }
        return foundTile;
    }

    public void CheckIfNewGridNeeded(HexTileInfo targetHex)
    {
        if (targetHex.hexCoordinates.x == hexTileInfoList.Last().hexCoordinates.x - 1)
        {
            hexGridWorldGenerator.SpawnAndDeleteGrid();
        }
    }
    
    private void StartColorCycle()
    {
        Debug.Log("Start Color Change Cycle");
        hexColorManager.StartColorChangeCycle();
    }
}