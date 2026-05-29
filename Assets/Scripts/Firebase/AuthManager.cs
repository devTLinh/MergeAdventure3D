using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AuthManager :
MonoBehaviour
{
    public static AuthManager
        Instance;

    FirebaseAuth auth;
    FirebaseFirestore db;

    public FirebaseUser
        CurrentUser;

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

        auth =
            FirebaseAuth
            .GetAuth(app);

        db =
            FirebaseFirestore
            .GetInstance(app);

        IsReady =
            true;

        Debug.Log(
            "Auth Ready");
    }

    public void CheckLoginState()
    {
        if (!IsReady)
            return;

        // Guest mode
        if (GameLaunchData.IsGuest)
        {
            MainUIManager
                .Instance
                ?.ShowMenu();

            return;
        }

        CurrentUser = auth.CurrentUser;

        if (CurrentUser != null)
        {
            Debug.Log(
                "Auto Login");

            if (CloudSaveManager.Instance != null)
            {
                CloudSaveManager
                .Instance
                .DownloadSave(
                hasSave =>
                {
                    GameLaunchData
                        .HasCloudSave =
                        hasSave;

                    MainUIManager
                        .Instance
                        ?.ShowMenu();
                });
            }
            else
            {
                MainUIManager
                    .Instance
                    ?.ShowMenu();
            }
        }
        else
        {
            MainUIManager
                .Instance
                ?.ShowLogin();
        }
    }

    string BuildEmail(
        string username)
    {
        return username
            .Trim()
            .ToLower()
            + "@mergeadventure.local";
    }

    public void Login(
        string username,
        string password)
    {
        if (!IsReady)
            return;

        LoginUI.Instance
            ?.ClearError();

        string email =
            BuildEmail(
                username);

        auth
        .SignInWithEmailAndPasswordAsync(
            email,
            password)
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.IsFaulted)
            {
                MainUIManager
                    .Instance
                    ?.ShowLogin();

                LoginUI
                    .Instance
                    ?.ShowError(
                    "Incorrect username or password!");

                return;
            }

            CurrentUser =
                task.Result.User;

            if (CloudSaveManager.Instance != null)
            {
                CloudSaveManager
                .Instance
                .DownloadSave(
                hasSave =>
                {
                    GameLaunchData
                        .HasCloudSave =
                        hasSave;

                    GameLaunchData
                        .IsGuest =
                        false;

                    Debug.Log(
                        hasSave
                        ? "Cloud Save Found"
                        : "New Game");

                    MainUIManager
                        .Instance
                        ?.ShowMenu();
                });
            }
            else
            {
                GameLaunchData
                    .IsGuest =
                    false;

                MainUIManager
                    .Instance
                    ?.ShowMenu();
            }
        });
    }

    public void Register(
        string username,
        string password)
    {
        if (!IsReady)
            return;

        LoginUI.Instance
            ?.ClearError();

        string email =
            BuildEmail(
                username);

        auth
        .CreateUserWithEmailAndPasswordAsync(
            email,
            password)
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.IsFaulted)
            {
                MainUIManager
                    .Instance
                    ?.ShowLogin();

                string msg =
                    "Register Failed";

                FirebaseException ex =
                    task.Exception
                    ?.GetBaseException()
                    as FirebaseException;

                if (ex != null)
                {
                    AuthError error =
                        (AuthError)
                        ex.ErrorCode;

                    switch (error)
                    {
                        case AuthError
                            .EmailAlreadyInUse:
                            msg =
                                "Username already exists";
                            break;

                        case AuthError
                            .WeakPassword:
                            msg =
                                "Password too weak";
                            break;

                        case AuthError
                            .InvalidEmail:
                            msg =
                                "Invalid username";
                            break;
                    }
                }

                LoginUI
                    .Instance
                    ?.ShowError(
                    msg);

                return;
            }

            CurrentUser =
                task.Result.User;

            UserProfile profile =
                new UserProfile
                {
                    DisplayName =
                        username
                };

            CurrentUser
            .UpdateUserProfileAsync(
                profile)
            .ContinueWithOnMainThread(
            t =>
            {
                SaveProfile(
                    username);

                GameLaunchData
                    .HasCloudSave =
                    false;

                GameLaunchData
                    .IsGuest =
                    false;

                MainUIManager
                    .Instance
                    ?.ShowMenu();
            });
        });
    }

    void SaveProfile(
        string username)
    {
        if (db == null
            || CurrentUser == null)
            return;

        Dictionary<string, object>
            data =
            new Dictionary<string, object>();

        data["username"] =
            username;

        db.Collection(
            "users")
        .Document(
            CurrentUser.UserId)
        .SetAsync(
            data,
            SetOptions.MergeAll);
    }

    public void Logout()
    {
        // Guest logout
        if (GameLaunchData.IsGuest)
        {
            DeleteLocalSave();

            CurrentUser =
                null;

            GameLaunchData
                .IsGuest =
                false;

            GameLaunchData
                .HasCloudSave =
                false;

            MainUIManager
                .Instance
                ?.ShowLogin();

            return;
        }

        if (CloudSaveManager.Instance != null)
        {
            CloudSaveManager
            .Instance
            .UploadSave(
            success =>
            {
                FinishLogout();
            });
        }
        else
        {
            FinishLogout();
        }
    }

    void FinishLogout()
    {
        DeleteLocalSave();

        auth?.SignOut();

        CurrentUser =
            null;

        GameLaunchData
            .HasCloudSave =
            false;

        GameLaunchData
            .IsGuest =
            false;

        MainUIManager
            .Instance
            ?.ShowLogin();
    }

    void DeleteLocalSave()
    {
        string path =
            Application
            .persistentDataPath
            + "/save.json";

        if (File.Exists(
            path))
        {
            File.Delete(
                path);

            Debug.Log(
                "Local Save Deleted");
        }
    }

    public void LoginGuest()
    {
        CurrentUser =
            null;

        GameLaunchData
            .IsGuest =
            true;

        GameLaunchData
            .HasCloudSave =
            false;

        MainUIManager
            .Instance
            ?.ShowMenu();

        Debug.Log(
            "Guest Mode");
    }
}