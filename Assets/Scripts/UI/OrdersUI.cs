using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class OrdersUI : MonoBehaviour { 
    [SerializeField] List<OrdersPanelUI> ordersPanelUI;
    [SerializeField] GameObject uIContainer;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleUI();
        }
        if (uIContainer.activeSelf)
        {
            Refresh();
        }
    }

    void ToggleUI()
    {
        bool isActive = !uIContainer.activeSelf;
        uIContainer.SetActive(isActive);
    }
    void Refresh()
    {
        if (OrderManager.Instance == null) return;
        var orders = OrderManager.Instance.ActiveOrders;
        for (int i = 0; i < ordersPanelUI.Count; i++)
        {
            if (i < orders.Count)
            {
                ordersPanelUI[i].gameObject.SetActive(true);
                ordersPanelUI[i].SetOrder(orders[i]);
            }
            else
            {
                ordersPanelUI[i].gameObject.SetActive(false);
            }
        }
    }
}