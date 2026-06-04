using System.Collections.Generic;
using UnityEngine;

public class HelpManager :
MonoBehaviour
{
    public static HelpManager Instance;

    public List<HelpPage> pages;

    void Awake()
    {
        Instance = this;
    }

    public void OpenPage(
        int index)
    {
        if (!HelpUI.Instance.root.activeSelf)
        {
            HelpUI.Instance.Show();
        }

        HelpUI.Instance.ShowPage(
            pages[index]);
    }
}