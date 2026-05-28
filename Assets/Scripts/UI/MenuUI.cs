using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private void Start()
    {
        continueButton.SetActive(GameLaunchData.HasCloudSave);
    }
    void OnEnable()
    {
        continueButton.SetActive(GameLaunchData.HasCloudSave);
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
        Application.Quit();
    }
}