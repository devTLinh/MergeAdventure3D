using UnityEngine;
public class ExplorationNodeManager : MonoBehaviour { 
    public static ExplorationNodeManager Instance;
    void Awake() { 
        Instance = this; 
    }
    public void TryUnlock(ExplorationNode node) { 
        if (node == null) return;
        if (!node.CanUnlock()) {
            NotificationUI.Instance.Show("Path Locked");
            return;
        } 
        if (!ExplorationEnergyManager.Instance.Spend(node.unlockCost)) {
            NotificationUI.Instance.Show("Need Explore Energy");
            return;
        }
        node.Unlock();
        RewardResolver.Resolve(node);
        NotificationUI.Instance.Show("Unlocked Area");
    } 
}