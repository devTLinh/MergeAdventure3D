using UnityEngine;
public class ExplorationNodeManager : MonoBehaviour { 
    public static ExplorationNodeManager Instance;
    void Awake() { 
        Instance = this; 
    }
    public void TryUnlock(ExplorationNode node) { 
        if (node == null) return;
        if (!node.CanUnlock()) return;
        AudioManager.Instance.PlaySfx(8);
        ExplorationEnergyManager.Instance.Spend(node.unlockCost);
        node.Unlock();
    } 
}