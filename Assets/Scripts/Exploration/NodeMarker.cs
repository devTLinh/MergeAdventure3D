using UnityEngine;
public class NodeMarker : MonoBehaviour, IHoverInfo
{
    [SerializeField] ExplorationNode node;
    public void Interact()
    {
        Debug.Log("interact");
        ExplorationNodeManager.Instance.TryUnlock(node);
    }
    public string GetHoverText()
    {
        return
            "[Exploration Node] Left Click: Unlock \n" +
            "Cost: " + node.unlockCost + " Energy\n";
    }
}