using UnityEngine;

public class AutoDestroy :
MonoBehaviour
{
    [SerializeField]
    float lifeTime = 1f;

    void Start()
    {
        Destroy(
            gameObject,
            lifeTime);
    }
}