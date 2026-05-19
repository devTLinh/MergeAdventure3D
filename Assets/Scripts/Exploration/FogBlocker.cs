using UnityEngine;
using System.Collections;

public class FogBlocker : MonoBehaviour
{
    [Header("Settings")]
    public float disableDelay = 2f;
    ParticleSystem particle;
    Collider collider;
    bool isFading;
    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        collider = GetComponentInChildren<Collider>();
    }

    public void FadeOut()
    {
        if (isFading) return;
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        isFading = true;

        // Disable collider ngay
        collider.enabled = false;

        // Stop particle emission
        particle.Stop();

        // Chờ particle tự tan
        yield return new WaitForSeconds(
            disableDelay);
        gameObject.SetActive(false);
    }
}