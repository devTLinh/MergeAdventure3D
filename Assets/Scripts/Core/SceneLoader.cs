using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{
    public static SceneLoader Instance;
    [SerializeField] Transform player;
    string currentMap;
    void Awake()
    {
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void LoadMap(string sceneName){
        if (!string.IsNullOrEmpty(currentMap)) return;
        StartCoroutine(LoadMapRoutine(sceneName));
    }
    IEnumerator LoadMapRoutine(string sceneName){
        currentMap = sceneName;
        CoreGameplayController.Instance.HideCore();
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return null;
        MapSpawn spawn = FindObjectOfType<MapSpawn>();
        yield return StartCoroutine(TeleportPlayerRoutine(spawn));
    }
    IEnumerator TeleportPlayerRoutine(MapSpawn spawn){
        if (player == null || spawn == null) yield break;
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        yield return null;
        yield return new WaitForEndOfFrame();
        player.position = spawn.transform.position;
        player.rotation = spawn.transform.rotation;
        Physics.SyncTransforms();
        yield return null;
        if (cc != null) cc.enabled = true;
        Debug.Log("TP OK -> " + player.position);
    }
    public void ReturnToCore(){
        if (string.IsNullOrEmpty(currentMap)) return;
        StartCoroutine(ReturnRoutine());
    }
    IEnumerator ReturnRoutine()
    {
        yield return SceneManager.UnloadSceneAsync(currentMap);
        yield return null;
        CoreGameplayController.Instance.ShowCore();
        MapSpawn spawn = FindObjectOfType<MapSpawn>();
        yield return StartCoroutine(TeleportPlayerRoutine(spawn));
        currentMap = null;
    }
}