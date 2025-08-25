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
        SetPlayerStartCoordinates();
    }

    private void SetPlayerStartCoordinates()
    {
        // Set the player's starting coordinates based on the hexTileInfoList
        if (hexTileInfoList.Count > 0)
        {
            HexTileInfo startingTile = GetHexTileFromHexCoords(2, 0);
            if (startingTile != null)
            {
                // Use the hex tile
                Debug.Log($"Found hex at (2,0): {startingTile.name}");
                //TODO: Set player start position
            }
            else
            {
                Debug.Log("No hex found at coordinates (2,0)");
            }
        }
    }

    public HexTileInfo GetHexTileFromHexCoords(int q, int r)
    {
        return hexTileInfoList.FirstOrDefault(tile => tile.hexCoordinates.x == q && tile.hexCoordinates.y == r);
    }
}