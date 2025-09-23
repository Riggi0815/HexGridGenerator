using UnityEngine;

public class PlayerManager : MonoBehaviour
{


    private HexGridManager hexGridManager;
    private HexColorManager hexColorManager;
    private SceneLoader sceneLoader;


    public void SetReferences(HexGridManager hexGridManager, HexColorManager hexColorManager, SceneLoader sceneLoader)
    {
        this.hexGridManager = hexGridManager;
        this.hexColorManager = hexColorManager;
        this.sceneLoader = sceneLoader;
    }

    public GameObject SpawnPlayer()
    {
        int hexStartCoordinateQ = 2;
        int hexStartCoordinateR = 0;

        HexTileInfo startingHex = hexGridManager.GetHexTileFromHexCoords(1, hexStartCoordinateQ, hexStartCoordinateR);
        if (startingHex != null)
        {
            GameObject player = Instantiate(sceneLoader.PlayerObject, startingHex.worldPosition + new Vector3(0, 1.59f, 0), Quaternion.identity);
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
