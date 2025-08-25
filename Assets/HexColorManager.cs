using System.Collections.Generic;
using UnityEngine;

public class HexColorManager : MonoBehaviour
{
    //HexGridColoring
    [Header("Hex Grid Coloring")]
    [SerializeField] private Material blackMaterial;

    public void SetInitialColors(List<HexTileInfo> hexTileInfoList)
    {
        foreach (HexTileInfo hexTileInfo in hexTileInfoList)
        {
            // Set the initial color for each hex tile
            hexTileInfo.hexTileRenderer.material = blackMaterial; // Default color
        }
    }
}
