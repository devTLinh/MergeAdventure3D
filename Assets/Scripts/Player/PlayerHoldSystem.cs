using UnityEngine;
public class PlayerHoldSystem : MonoBehaviour {
    public static PlayerHoldSystem Instance; 
    [SerializeField] Transform holdPoint;
    public ItemView HeldItem { get; private set; } 
    void Awake() { 
        Instance = this;
    } 
    public void Pickup(ItemView item) {
        if (HeldItem != null) {
            MergeManager.Instance.TryMerge(HeldItem, item);
            HeldItem = null;
            return;
        }
        HeldItem = item;
        if (item.CurrentSlot != null) item.CurrentSlot.Clear();
        item.transform.SetParent(holdPoint);
        item.transform.localPosition = new Vector3(0.35f, -0.2f, 0.6f);
    }
    public void Place(BoardSlot slot) {
        if (HeldItem == null) return;
        if (slot.IsEmpty()) { 
            slot.SetItem(HeldItem);
            HeldItem = null;
            return;
        }
        MergeManager.Instance.TryMerge(HeldItem, slot.currentItem);
        HeldItem = null;
    } 
    public void ClearHeld() { 
        HeldItem = null;
    }
}