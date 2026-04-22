using UnityEngine;
public class RegionTransitionSystem : MonoBehaviour { 
    public static void OpenGate(ExplorationNode node) { 
        NotificationUI.Instance.Show("Next Region Opened");
    }
}