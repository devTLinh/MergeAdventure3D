using System.Collections;
using UnityEngine;

public class MergePopEffect :
MonoBehaviour
{
    IEnumerator Start()
    {
        Vector3 target =
            transform.localScale;

        transform.localScale =
            target * 1.4f;

        float t = 0f;

        while (t < 0.15f)
        {
            t += Time.deltaTime;

            transform.localScale =
                Vector3.Lerp(
                    target * 1.4f,
                    target,
                    t / 0.15f);

            yield return null;
        }

        transform.localScale =
            target;
    }
}