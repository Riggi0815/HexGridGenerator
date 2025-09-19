using UnityEngine;

public class HexTile : MonoBehaviour
{

    public Vector3Int hexCoordinates;
    public Vector3Int HexCoordinates => hexCoordinates;
    [SerializeField]bool isSafe = false;
    public bool IsSafe {
        get { return isSafe; }
        set { isSafe = value; }
    }

    private bool isWhite = false;
    public bool IsWhite
    {
        get { return isWhite; }
        set { isWhite = value; }
    }

    public void SetHexCoordinates(int x, int y, int z)
    {
        hexCoordinates = new Vector3Int(x, y, z);
    }
}
