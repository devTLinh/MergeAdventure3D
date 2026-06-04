using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public GameSaveData currentData;
    Dictionary<string, bool> nodeLookup = new Dictionary<string, bool>();
    string savePath;
    bool initialized;
    void Awake(){
        Instance = this;
        savePath = Application.persistentDataPath + "/save.json";
    }
    void Start(){
        InitializeGame();
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }
    public void InitializeGame()
    {
        if (initialized)
            return;

        initialized = true;

        if (GameLaunchData.StartNewGame)
        {
            NewGame(true);
            return;
        }

        bool canLoad =
            GameMode.IsGuest
            ? HasLocalSave()
            : GameLaunchData.HasCloudSave;

        if (canLoad)
            LoadGame();
        else
            NewGame();
    }

    public void NewGame(bool deleteOld = false)
    {
        if (deleteOld && File.Exists(savePath)){
            File.Delete(savePath);
        }
        currentData = new GameSaveData();
        nodeLookup.Clear();
        SceneLoader.Instance.currentMap = "ForestCamp";
        BoardManager.Instance.ClearBoard();
        OrderManager.Instance.ActiveOrders.Clear();
        OrderManager.Instance.FillOrders();
        EnergyManager.Instance.Set(EnergyManager.Instance.max);
        ExplorationEnergyManager.Instance.Set(ExplorationEnergyManager.Instance.MaxEnergy);
        EnergyRegenManager.Instance.StartRealtimeTimers();
        //Tutorial
        if (!TutorialManager.Instance.IsCompleted())
        {
            TutorialManager.Instance.StartTutorial();
        }
        Debug.Log("NEW GAME");
    }
    bool HasLocalSave()
    {
        return
            File.Exists(
                savePath);
    }
    public void SaveGame(){
        GameSaveData data = new GameSaveData();
        SavePlayer(data);
        SaveBoard(data);
        SaveOrders(data);
        SaveStateScene(data);
        data.nodes = new List<NodeSaveData>();
        if (currentData != null
            && currentData.nodes != null)
        {
            foreach (var n in currentData.nodes)
            {
                data.nodes.Add(
                    new NodeSaveData
                    {
                        nodeId =
                            n.nodeId,
                        unlocked =
                            n.unlocked
                    });
            }
        }
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath,json);
        currentData = data;
        Debug.Log("GAME SAVED");
        Debug.Log("Link: " + savePath);
    }
    public void LoadGame(){
        if (!File.Exists(savePath)){
            Debug.Log( "NO SAVE FILE");
            NewGame(false);
            return;
        }
        string json = File.ReadAllText(savePath);
        GameSaveData data = null;
        try
        {
            data =
                JsonUtility
                .FromJson<GameSaveData>(
                    json);
        }
        catch
        {
            Debug.LogError(
                "Bad Save File");

            NewGame(false);
            return;
        }

        if (data == null)
        {
            Debug.LogError(
                "Invalid Save");

            NewGame(false);
            return;
        }
        currentData = data;
        BuildNodeLookup();
        LoadPlayer(data);
        LoadBoard(data);
        LoadSceneState(data);
        LoadOrders(data);
        EnergyRegenManager.Instance.ApplyOfflineRegen(data);
        Debug.Log( "GAME LOADED");
    }
    void SavePlayer( GameSaveData data){
        data.energy = EnergyManager.Instance.current;
        data.exploreEnergy = ExplorationEnergyManager.Instance.CurrentEnergy;
        data.energyTimestamp = System.DateTime.UtcNow.ToString("o");
        data.exploreTimestamp =  System.DateTime.UtcNow.ToString("o");
    }

    void LoadPlayer(GameSaveData data){
        EnergyManager.Instance.Set(data.energy);
        ExplorationEnergyManager.Instance.Set(data.exploreEnergy);
    }
    void SaveBoard(GameSaveData data){
        data.boardItems.Clear();
        for (int i = 0; i < BoardManager.Instance.slots.Length; i++){
            BoardSlot slot = BoardManager.Instance.slots[i];
            if (slot.currentItem == null) continue;
            ItemSaveData save = new ItemSaveData();
            save.mergeGroup =  slot.currentItem.Model.Data.mergeGroup;
            save.level = slot.currentItem .Model.Data.level;
            save.slotIndex = i;
            data.boardItems.Add(save);
        }
    }
    void LoadBoard(GameSaveData data){
        if (data.boardItems == null) return;
        BoardManager.Instance.ClearBoard();
        foreach ( ItemSaveData save in data.boardItems){
            BoardSlot slot = BoardManager.Instance.slots[save.slotIndex];
            ItemData itemData = ItemDatabase.Instance.Get(save.mergeGroup, save.level);
            if (itemData == null){
                Debug.LogError("Missing ItemData");
                continue;
            }
            ItemFactory.Instance.SpawnToSlot(slot, itemData);
        }
    }
    void SaveOrders(GameSaveData data){
        data.activeOrders.Clear();
        foreach ( RuntimeOrder order in OrderManager.Instance.ActiveOrders){
            if (order == null) continue;
            data.activeOrders.Add(order.orderIndex);
        }
    }
    void LoadOrders(GameSaveData data){
        OrderManager.Instance.ActiveOrders.Clear();
        if (data.activeOrders == null || data.activeOrders.Count == 0){
            OrderManager.Instance.FillOrders();
            return;
        }
        foreach (int id in data.activeOrders){
            OrderData dataOrder = OrderManager.Instance.GetOrderData(id);
            if (dataOrder == null)
            {
                Debug.LogWarning(
                    "Missing OrderData "
                    + id);

                continue;
            }
            RuntimeOrder order = new RuntimeOrder(dataOrder, id);
            OrderManager.Instance.ActiveOrders.Add(order);
        }
    }
    void SaveStateScene(GameSaveData data){
        data.currentScene = SceneLoader.Instance.currentMap;
        Vector3 spawn = SceneLoader.Instance.currentMapSpawn;
        data.mapSpawnX = spawn.x;
        data.mapSpawnY = spawn.y;
        data.mapSpawnZ = spawn.z;
    }
    void LoadSceneState(GameSaveData data){
        if (string.IsNullOrEmpty(data.currentScene)){
            SceneLoader.Instance .currentMap = "ForestCamp";
            return;
        }
        SceneLoader.Instance.RestoreMapState(data);
    }
    void BuildNodeLookup(){
        nodeLookup.Clear();
        if (currentData == null || currentData.nodes == null) return;
        foreach ( NodeSaveData node in currentData.nodes){
            nodeLookup[node.nodeId] = node.unlocked;
        }
    }
    public void SaveNodesCurrentScene(){
        if (currentData == null){
            currentData = new GameSaveData();
        }
        if (currentData.currentScene != SceneLoader.Instance.currentMap){
            currentData.nodes = new List<NodeSaveData>();
            nodeLookup.Clear();
        }
        ExplorationNode[] nodes = FindObjectsOfType<ExplorationNode>();
        foreach (ExplorationNode node in nodes){
            nodeLookup[node.nodeId] = node.unlocked;
        }
        if (currentData.nodes == null)
        {
            currentData.nodes =
                new List<NodeSaveData>();
        }
        currentData.nodes.Clear();
        foreach ( var kv in nodeLookup){
            currentData.nodes.Add(new NodeSaveData{
                    nodeId = kv.Key,
                    unlocked = kv.Value
            });
        }
        Debug.Log("Nodes Saved");
    }
    public void LoadNodesForScene(string sceneName){
        if (currentData == null || currentData.currentScene != sceneName) return;
        ExplorationNode[] nodes = FindObjectsOfType<ExplorationNode>();
        foreach ( ExplorationNode node in nodes){
            if (nodeLookup.TryGetValue(node.nodeId, out bool unlocked) && unlocked){
                node.unlocked = true;
            }
        }
        Debug.Log("Node restore " + sceneName);
    }
}