using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Merge Adventure/Order")] 
public class OrderData : ScriptableObject { 
    public string orderId; 
    public string customerName; 
    public List<OrderRequirement> requirements = new List<OrderRequirement>(); 
    [Header("Rewards")] 
    public int rewardEnergy = 5;
    public int rewardCoin = 0;
}