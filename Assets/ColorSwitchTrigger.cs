
using UnityEngine;

public class ColorSwitchTrigger : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;

    [SerializeField] private float followSpeed = 2f;
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
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, player.transform.position.z), followSpeed * Time.deltaTime);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
}
