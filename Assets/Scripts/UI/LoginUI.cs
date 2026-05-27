using UnityEngine;
using UnityEngine.UI;

public class LoginUI
    : MonoBehaviour
{
    public static LoginUI Instance;
    public InputField usernameInput;
    public InputField passwordInput;
    public Text errorText;
    void Awake()
    {
        Instance = this;
    }
    public void Login()
    {
        AuthManager.Instance.Login(usernameInput.text, passwordInput.text);
    }

    public void Register()
    {
        AuthManager.Instance.Register(usernameInput.text, passwordInput.text);
    }
    public void Guest()
    {
        MainUIManager.Instance.ShowMenu();
    }
    public void ShowError(
        string msg)
    {
        errorText.text = msg;
        errorText.gameObject
            .SetActive(true);
    }

    public void ClearError()
    {
        errorText.text = "";
        errorText.gameObject
            .SetActive(false);
    }
}