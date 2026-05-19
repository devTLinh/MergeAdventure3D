using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField]
    Transform player;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void LoadSceneByName(
        string sceneName)
    {
        StartCoroutine(
            LoadRoutine(sceneName));
    }

    IEnumerator LoadRoutine(
        string sceneName)
    {
        yield return SceneManager
            .LoadSceneAsync(sceneName);

        yield return null;

        MapSpawn spawn =
            FindObjectOfType<MapSpawn>();

        if (spawn != null)
        {
            player.position =
                spawn.transform.position;
        }
    }
    public void ReturnBoard() {
        StartCoroutine(ReturnBoardRoutine());
    }

    IEnumerator ReturnBoardRoutine() {
        yield return SceneManager.LoadSceneAsync("CoreGame");
        yield return null;

        MapSpawn spawn = FindObjectOfType<MapSpawn>();
        if (spawn != null) {
            player.position = spawn.transform.position;
        }
    }
}