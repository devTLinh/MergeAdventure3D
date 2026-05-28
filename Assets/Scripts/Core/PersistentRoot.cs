using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    public static PersistentRoot Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    public void Shutdown()
    {
        Instance = null;

        Destroy(gameObject);
    }
    void OnApplicationQuit()
    {
        SaveManager.Instance.SaveGame();
    }

    void OnApplicationPause( bool pause)
    {
        if (pause)
        {
            SaveManager.Instance.SaveGame();
        }
    }
}