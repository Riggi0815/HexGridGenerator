using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveTime = 0.3f;
    [SerializeField] private HexTileInfo currentHexTile;
    private bool canMove = true;

    private PlayerControls playerControls;
    private HexGridManager hexGridManager;

    public void InitialSetup(HexGridManager hexGridManager)
    {
        this.hexGridManager = hexGridManager;
        currentHexTile = hexGridManager.CurrentHexTile;
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Up.performed += ctx => Move(new Vector2Int(0, 1));
        playerControls.Gameplay.UpperRight.performed += ctx => Move(new Vector2Int(1, 0));
        playerControls.Gameplay.LowerRight.performed += ctx => Move(new Vector2Int(1, -1));
        playerControls.Gameplay.Down.performed += ctx => Move(new Vector2Int(0, -1));
        playerControls.Gameplay.UpperLeft.performed += ctx => Move(new Vector2Int(-1, 0));
        playerControls.Gameplay.LowerLeft.performed += ctx => Move(new Vector2Int(-1, -1));
    }

    private void Move(Vector2Int direction)
    {
        if (!canMove) return;

        canMove = false;
        //even to odd Row Move
        if (currentHexTile.hexCoordinates.y % 2 != 0 && direction.x != 0)
        {
            direction.y += 1;
        }

        //Move Player
        Vector3Int targetHexTile = currentHexTile.hexCoordinates + new Vector3Int(0, direction.x, direction.y);
        if (targetHexTile.z > 9)
        {
            targetHexTile.z = 0;
            targetHexTile.x += 1;
        }
        else if (targetHexTile.z < 0)
        {
            targetHexTile.z = 9;
            targetHexTile.x -= 1;
        }
        HexTileInfo targetHex = hexGridManager.GetHexTileFromHexCoords(targetHexTile.x, targetHexTile.y, targetHexTile.z);
        if (targetHex == null)
        {
            canMove = true;
            return;
        }
        currentHexTile = targetHex;
        transform.LookAt(new Vector3(targetHex.worldPosition.x, transform.position.y, targetHex.worldPosition.z));
        StartCoroutine(MoveToHex(new Vector3(targetHex.worldPosition.x, transform.position.y, targetHex.worldPosition.z), moveTime));
        hexGridManager.CheckIfNewGridNeeded(targetHex); 
    }

    private IEnumerator MoveToHex(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        canMove = true;
    }

    void OnDisable()
    {
        playerControls.Gameplay.Disable();

        playerControls.Gameplay.Up.performed -= ctx => Move(new Vector2Int(0, 1));
        playerControls.Gameplay.UpperRight.performed -= ctx => Move(new Vector2Int(1, 0));
        playerControls.Gameplay.LowerRight.performed -= ctx => Move(new Vector2Int(1, -1));
        playerControls.Gameplay.Down.performed -= ctx => Move(new Vector2Int(0, -1));
        playerControls.Gameplay.UpperLeft.performed -= ctx => Move(new Vector2Int(-1, 0));
        playerControls.Gameplay.LowerLeft.performed -= ctx => Move(new Vector2Int(-1, -1));
    }

    private void ColorCheck()
    {
        HexTileInfo targetHex = currentHexTile;
        if (targetHex != null)
        {
            if (targetHex.hexTileRenderer.sharedMaterial.color.grayscale > 0.6f && !targetHex.hexTileGameObject.GetComponent<HexTile>().IsSafe)
            {
                Debug.Log("Player on White Hex - Game Over" + targetHex.hexTileRenderer.sharedMaterial.color.grayscale + " IsSafe: " + targetHex.hexTileGameObject.GetComponent<HexTile>().IsSafe);
                //Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            ColorCheck();
        }
    }
}
