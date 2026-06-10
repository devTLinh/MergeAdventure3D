using UnityEngine;
public class TreasureChest : MonoBehaviour, IHoverInfo {
    public void Open() {
        int roll = Random.Range(0, 100);
        if (roll < 60) { //40
            EnergyManager.Instance.Set(EnergyManager.Instance.current + 5); 
            NotificationUI.Instance.Show("Chest +5 Energy");
        } 
        else if (roll < 90) { //70
            ExplorationEnergyManager.Instance.Set(ExplorationEnergyManager.Instance.CurrentEnergy + 4);
            NotificationUI.Instance.Show("Chest +4 Explore");
        }
        else
        {
            NotificationUI.Instance.Show("Empty Chest!");
        }
        /* Development
        else if (roll < 90) { 
            NotificationUI.Instance.Show("Utility Item"); 
        } 
        else {
            NotificationUI.Instance.Show("Rare Item");
        }*/
        Destroy(gameObject);
    }
    public string GetHoverText()
    {
        return
            "[Treasure Chest]\n" +
            "Left Click: Open";
    }
}