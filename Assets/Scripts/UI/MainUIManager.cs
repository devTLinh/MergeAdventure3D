using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour{
    public static MainUIManager Instance;
    public GameObject loginPanel;
    public GameObject menuPanel;
    public GameObject settingsPanel;
    public GameObject loadingPanel;
    public GameObject profilePanel;
    void Awake(){
        Instance = this;
    }
    void Start()
    {
        RefreshUIState();
    }
    public void RefreshUIState()
    {
        if (AuthManager.Instance != null &&
            AuthManager.Instance.CurrentUser != null)
        {
            ShowMenu();
        }
        else
        {
            ShowLogin();
        }
    }
    void HideAll(){
        loginPanel.SetActive(false);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        profilePanel.SetActive(false);
        loadingPanel.SetActive(false);
    }
    public void ShowLogin(){
        HideAll();
        loginPanel.SetActive(true);
    }
    public void ShowMenu(){
        HideAll();
        menuPanel.SetActive(true);
    }
    public void ShowSettings(){
        settingsPanel.SetActive(true);
    }

    public void HideSettings(){
        settingsPanel.SetActive(false);
    }
    public void ShowProfile(){
        profilePanel.SetActive(true);
    }

    public void HideProfile(){
        profilePanel.SetActive(false);
    }

    public void ShowLoading(){
        loadingPanel.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(
            "CoreGame");
    }
    public void ShowMenuDelayed()
    {
        StartCoroutine(
            MenuDelayRoutine());
    }

    IEnumerator MenuDelayRoutine()
    {
        ShowLoading();

        yield return new WaitForSeconds(
            1.5f);

        ShowMenu();
    }
}