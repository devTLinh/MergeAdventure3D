using UnityEngine;

public class PlayerHoldSystem :
MonoBehaviour
{
    public static PlayerHoldSystem Instance;
    [SerializeField] Transform holdPoint;
    public ItemView HeldItem{
        get;
        private set;
    }

    void Awake(){
        Instance = this;
    }

    public bool IsHolding(){
        return HeldItem != null;
    }

    public void Pickup(ItemView item){
        if (item == null) return;
        if (item == HeldItem) return;
        if (HeldItem != null){
            bool merged =  MergeManager.Instance .TryMerge(HeldItem,item);
            if (merged){
                HeldItem = null;
            }
            return;
        }
        HeldItem = item;
        if (item.CurrentSlot != null){
            item.CurrentSlot.Clear();
        }
        item.transform.SetParent(holdPoint);
        item.transform.localPosition =new Vector3( 0.35f, -0.2f,0.6f);
        Collider col = item.GetComponent<Collider>();
        if (col != null){
            col.enabled = false;
        }
        TutorialManager.Instance.Notify(TutorialStep.PickupItem);
    }

    public void Place( BoardSlot slot){
        if (HeldItem == null) return;
        if (slot.IsEmpty()){
            Collider col = HeldItem.GetComponent<Collider>();
            if (col != null){
                col.enabled = true;
            }
            slot.SetItem(HeldItem);
            HeldItem = null;
            TutorialManager.Instance.Notify(TutorialStep.PlaceItem);
            return;
        }
        bool merged = MergeManager.Instance.TryMerge(HeldItem,slot.currentItem);
        if (merged){
            HeldItem = null;
        }
    }
    public void ClearHeld(){
        HeldItem = null;
    }
}