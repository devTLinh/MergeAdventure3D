using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using UnityEngine;
public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance;
    FirebaseAuth auth;
    FirebaseFirestore db;

    public FirebaseUser CurrentUser;

    void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        auth =
            FirebaseAuth.DefaultInstance;

        db =
            FirebaseFirestore.DefaultInstance;

        Debug.Log(
            "Auth Ready");
    }

    public void CheckLoginState()
    {
        CurrentUser = auth.CurrentUser;

        if (CurrentUser != null){
            Debug.Log("Auto Login");

            MainUIManager.Instance.ShowMenu();
        }
        else
        {
            MainUIManager.Instance.ShowLogin();
        }
    }

    string BuildEmail(
        string username)
    {
        return username
            + "@mergeadventure.local";
    }

    public void Login(
        string username,
        string password)
    {
        LoginUI.Instance.ClearError();
        string email =
            BuildEmail(username);

        auth
        .SignInWithEmailAndPasswordAsync(
            email,
            password)
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.IsFaulted)
            {
                LoginUI.Instance.ShowError(
                    "Incorrect username or password!");
                return;
            }

            CurrentUser =
                task.Result.User;

            MainUIManager.Instance.ShowMenu();
        });
    }

    public void Register(
        string username,
        string password)
    {
        string email = BuildEmail(username);
        auth.CreateUserWithEmailAndPasswordAsync(
            email,
            password)
        .ContinueWithOnMainThread(
        task =>
        {
            if (task.IsFaulted)
            {
                FirebaseException ex = task.Exception.GetBaseException() as FirebaseException;

                string msg =
                    "Register Failed";

                if (ex != null)
                {
                    AuthError error =
                        (AuthError)ex.ErrorCode;

                    switch (error)
                    {
                        case AuthError.EmailAlreadyInUse:
                            msg =
                                "Username already exists";
                            break;

                        case AuthError.WeakPassword:
                            msg =
                                "Password too weak";
                            break;

                        case AuthError.InvalidEmail:
                            msg =
                                "Invalid username";
                            break;
                    }
                }

                LoginUI.Instance.ShowError(
                    msg);

                return;
            }

            CurrentUser =
                task.Result.User;
            UserProfile profile = new UserProfile{ DisplayName = username};

            CurrentUser
            .UpdateUserProfileAsync(
                profile)
            .ContinueWithOnMainThread(
            t =>
            {
                SaveProfile(username);

                MainUIManager
                    .Instance
                    .ShowMenu();
            });
        });
    }

    void SaveProfile(
        string username)
    {
        Dictionary<string, object>
            data =
            new Dictionary<string, object>();

        data["username"] =
            username;

        db.Collection("users")
            .Document(CurrentUser.UserId)
            .SetAsync(data);
    }

    public void Logout()
    {
        auth.SignOut();

        CurrentUser = null;

        MainUIManager
            .Instance
            .ShowLogin();
    }
}