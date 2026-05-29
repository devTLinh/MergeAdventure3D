using UnityEngine;
using Firebase;
using Firebase.Extensions;

public class FirebaseManager :
MonoBehaviour
{
    public static FirebaseManager
        Instance;

    public bool Ready
    {
        get;
        private set;
    }

    FirebaseApp app;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(
            gameObject);
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

            InitFirebase();
        });
    }

    void InitFirebase()
    {
        if (Ready)
            return;

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
            app =
                FirebaseApp
                .Create(
                    options);

        Ready =
            true;

        Debug.Log(
            "Firebase Ready");

        AuthManager
        .Instance
        ?.Init(app);

        CloudSaveManager
            .Instance
            ?.Init(app);

        AuthManager
            .Instance
            ?.CheckLoginState();
    }

    public FirebaseApp
        GetApp()
    {
        return app;
    }
}