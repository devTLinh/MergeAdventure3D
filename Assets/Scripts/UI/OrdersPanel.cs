using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class OrdersPanel : MonoBehaviour {
    [SerializeField] private Text textUI; 
    private void Update() {
        Refresh();
    }
    void Refresh() { 
        if (OrderManager.Instance == null) return; 
        var orders = OrderManager.Instance.ActiveOrders;
        StringBuilder sb = new StringBuilder(); 
        sb.AppendLine("ORDERS");
        for (int i = 0; i < orders.Count; i++) { 
            RuntimeOrder order = orders[i];
            sb.AppendLine("------------"); 
            sb.AppendLine(order.source.customerName);
            for (int j = 0; j < order.source.requirements.Count; j++) {
                var req = order.source.requirements[j];
                sb.AppendLine(req.item.itemName + " x" + order.remain[j]);
            }
            sb.AppendLine("Reward +" + order.source.rewardEnergy + " Energy");
        }
        textUI.text = sb.ToString();
    }
}