using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{
    public static SceneLoader Instance;
    [SerializeField] Transform player;
    public Vector3 currentMapSpawn;
    public string currentMap;
    public string oldMap;
    void Awake()
    {
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void LoadMap(){
        if (string.IsNullOrEmpty(currentMap)) return;
        StartCoroutine(LoadMapRoutine());
    }
    IEnumerator LoadMapRoutine(){
        CoreGameplayController.Instance.HideCore();
        yield return SceneManager.LoadSceneAsync(currentMap, LoadSceneMode.Additive);
        yield return null;
        Debug.Log("Scene loaded: " + currentMapSpawn);
        if (currentMapSpawn == Vector3.zero){
            MapSpawn spawn = FindObjectOfType<MapSpawn>();
            Debug.Log(
    "Found Spawn = "
    + spawn.gameObject.scene.name);
            currentMapSpawn = spawn.transform.position;
            StartCoroutine(TeleportPlayerRoutine(spawn));
        }
        else yield return StartCoroutine(TeleportPlayerRoutine());
    }
    IEnumerator TeleportPlayerRoutine(){
        if (player == null || currentMapSpawn == null) yield break;
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        yield return null;
        yield return new WaitForEndOfFrame();
        player.position = currentMapSpawn;
        player.rotation = Quaternion.identity;
        SaveManager.Instance.LoadNodesForScene(currentMap);
        Physics.SyncTransforms();
        yield return null;
        if (cc != null) cc.enabled = true;
        Debug.Log("TP OK -> " + player.position);
    }
    IEnumerator TeleportPlayerRoutine(MapSpawn spawn)
    {
        if (player == null || spawn == null) yield break;
        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        yield return null;
        yield return new WaitForEndOfFrame();
        player.position = spawn.transform.position;
        player.rotation = spawn.transform.rotation;
        SaveManager.Instance.LoadNodesForScene(currentMap);
        Physics.SyncTransforms();
        yield return null;
        if (cc != null) cc.enabled = true;
        Debug.Log("TP OK -> " + player.position);
    }
    public void RestoreMapState(GameSaveData data)
    {
        currentMap = data.currentScene;
        currentMapSpawn = new Vector3(data.mapSpawnX, data.mapSpawnY, data.mapSpawnZ);
        Debug.Log("Loading...");
    }
    public void ReturnToCore(){
        if (string.IsNullOrEmpty(currentMap)) return;
        SaveManager.Instance.SaveNodesCurrentScene();
        StartCoroutine(ReturnRoutine());
        TutorialManager.Instance.Notify(TutorialStep.ReturnToCore);
    }
    IEnumerator ReturnRoutine()
    {
        if (!string.IsNullOrEmpty(oldMap)){
            yield return SceneManager.UnloadSceneAsync(oldMap);
            oldMap = null;
        }
        else yield return SceneManager.UnloadSceneAsync(currentMap);
        yield return null;
        CoreGameplayController.Instance.ShowCore();
        GeneratorUnlockSystem.Instance.RefreshGenerators();
        MapSpawn spawn = FindObjectOfType<MapSpawn>();
        yield return StartCoroutine(TeleportPlayerRoutine(spawn));
    }
}