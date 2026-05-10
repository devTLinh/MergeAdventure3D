using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    string currentRegion;

    void Awake()
    {
        Instance = this;
    }

    public void LoadRegion(string sceneName)
    {
        StartCoroutine(
            LoadRoutine(sceneName));
    }

    IEnumerator LoadRoutine(string sceneName)
    {
        if (!string.IsNullOrEmpty(currentRegion))
        {
            yield return SceneManager
                .UnloadSceneAsync(
                    currentRegion);
        }

        yield return SceneManager
            .LoadSceneAsync(
                sceneName,
                LoadSceneMode.Additive);

        currentRegion = sceneName;
    }
}