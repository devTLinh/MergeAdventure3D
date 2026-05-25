//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MenuUI : MonoBehaviour{
//    public void Play(){
//        StartGame();
//    }

//    public void Continue(){
//        StartGame();
//    }
//    void StartGame(){
//        MainUIManager.Instance.ShowLoading();
//        StartCoroutine(Loading());
//    }
//    IEnumerator Loading(){
//        yield return SceneManager.LoadSceneAsync("CoreGame", LoadSceneMode.Additive);
//        Scene scene = SceneManager.GetSceneByName("CoreGame");

//        SceneManager.SetActiveScene(scene);
//    }
//    public void Settings(){
//        MainUIManager.Instance.ShowSettings();
//    }
//    public void Profile(){
//        MainUIManager.Instance.ShowProfile();
//    }
//    public void Logout(){
//        FirebaseManager.Instance.Logout();
//        MainUIManager.Instance.ShowLogin();
//    }

//    public void Quit()
//    {
//        Application.Quit();
//    }
//}