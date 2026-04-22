using UnityEngine;
public class GeneratorUnlockSystem : MonoBehaviour {
    public static GeneratorUnlockSystem Instance;
    [SerializeField] private Generator generatorPrefab;
    private void Awake() { 
        Instance = this;
    }
    public static void UnlockAt(Vector3 pos) {
        if (Instance.generatorPrefab == null) return;
        Instantiate(Instance.generatorPrefab, pos + Vector3.up * 0.5f, Quaternion.identity);
        NotificationUI.Instance.Show("New Generator");
    }
}