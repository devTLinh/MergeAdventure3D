using UnityEngine;

public class CoreGameplayController : MonoBehaviour
{
    public static CoreGameplayController Instance;

    void Awake(){
        Instance = this;
    }

    public void ShowCore(){
        gameObject.SetActive(true);
    }

    public void HideCore(){
        gameObject.SetActive(false);
    }
}