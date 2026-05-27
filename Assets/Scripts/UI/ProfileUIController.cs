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
        if (AuthManager.Instance == null)
            return;

        var user =
            AuthManager.Instance.CurrentUser;

        if (user == null)
            return;

        usernameText.text = user.DisplayName;

        if (string.IsNullOrEmpty(
            user.DisplayName))
        {
            usernameText.text = "Local User";
        }

        userIdText.text = user.UserId;

        saveStatusText.text = HasLocalSave() ? "Found" : "Empty";
    }

    bool HasLocalSave()
    {
        string path =
            Application.persistentDataPath
            + "/save.json";

        return
            System.IO.File.Exists(path);
    }

    public void Logout()
    {
        AuthManager.Instance.Logout();
    }

    public void Back()
    {
        MainUIManager.Instance.HideProfile();
    }
}