using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSfx : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>()
            .onClick
            .AddListener(PlayClick);
    }

    void PlayClick()
    {
        AudioManager.Instance.PlaySfx(0);
    }
}