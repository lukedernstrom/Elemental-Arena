using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void OnEnable()
{
    Debug.Log("MusicController ENABLED in scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}

void OnDestroy()
{
    Debug.Log("MusicController DESTROYED in scene: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
}

}
