using UnityEngine;

public class HexTileInfo
{
    public string name;
    public Vector3Int hexCoordinates;
    public Vector3 worldPosition;
    public Renderer hexTileRenderer;
    public GameObject hexTileGameObject => hexTileRenderer.gameObject;

    public HexTileInfo(string name, Vector3Int hexCoords, Vector3 position, Renderer renderer)
    {
        this.name = name;
        hexCoordinates = hexCoords;
        worldPosition = position;
        hexTileRenderer = renderer;
    }
}