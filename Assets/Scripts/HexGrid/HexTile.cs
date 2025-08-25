using UnityEngine;

public class HexTile : MonoBehaviour
{

    public Vector2Int hexCoordinates;
    public Vector2Int HexCoordinates => hexCoordinates;

    public void SetCoordinates(int x, int y)
    {
        hexCoordinates = new Vector2Int(x, y);
    }
}
