using UnityEngine;
public class MergeManager : MonoBehaviour {
    public static MergeManager Instance;
    [SerializeField]
    GameObject mergeVfxPrefab;
    void Awake() {
        Instance = this;
    } 
    public bool TryMerge(ItemView a, ItemView b) {
        if (a.Model.Data.mergeGroup != b.Model.Data.mergeGroup) return false;
        if (a.Model.Data.level != b.Model.Data.level) return false; 
        AudioManager.Instance.PlaySfx(4);
        BoardSlot slot = b.CurrentSlot; 
        b.CurrentSlot.Clear();
        ItemData next = ItemDatabase.Instance.Get(a.Model.Data.mergeGroup, a.Model.Data.level + 1);
        Destroy(a.gameObject);
        Destroy(b.gameObject);
        Instantiate(
    mergeVfxPrefab,
    slot.transform.position +
    Vector3.up * 0.5f,
    Quaternion.identity);
        ItemFactory.Instance.SpawnToSlot(slot, next);
        TutorialManager.Instance.Notify(TutorialStep.MergeItems);
        return true;
    }
}