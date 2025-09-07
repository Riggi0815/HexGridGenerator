using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Transform player;

    // Update is called once per frame
    void Update () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        transform.position = player.transform.position + new Vector3(0, 7.65f, -12.8f);
    }
}
