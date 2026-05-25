using UnityEngine;
public class NodeMarker : MonoBehaviour
{
    [SerializeField] ExplorationNode node;
    public void Interact()
    {
        ExplorationNodeManager.Instance.TryUnlock(node);
    }
}