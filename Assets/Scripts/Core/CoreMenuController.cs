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

        // Always save local
        SaveManager
            .Instance
            .SaveGame();

        bool success =
            false;

        // Account mode → upload cloud
        if (GameMode.UseCloud
            && CloudSaveManager.Instance != null)
        {
            bool done =
                false;

            CloudSaveManager
            .Instance
            .UploadSave(
            result =>
            {
                success =
                    result;

                done =
                    true;
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

        // Guest
        if (GameMode.IsGuest)
        {
            GameLaunchData
                .HasCloudSave =
                false;
        }
        // Account
        else
        {
            GameLaunchData
                .HasCloudSave =
                success;
        }

        Debug.Log(
            "Returning To Menu | Guest="
            + GameLaunchData.IsGuest
            + " | Cloud="
            + GameLaunchData.HasCloudSave);

        // Destroy gameplay persistent
        if (PersistentRoot.Instance != null)
        {
            PersistentRoot
                .Instance
                .Shutdown();
        }

        SceneManager
            .LoadScene(
                "Main");
    }
}