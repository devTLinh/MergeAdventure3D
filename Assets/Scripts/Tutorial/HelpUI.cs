using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HelpUI :
MonoBehaviour
{
    public static HelpUI Instance;

    public GameObject root;

    [SerializeField]
    Text titleText;

    [SerializeField]
    Text descText;

    [SerializeField]
    VideoPlayer player;

    void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        root.SetActive(true);

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;
    }

    public void Hide()
    {
        root.SetActive(false);

        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;
    }

    public void ShowPage(
        HelpPage page)
    {
        titleText.text =
            page.title;

        descText.text =
            page.description;

        player.Stop();

        player.clip =
            page.video;

        player.Play();
    }
}