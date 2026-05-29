using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
public class CloudSaveManager :
MonoBehaviour
{
    public static CloudSaveManager
        Instance;

    FirebaseFirestore db;

    public bool IsReady
    {
        get;
        private set;
    }

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

    public void Init(
        FirebaseApp app)
    {
        if (IsReady)
            return;

        db =
            FirebaseFirestore
            .GetInstance(app);

        IsReady =
            true;

        Debug.Log(
            "Cloud Save Ready");
    }

    public void UploadSave(
        Action<bool> onDone)
    {
        // Guest mode
        if (!GameMode.UseCloud)
        {
            onDone(false);
            return;
        }
        if (!IsReady || db == null)
        {
            Debug.LogError(
                "Firestore not ready");

            onDone?.Invoke(
                false);

            return;
        }

        

        if (AuthManager.Instance == null
            || AuthManager.Instance.CurrentUser == null)
        {
            Debug.LogWarning(
                "No User");

            onDone?.Invoke(
                false);

            return;
        }

        string path =
            Application
            .persistentDataPath
            + "/save.json";

        if (!File.Exists(
            path))
        {
            Debug.LogWarning(
                "No save.json");

            onDone?.Invoke(
                false);

            return;
        }

        string json =
            File.ReadAllText(
                path);

        Dictionary<string, object>
            data =
            new Dictionary<string, object>();

        data["saveData"] =
            json;

        db.Collection(
            "users")
        .Document(
            AuthManager
            .Instance
            .CurrentUser
            .UserId)
        .SetAsync(
            data,
            SetOptions.MergeAll)
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(
                    "Cloud Upload Failed");

                if (task.Exception != null)
                {
                    Debug.LogException(
                        task.Exception);
                }

                onDone?.Invoke(
                    false);

                return;
            }

            Debug.Log(
                "Cloud Upload Success");

            onDone?.Invoke(
                true);
        });
    }

    public void DownloadSave(
        Action<bool> onDone)
    {
        // Guest mode
        if (!GameMode.UseCloud)
        {
            onDone(false);
            return;
        }

        if (!IsReady || db == null)
        {
            Debug.LogError(
                "Firestore not ready");

            onDone?.Invoke(
                false);

            return;
        }

        

        if (AuthManager.Instance == null
            || AuthManager.Instance.CurrentUser == null)
        {
            Debug.LogWarning(
                "No User");

            onDone?.Invoke(
                false);

            return;
        }

        db.Collection(
            "users")
        .Document(
            AuthManager
            .Instance
            .CurrentUser
            .UserId)
        .GetSnapshotAsync()
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError(
                    "Cloud Download Failed");

                if (task.Exception != null)
                {
                    Debug.LogException(
                        task.Exception);
                }

                onDone?.Invoke(
                    false);

                return;
            }

            DocumentSnapshot doc =
                task.Result;

            if (!doc.Exists
                || !doc.ContainsField(
                    "saveData"))
            {
                Debug.Log(
                    "No Cloud Save");

                onDone?.Invoke(
                    false);

                return;
            }

            string saveJson =
                doc.GetValue<string>(
                    "saveData");

            try
            {
                // Validate json
                JsonUtility
                    .FromJson<GameSaveData>(
                        saveJson);

                string path =
                    Application
                    .persistentDataPath
                    + "/save.json";

                File.WriteAllText(
                    path,
                    saveJson);

                Debug.Log(
                    "save.json synced");

                onDone?.Invoke(
                    true);
            }
            catch
            {
                Debug.LogError(
                    "Bad save json");

                onDone?.Invoke(
                    false);
            }
        });
    }
}