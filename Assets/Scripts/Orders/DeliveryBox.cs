using UnityEngine;
public class DeliveryBox : MonoBehaviour { 
    public void Deliver() {
        ItemView item = PlayerHoldSystem.Instance.HeldItem;
        if (item == null) return;
        bool ok = OrderManager.Instance.TryDeliver(item); 
        if (ok) PlayerHoldSystem.Instance.ClearHeld();
    } 
}