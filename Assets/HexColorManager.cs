using System.Collections.Generic;
using UnityEngine;

public class HexColorManager : MonoBehaviour
{
    //HexGridColoring
    [Header("Hex Grid Coloring")]
    [Header("Materials that will be changed")]
    [SerializeField] private Material blackMaterial;
    [SerializeField] private Material whiteMaterial;
    [Header("Transition Materials")]
    [SerializeField] private Material blackToWhiteMaterial;
    [SerializeField] private Material whiteToBlackMaterial;
    [Header("Permanent Materials for Safe Spots or Hexes that are not to be changed")]
    [SerializeField] private Material permaWhiteMaterial;
    [SerializeField] private Material permaBlackMaterial;

    private int gridNumber;
    private int safeHexCount = 3; // Number of safe hexes per grid
    List<HexTileInfo> safeHexes = new List<HexTileInfo>();

    public List<HexTileInfo> SetInitialGridColors(List<HexTileInfo> hexTileGridInfoList, int gridNumber)
    {
        List<HexTileInfo> hexListCopy = new List<HexTileInfo>(hexTileGridInfoList);

        this.gridNumber = gridNumber;
        if (gridNumber == 1)
        {
            int[] array = new int[] { 1, 5, 10, 15, 20 };
            for (int i = 0; i < array.Length; i++)
            {
                HexTileInfo safeHex = hexListCopy[array[i]];
                safeHex.hexTileRenderer.material = permaBlackMaterial;
                safeHexes.Add(safeHex);
            }
            // Remove the elements at the specified indices (from highest to lowest to avoid shifting)
            for (int i = array.Length - 1; i >= 0; i--)
            {
                hexListCopy.RemoveAt(array[i]);
            }
        }

        // Randomly select safe hexes
        for (int i = 0; i < safeHexCount; i++)
        {
            int randomIndex = Random.Range(0, hexListCopy.Count);
            HexTileInfo safeHex = hexListCopy[randomIndex];
            safeHex.hexTileRenderer.material = permaBlackMaterial;
            safeHexes.Add(safeHex);
            hexListCopy.RemoveAt(randomIndex); // Remove to avoid selecting the same hex again
        }

        List<HexTileInfo> hexesLeft = hexListCopy;
        int halfHexCount = hexesLeft.Count / 2;
        for (int i = 0; i < halfHexCount; i++)
        {
            int randomIndex = Random.Range(0, hexesLeft.Count);
            HexTileInfo blackHex = hexesLeft[randomIndex];
            blackHex.hexTileRenderer.material = blackMaterial;
            hexesLeft.RemoveAt(randomIndex);
        }

        for (int i = 0; i < hexesLeft.Count; i++)
        {
            HexTileInfo whiteHex = hexesLeft[i];
            whiteHex.hexTileRenderer.material = whiteMaterial;
        }

        return hexTileGridInfoList;
    }
}
