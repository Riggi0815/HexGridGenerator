using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;

    private HexGridManager hexGridManager;
    private HexColorManager hexColorManager;


    public void SetReferences(HexGridManager hexGridManager , HexColorManager hexColorManager)
    {
        this.hexGridManager = hexGridManager;
        this.hexColorManager = hexColorManager;
    }

    public GameObject SpawnPlayer()
    {
        int hexStartCoordinateQ = 2;
        int hexStartCoordinateR = 0;

        HexTileInfo startingHex = hexGridManager.GetHexTileFromHexCoords(1, hexStartCoordinateQ, hexStartCoordinateR);
        if (startingHex != null)
        {
            GameObject player = Instantiate(playerPrefab, startingHex.worldPosition + new Vector3(0, 1.59f, 0), Quaternion.identity);
            Debug.Log("Active? " + player.activeInHierarchy);
            Debug.Log($"Player spawned at hex coordinates ({hexStartCoordinateQ}, {hexStartCoordinateR})");
            player.GetComponent<PlayerMovement>().InitialSetup(hexGridManager, hexColorManager);
            return player;
        }
        else
        {
            Debug.LogError($"No hex found at coordinates ({hexStartCoordinateQ}, {hexStartCoordinateR}). Player not spawned.");
            return null;
        }
        
    }

}
