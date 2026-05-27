using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseManager :
MonoBehaviour
{
    public static FirebaseManager
        Instance;

    public bool Ready;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FirebaseApp
        .CheckAndFixDependenciesAsync()
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.Result
                != DependencyStatus.Available)
            {
                Debug.LogError(
                    "Firebase deps failed");

                return;
            }

            AppOptions options =
                new AppOptions
                {
                    ApiKey =
                        "AIzaSyDzhHCln_5GU41hCbkiESX8RlR13GbzsWM",

                    ProjectId =
                        "mergeadventure3d-53c34",

                    AppId =
                        "1:230941826482:web:7ed444831653e7e70718ab",

                    StorageBucket =
                        "mergeadventure3d-53c34.firebasestorage.app"
                };

            FirebaseApp.Create(
                options);

            Ready = true;

            Debug.Log(
                "Firebase Ready");

            AuthManager
                .Instance
                .Init();

            AuthManager
                .Instance
                .CheckLoginState();
        });
    }
}