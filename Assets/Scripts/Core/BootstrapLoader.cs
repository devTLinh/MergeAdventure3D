using UnityEngine;

public class BootstrapLoader : MonoBehaviour{
    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        CheckLogin();
    }
    void CheckLogin()
    {
        if (FirebaseManager.Instance.IsLoggedIn()){
            MainUIManager.Instance.ShowMenu();
        }
        else
        {
            MainUIManager.Instance.ShowLogin();
        }
    }
}