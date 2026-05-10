using UnityEngine;

public class FogScroller : MonoBehaviour
{
    public Vector2 speed =
        new Vector2(0.01f, 0);

    Material mat;

    Vector2 offset;

    void Start()
    {
        mat = GetComponent<Renderer>()
            .material;
    }

    void Update()
    {
        offset += speed * Time.deltaTime;

        mat.mainTextureOffset =
            offset;
    }
}