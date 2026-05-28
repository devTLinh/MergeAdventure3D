using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour{
    public static FirebaseManager Instance;
    public bool Ready;
    FirebaseApp app;
    void Awake(){
        if (Instance != null){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start(){
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(
        task =>{
            if (task.Result != DependencyStatus.Available){
                Debug.Log("Firebase deps failed");
                return;
            }
            InitFirebase();
        });
    }
    void InitFirebase(){
        try{
            app = FirebaseApp.DefaultInstance;
        }
        catch{
            AppOptions options = new AppOptions{
                    ApiKey = "AIzaSyDzhHCln_5GU41hCbkiESX8RlR13GbzsWM",
                    ProjectId = "mergeadventure3d-53c34",
                    AppId = "1:230941826482:web:7ed444831653e7e70718ab",
                    StorageBucket = "mergeadventure3d-53c34.firebasestorage.app"
                };

            app = FirebaseApp.Create(options);
        }
        Ready = true;
        Debug.Log("Firebase Ready");
        AuthManager.Instance.Init();
        CloudSaveManager.Instance.Init();
        AuthManager.Instance.CheckLoginState();
    }
}