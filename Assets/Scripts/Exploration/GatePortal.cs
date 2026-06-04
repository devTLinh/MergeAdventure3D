using UnityEngine;

public class GatePortal :
MonoBehaviour, IHoverInfo
{
    public void Interact()
    {
        TutorialManager.Instance.Notify(TutorialStep.EnterPortal);
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