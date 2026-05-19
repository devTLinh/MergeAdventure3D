using UnityEngine;
public class NodeMarker : MonoBehaviour
{
    [SerializeField] ExplorationNode node;
    public void Interact()
    {
        if (node == null) return;
        node.Unlock();
    }
}