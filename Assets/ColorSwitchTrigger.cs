
using UnityEngine;

public class ColorSwitchTrigger : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    HexColorManager hexColorManager;

    [SerializeField] private float triggerDistance = 15f;

    public void InitializePlane(GameObject player, PlayerMovement playerMovement)
    {
        this.player = player;
        this.playerMovement = playerMovement;
        transform.position = player.transform.position - player.transform.forward * triggerDistance;
    }

    private void Update()
    {
        if (playerMovement.FirstMoveDone && player != null)
        {
            float currentDistance = Mathf.Abs(transform.position.z - player.transform.position.z);
            
            if (currentDistance > triggerDistance)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z - triggerDistance);
            }
            
        }

    }

    public void SetReferences(HexColorManager hexColorManager)
    {
        this.hexColorManager = hexColorManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        hexColorManager.ChangeColorPermanently(other.gameObject);
    }
}
