using UnityEngine;
public class PlayerInteraction : MonoBehaviour { 
    public Camera cam;
    public float range = 4f;
    [Header("Hold Position")]
    public float holdForward = 1.2f;
    public float holdRight = 0.55f;
    public float holdDown = 0.25f;
    public float followSpeed = 12f;
    public ItemView heldItem; 
    void Update() { 
        if (Input.GetKeyDown(KeyCode.E)) { 
            TryInteract(); 
        }
        UpdateHeldItem(); 
    }
    void TryInteract() {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range)) {
            if (heldItem)
            {
                BoardSlot slot = hit.collider.GetComponentInParent<BoardSlot>();
                if (slot != null)
                {
                    Debug.Log("Interacted with slot at: " + slot.pos);
                    TryPlace(slot);
                    return;
                }
            }
            else
            {
                ItemView item = hit.collider.GetComponent<ItemView>();
                if (item != null)
                {
                    TryPickup(item);
                    Debug.Log("Picked up item: " + item.model.data.name);
                    return;
                }
            }  
        }
    } 
    void TryPickup(ItemView item) { 
        //if (heldItem != null) return;
        heldItem = item;
        BoardSlot oldSlot = item.transform.parent.GetComponent<BoardSlot>();
        if (oldSlot != null) {
            oldSlot.Clear();
        }
        DisableHeldCollision(item);
        item.transform.SetParent(null); 
    } 
    void TryPlace(BoardSlot slot) { 
        //if (heldItem == null) return;
        EnableHeldCollision(heldItem);
        if (slot.IsEmpty()) { 
            slot.SetItem(heldItem); 
            heldItem = null;
        } 
        else {
            ItemView existing = slot.currentItem;
            if (MergeManager.Instance.CanMerge(existing, heldItem)) {
                slot.Clear();
                MergeManager.Instance.Merge(existing, heldItem, slot); 
                heldItem = null;
            }
        }
    } 
    void UpdateHeldItem() {
        if (heldItem == null) return;
        Vector3 targetPos = cam.transform.position + cam.transform.forward * holdForward + cam.transform.right * holdRight + cam.transform.up * (-holdDown);
        heldItem.transform.position = Vector3.Lerp(heldItem.transform.position, targetPos, Time.deltaTime * followSpeed);
        heldItem.transform.rotation = Quaternion.Lerp(heldItem.transform.rotation, cam.transform.rotation, Time.deltaTime * followSpeed); 
    }
    void DisableHeldCollision(ItemView item) {
        Collider col = item.GetComponent<Collider>();
        if (col != null) col.enabled = false; 
    } 
    void EnableHeldCollision(ItemView item) { 
        Collider col = item.GetComponent<Collider>();
        if (col != null) col.enabled = true; 
    } 
}