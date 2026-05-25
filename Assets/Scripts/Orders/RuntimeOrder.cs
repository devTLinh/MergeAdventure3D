using System.Collections.Generic;
public class RuntimeOrder {
    public OrderData source;
    public int orderIndex;
    public List<int> remain = new List<int>();
    public RuntimeOrder(OrderData data, int index) {
        source = data;
        orderIndex = index;
        foreach (var req in data.requirements) remain.Add(req.amount);
    }
    public bool IsCompleted() {
        for (int i = 0; i < remain.Count; i++) {
            if (remain[i] > 0) return false; 
        } 
        return true;
    }
}