using UnityEngine;
using UnityEngine.UI;

public class HoverUI : MonoBehaviour
{
    public static HoverUI Instance;

    [SerializeField]
    GameObject root;

    [SerializeField]
    Text infoText;

    void Awake()
    {
        Instance = this;
    }

    public void Show(string msg)
    {
        root.SetActive(true);
        infoText.text = msg;
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}