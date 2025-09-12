using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    [Header("Timing Settings")]
    [SerializeField] private float colorChangeCycleInterval; // Time between color changes
    [SerializeField] private bool isColorCycleActive = false; // Duration of color transition
    private Coroutine colorCycleCoroutine;
    List<Task> transitions = new List<Task>();

    private int gridNumber;
    private int safeHexCount = 3; // Number of safe hexes per grid
    List<HexTileInfo> safeHexes = new List<HexTileInfo>();

    public List<List<HexTileInfo>> hexGridList = new List<List<HexTileInfo>>();


    public List<HexTileInfo> SetInitialGridColors(List<HexTileInfo> hexTileGridInfoList, int gridNumber)
    {
        List<HexTileInfo> hexListCopy = new List<HexTileInfo>(hexTileGridInfoList);

        this.gridNumber = gridNumber;
        if (gridNumber == 1)
        {
            int[] array = new int[] { 0, 10, 20, 30, 40 };
            for (int i = 0; i < array.Length; i++)
            {
                HexTileInfo safeHex = hexListCopy[array[i]];
                safeHex.hexTileRenderer.material = permaBlackMaterial;
                safeHex.hexTileGameObject.GetComponent<HexTile>().IsSafe = true;
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
            safeHex.hexTileGameObject.GetComponent<HexTile>().IsSafe = true;
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

        for (int i = 0; i < hexTileGridInfoList.Count; i++)
        {
            Debug.Log(hexTileGridInfoList[i].name);
        }

        List<HexTileInfo> hexTileGridInfoListCopy = new List<HexTileInfo>(hexTileGridInfoList);
        hexGridList.Add(hexTileGridInfoListCopy);

        return hexTileGridInfoList;
    }

    public void RemoveFirstGridFromList()
    {
        if (hexGridList.Count > 0)
        {
            hexGridList.RemoveAt(0);
        }
    }

    public void StopColorChangeCycle()
    {
        if (colorCycleCoroutine != null)
        {
            StopCoroutine(colorCycleCoroutine);
            colorCycleCoroutine = null;
        }
    }

    public void StartColorChangeCycle()
    {
        if (colorCycleCoroutine != null)
        {
            StopCoroutine(colorCycleCoroutine);
        }

        for (int i = 0; i < hexGridList.Count; i++)
        {
            for (int j = 0; j < hexGridList[i].Count; j++)
            {
                Debug.Log($"Grid {i + 1} - Hex: {hexGridList[i][j].name}, Material: {hexGridList[i][j].hexTileRenderer.material.name}");
            }
        }
        isColorCycleActive = true;
        colorCycleCoroutine = StartCoroutine(ColorChangeCycleRoutine());
    }

    private IEnumerator ColorChangeCycleRoutine()
    {
        while (isColorCycleActive)
        {

            yield return new WaitForSeconds(colorChangeCycleInterval);

            List<Task> transitionTasks = new List<Task>();

            List<Renderer> hexesToChangeToBlack = new List<Renderer>();
            List<Renderer> hexesToChangeToWhite = new List<Renderer>();


            for (int i = 0; i < hexGridList.Count; i++)
            {
                int amountHexesToChange = hexGridList[i].Count / 2;
                List<HexTileInfo> hexesToChange = new List<HexTileInfo>();

                for (int j = 0; j < amountHexesToChange; j++)
                {
                    int randomIndex = Random.Range(0, hexGridList[i].Count);
                    HexTileInfo hexToChange = hexGridList[i][randomIndex];

                    if (safeHexes.Contains(hexToChange) || hexesToChange.Contains(hexToChange))
                    {
                        // Skip safe hexes
                        j--;
                        continue;
                    }
                    else
                    {
                        hexesToChange.Add(hexToChange);
                        Debug.Log(hexToChange.hexTileGameObject.name);
                        if (hexToChange.hexTileRenderer.material.color == blackMaterial.color)
                        {
                            hexesToChangeToWhite.Add(hexToChange.hexTileRenderer);
                        }
                        else if (hexToChange.hexTileRenderer.material.color == whiteMaterial.color)
                        {
                            hexesToChangeToBlack.Add(hexToChange.hexTileRenderer);
                        }
                    }
                }
            }

            // Starte die Übergänge
            Task blackToWhiteTask = TransitionColor(hexesToChangeToBlack, whiteMaterial, whiteToBlackMaterial, blackMaterial);
            Task whiteToBlackTask = TransitionColor(hexesToChangeToWhite, blackMaterial, blackToWhiteMaterial, whiteMaterial);

            // Warte bis beide Übergänge abgeschlossen sind
            yield return new WaitUntil(() => blackToWhiteTask.IsCompleted && whiteToBlackTask.IsCompleted);

            
        }
    }

    private async Task TransitionColor(List<Renderer> hexRenderers, Material startMaterial, Material transitionMaterial, Material targetMaterial)
    {
        Material sharedMaterial = transitionMaterial;
        // Change to transition material
        foreach (var hexRenderer in hexRenderers)
        {
            hexRenderer.sharedMaterial = sharedMaterial;
        }

        // Lerp the color over time
        float duration = 3.0f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            sharedMaterial.color = Color.Lerp(startMaterial.color, targetMaterial.color, t);
            elapsed += Time.deltaTime;
            await Task.Yield();
        }

        foreach (var hexRenderer in hexRenderers)
        {
            hexRenderer.material = targetMaterial;
            transitionMaterial.color = startMaterial.color; // Reset transition material color
        }

    
}
}


