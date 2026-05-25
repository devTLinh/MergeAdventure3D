using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections.Generic;
public class OrdersPanelUI : MonoBehaviour {
    [SerializeField] private Text textName;
    [SerializeField] private List<Text> textRequirements;
    [SerializeField] private List<Text> textRewards;

    public void SetOrder(RuntimeOrder order) {
        textName.text = order.source.customerName;
        for (int i = 0; i < textRequirements.Count; i++) {
            if (i < order.source.requirements.Count) {
                OrderRequirement req = order.source.requirements[i];
                textRequirements[i].text = req.item.mergeGroup + " Level " + req.item.level + ": " + req.item.itemName + " (" + order.remain[i] + ")";
                textRequirements[i].gameObject.SetActive(true);
            } else {
                textRequirements[i].gameObject.SetActive(false);
            }
        }
        if(order.source.rewardEnergy > 0) {
            textRewards[0].text = "+ " + order.source.rewardEnergy +" energy";
            textRewards[0].gameObject.SetActive(true);
        } else {
            textRewards[0].gameObject.SetActive(false);
        }
        if (order.source.rewardCoin > 0)
        {
            textRewards[1].text = "+ " + order.source.rewardCoin + " coin";
            textRewards[1].gameObject.SetActive(true);
        }
        else
        {
            textRewards[1].gameObject.SetActive(false);
        }
    }
}