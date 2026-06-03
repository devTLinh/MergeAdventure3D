using UnityEngine;
public class DeliveryBox : MonoBehaviour, IHoverInfo { 
    public void Deliver() {
        ItemView item = PlayerHoldSystem.Instance.HeldItem;
        if (item == null) return;
        bool ok = OrderManager.Instance.TryDeliver(item); 
        if (ok) PlayerHoldSystem.Instance.ClearHeld();
    }
    public string GetHoverText()
    {
        return
            "[Delivery Box]\n" +
            "Left Click: Deliver";
    }
}