using UnityEngine;
public class ExplorationNodeManager : MonoBehaviour { 
    public static ExplorationNodeManager Instance;
    void Awake() { 
        Instance = this; 
    }
    public void TryUnlock(ExplorationNode node) { 
        if (node == null) return;
        if (!node.CanUnlock()) return;
        ExplorationEnergyManager.Instance.Spend(node.unlockCost);
        node.Unlock();
        NotificationUI.Instance.Show("Unlocked Area");

    } 
}