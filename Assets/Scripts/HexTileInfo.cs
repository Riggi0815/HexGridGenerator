using UnityEngine;

public class HexTileInfo
{
    public string name;
    public Vector2Int hexCoordinates;
    public Vector3 worldPosition;
    public Renderer hexTileRenderer;
    public GameObject hexTileGameObject => hexTileRenderer.gameObject;

    public HexTileInfo(string name, Vector2Int hexCoords, Vector3 position, Renderer renderer)
    {
        this.name = name;
        hexCoordinates = hexCoords;
        worldPosition = position;
        hexTileRenderer = renderer;
    }
}