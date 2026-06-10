using UnityEngine;
public class RegionTransitionSystem : MonoBehaviour { 
    public static void OpenGate() {
        SceneLoader.Instance.ReturnToCore();
        NotificationUI.Instance.Show("Next Region Opened");
        switch (SceneLoader.Instance.currentMap)
        {
            case "ForestCamp":
                SceneLoader.Instance.currentMap = "AbandonedVillage";
                break;
            case "AbandonedVillage":
                SceneLoader.Instance.currentMap = "Desert";
                break;
            case "Desert":
                SceneLoader.Instance.currentMap = "LostCivilization";
                break;
        }
    }
}