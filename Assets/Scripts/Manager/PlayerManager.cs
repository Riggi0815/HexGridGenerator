using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    
    private HexGridManager hexGridManager;

    public void SetReferences(HexGridManager hexGridManager) {
        this.hexGridManager = hexGridManager;
    }

    private void InitializePlayer()
    {
        // Initialize player-related settings or properties here
    }

    public void SpawnPlayer()
    {
        int hexStartCoordinateQ = 2;
        int hexStartCoordinateR = 0;

        HexTileInfo startingHex = hexGridManager.GetHexTileFromHexCoords(hexStartCoordinateQ, hexStartCoordinateR);
        if (startingHex != null)
        {
            Instantiate(playerPrefab, startingHex.worldPosition + new Vector3(0, 1.15f, 0), Quaternion.identity);
            Debug.Log($"Player spawned at hex coordinates ({hexStartCoordinateQ}, {hexStartCoordinateR})");
        }
        else
        {
            Debug.LogError($"No hex found at coordinates ({hexStartCoordinateQ}, {hexStartCoordinateR}). Player not spawned.");
        }
    }

}
