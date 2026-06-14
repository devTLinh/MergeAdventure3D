using UnityEngine;
public class RegionTransitionSystem : MonoBehaviour { 
    public static void OpenGate() {
        
        NotificationUI.Instance.Show("Next Region Opened");
        SceneLoader.Instance.oldMap = SceneLoader.Instance.currentMap;
        SceneLoader.Instance.currentMapSpawn = Vector3.zero;
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
        SceneLoader.Instance.ReturnToCore();
    }
}