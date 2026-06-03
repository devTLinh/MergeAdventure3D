using UnityEngine;

public class GatePortal :
MonoBehaviour, IHoverInfo
{
    public void Interact()
    {
        SceneLoader
            .Instance
            .LoadMap();
    }
    public string GetHoverText()
    {
        return
            "[Map Gate]\n" +
            "Left Click: Enter";
    }
}