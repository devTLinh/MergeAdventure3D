using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BootstrapLoader : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return SceneManager .LoadSceneAsync("CoreGame",LoadSceneMode.Additive);
        Scene scene = SceneManager.GetSceneByName("CoreGame");

        SceneManager.SetActiveScene(scene);
    }
}