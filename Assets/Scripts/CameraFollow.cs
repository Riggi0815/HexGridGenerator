using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    private Transform player;

    public void Initialize()
    {
        Debug.Log("Camera Follow Initialized");
    }

    public void SetReferences(GameObject player)
    {
        this.player = player.transform;
    }

    // Update is called once per frame
    void Update () {
        if (player!= null)
        {
            transform.position = player.transform.position + new Vector3(0, 7.65f, -12.8f);
        }
        
    }
}
