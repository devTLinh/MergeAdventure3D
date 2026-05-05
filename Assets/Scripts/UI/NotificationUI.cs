using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class NotificationUI : MonoBehaviour {
    public static NotificationUI Instance;
    [SerializeField] private Text textUI;
    Coroutine routine;
    public CanvasGroup canvasGroup;
    private void Awake() { 
        Instance = this;
    }
    public void Show(string msg) {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ShowRoutine(msg)); 
    }
    IEnumerator ShowRoutine(string msg) {
        textUI.text = msg;
        textUI.enabled = true; 
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        yield return new WaitForSeconds(2f);
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        textUI.enabled = false;
    }
}