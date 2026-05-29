using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private void Start()
    {
        continueButton.SetActive(
        HasLocalSave());
    }
    void OnEnable()
    {
        continueButton.SetActive(
        HasLocalSave());
    }
    bool HasLocalSave()
    {
        string path =
            Application
            .persistentDataPath
            + "/save.json";

        return File.Exists(
            path);
    }
    public void Play()
    {
        GameLaunchData.StartNewGame = true;
        StartGame();

    }

    public void Continue()
    {
        GameLaunchData.StartNewGame = false;
        StartGame();
    }
    void StartGame()
    {
        MainUIManager.Instance.ShowLoading();
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        yield return null;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("CoreGame");

        while (!asyncLoad.isDone)
        {
            // asyncLoad.progress sẽ chạy từ 0 đến 0.9 (khi tải xong)
            // Nếu có thanh loading bar, bạn có thể cập nhật ở đây:
            // LoadingSlider.value = asyncLoad.progress;

            yield return null;
        }
    }
    public void Settings()
    {
        MainUIManager.Instance.ShowSettings();
    }
    public void Profile()
    {
        MainUIManager.Instance.ShowProfile();
    }
    public void Logout()
    {
        AuthManager.Instance.Logout();
        MainUIManager.Instance.ShowLogin();
    }

    public void Quit()
    {
        StartCoroutine(QuitRoutine());
    }
    IEnumerator QuitRoutine()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager
                .Instance
                .SaveGame();
        }

        bool done =
            false;

        bool success =
            false;

        if (!GameLaunchData.IsGuest
            && CloudSaveManager.Instance != null)
        {
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
                    "Cloud Upload Failed");
            }
            else
            {
                Debug.Log(
                    "Cloud Upload Success");
            }
        }

        Debug.Log(
            "Quit Game");

        Application.Quit();
    }
}