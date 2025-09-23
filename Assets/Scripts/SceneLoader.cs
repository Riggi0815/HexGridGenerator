using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject PlayerObject => playerObject;
    
    private static SceneLoader instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
