using UnityEngine;
using System.Collections.Generic;
public class ExplorationNode : MonoBehaviour {
    public string nodeId;
    public bool unlocked; 
    public int unlockCost = 3;
    public NodeRewardType rewardType;
    public List<ExplorationNode> connectedNodes = new List<ExplorationNode>(); 
    [Header("Visual")] 
    public GameObject fogBlocker; 
    public GameObject marker; 
    private void Start() {
        Refresh(); 
    } 
    public void Refresh() {
        if (fogBlocker != null) fogBlocker.SetActive(!unlocked);
        if (marker != null) marker.SetActive(true);
    } 
    public bool CanUnlock() {
        if (unlocked) return false;
        if (connectedNodes.Count == 0) return true;
        foreach (var node in connectedNodes) { 
            if (node != null && node.unlocked) return true;
        }
        return false;
    } 
    public void Unlock() {
        unlocked = true;
        Refresh();
    }
}