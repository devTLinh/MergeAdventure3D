using UnityEngine;

public class GatePortal :
MonoBehaviour
{
    public void Interact()
    {
        SceneLoader
            .Instance
            .LoadMap();
    }
}