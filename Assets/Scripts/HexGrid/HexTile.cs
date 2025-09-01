using UnityEngine;

public class HexTile : MonoBehaviour
{

    public Vector3Int hexCoordinates;
    public Vector3Int HexCoordinates => hexCoordinates;

    public void SetHexCoordinates(int x, int y, int z)
    {
        hexCoordinates = new Vector3Int(x, y, z);
    }
}
