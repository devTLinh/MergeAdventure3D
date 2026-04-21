using UnityEngine;
public class MergeManager : MonoBehaviour {
    public static MergeManager Instance; 
    void Awake() {
        Instance = this;
    } 
    public void TryMerge(ItemView a, ItemView b) {
        if (a.Model.Data.mergeGroup != b.Model.Data.mergeGroup) return;
        if (a.Model.Data.level != b.Model.Data.level) return; 
        BoardSlot slot = b.CurrentSlot; 
        b.CurrentSlot.Clear();
        ItemData next = ItemDatabase.Instance.Get(a.Model.Data.mergeGroup, a.Model.Data.level + 1);
        Destroy(a.gameObject);
        Destroy(b.gameObject);
        ItemFactory.Instance.SpawnToSlot(slot, next);
    }
}