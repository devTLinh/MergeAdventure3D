using UnityEngine;
using UnityEngine.UI;
public class BlinkText : MonoBehaviour
{
    [SerializeField]
    Text textUI;

    [SerializeField]
    float speed = 2f;

    void Update()
    {
        Color c =
            textUI.color;

        c.a =
            Mathf.Lerp(
                0.3f,
                1f,
                Mathf.PingPong(
                    Time.time * speed,
                    1f));

        textUI.color =
            c;
    }
}