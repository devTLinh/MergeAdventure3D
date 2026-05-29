using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUIController :
MonoBehaviour
{
    public Text usernameText;
    public Text userIdText;
    public Text saveStatusText;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (GameMode.IsGuest)
        {
            usernameText.text =
                "Guest";

            userIdText.text =
                "-";

            saveStatusText.text =
                HasLocalSave()
                ? "Local Save"
                : "Empty";

            return;
        }
        if (AuthManager.Instance == null)
            return;

        var user =
            AuthManager.Instance.CurrentUser;

        if (user == null)
            return;

        usernameText.text = user.DisplayName;

        //if (string.IsNullOrEmpty(
        //    user.DisplayName))
        //{
        //    usernameText.text = "Local User";
        //}

        userIdText.text = user.UserId;

        saveStatusText.text =
            GameLaunchData
            .HasCloudSave
            ? "Cloud Save Found"
            : "No Cloud Save";
    }
    bool HasLocalSave()
    {
        string path =
            Application
            .persistentDataPath
            + "/save.json";

        return File.Exists(
            path);
    }
    public void Logout()
    {
        //
        if (GameLaunchData.IsGuest)
        {
            MainUIManager
                .Instance
                .ShowLogin();

            return;
        }
        // Logout the user
        AuthManager.Instance.Logout();
    }

    public void Back()
    {
        MainUIManager.Instance.HideProfile();
    }
}