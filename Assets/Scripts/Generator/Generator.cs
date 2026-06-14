using UnityEngine;
public class Generator : MonoBehaviour, IHoverInfo{
    [SerializeField] GeneratorData data;
    int charges;
    float nextReady = 0f; 
    void Start() { 
        charges = data.maxCharges;
    } 
    public void Use() {
        if (!CanUse()) return;
        BoardSlot slot = BoardManager.Instance.GetFirstEmpty();
        if (slot == null) return;
        EnergyManager.Instance.Spend(data.energyCost);
        charges--;
        nextReady = Time.time + data.cooldown; 
        ItemData drop = data.drops[Random.Range(0, data.drops.Count)].item;
        ItemFactory.Instance.SpawnToSlot(slot, drop);
        NotificationUI.Instance.Show("Spawned " + drop.itemName);
        AudioManager.Instance.PlaySfx(1);
        TutorialManager.Instance.Notify(TutorialStep.UseGenerator);
    }
    bool CanUse() {
        return charges > 0 && Time.time >= nextReady && EnergyManager.Instance.HasEnough(data.energyCost); 
    }
    public string GetHoverText()
    {
        return
            "[ "+ data.displayName +" ]\n" +
            "Left Click: Produce";
    }
}