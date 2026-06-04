using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class OrderManager : MonoBehaviour { 
    public static OrderManager Instance;
    [Header("Database")]
    [SerializeField] private List<OrderData> orderPool = new List<OrderData>();
    [Header("Settings")]
    [SerializeField] private int maxActiveOrders = 3;
    public List<RuntimeOrder> ActiveOrders = new List<RuntimeOrder>();
    private int nextOrderIndex = 0;
    private void Awake() { 
        Instance = this;
    } 
    private void Start() {
        nextOrderIndex = GetNextOrderIndex();
    } 
    public void FillOrders() {
        while (ActiveOrders.Count < maxActiveOrders) { 
            OrderData data = orderPool[nextOrderIndex];
            ActiveOrders.Add(new RuntimeOrder(data, nextOrderIndex));
            nextOrderIndex = (nextOrderIndex + 1) % orderPool.Count;
        }
    }
    public int GetNextOrderIndex()
    {
        int max = -1;
        foreach (RuntimeOrder order in ActiveOrders)
        {
            if (order.orderIndex  > max){
                max = order.orderIndex;
            }
        }
        return max + 1;
    }
    public OrderData GetOrderData(int index) {
        if (index < 0 || index >= orderPool.Count) return null;
        return orderPool[index];
    }
    public bool TryDeliver(ItemView item) { 
        if (item == null) return false;
        for (int i = 0; i < ActiveOrders.Count; i++) {
            RuntimeOrder order = ActiveOrders[i];
            for (int j = 0; j < order.source.requirements.Count; j++) {
                OrderRequirement req = order.source.requirements[j];
                if (req.item == item.Model.Data && order.remain[j] > 0) { 
                    order.remain[j]--;
                    Object.Destroy(item.gameObject);
                    NotificationUI.Instance.Show("Delivered!");
                    if (order.IsCompleted()) CompleteOrder(order);
                    return true;
                }
            }
        } 
        NotificationUI.Instance.Show("No Order Needs This");
        return false;
    }
    void CompleteOrder(RuntimeOrder order) {
        ExplorationEnergyManager.Instance.CurrentEnergy += order.source.rewardEnergy;
        NotificationUI.Instance.Show("Order Complete! +" + order.source.rewardEnergy + " Energy");
        ActiveOrders.Remove(order);
        FillOrders();
        TutorialManager.Instance.Notify(TutorialStep.CompleteOrder);
    }
}