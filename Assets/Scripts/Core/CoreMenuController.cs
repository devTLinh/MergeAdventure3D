using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CoreMenuController :
MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(
            KeyCode.P))
        {
            ReturnToMenu();
        }
    }

    public void ReturnToMenu()
    {
        StartCoroutine(
            ReturnRoutine());
    }

    IEnumerator ReturnRoutine()
    {
        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible =
            true;

        SaveManager
            .Instance
            .SaveGame();

        bool done =
            false;

        bool success =
            false;
        if (CloudSaveManager.Instance != null)
        {
            CloudSaveManager
            .Instance
            .UploadSave(
            result =>
            {
                success = result;
                done = true;
            });

            while (!done)
            {
                yield return null;
            }

            if (!success)
            {
                Debug.LogWarning(
                    "Cloud Upload Failed - Using Local Save");
            }
            else
            {
                Debug.Log(
                    "Cloud Upload Success");
            }
        }

        // Reset launch state
        GameLaunchData.StartNewGame =
            false;

        GameLaunchData.HasCloudSave =
            true;
        if (PersistentRoot.Instance != null)
        {
            PersistentRoot
                .Instance
                .Shutdown();
        }

        SceneManager.LoadScene(
            "Main");
    }
}