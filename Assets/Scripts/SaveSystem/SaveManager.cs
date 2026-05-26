using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public GameSaveData currentData;
    Dictionary<string, bool> nodeLookup = new Dictionary<string, bool>();
    string savePath;
    bool initialized;

    private void Awake()
    {
        Instance = this;

        savePath =
            Application.persistentDataPath
            + "/save.json";
    }
    private void Start()
    {
        InitializeGame();
    }
    public void InitializeGame()
    {
        if (initialized) return;
        initialized = true;
        LoadGame();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }
    void BuildNodeLookup()
    {
        nodeLookup.Clear();
        if (currentData == null || currentData.nodes == null) return;
        foreach(NodeSaveData node in currentData.nodes) {
            nodeLookup[node.nodeId] = node.unlocked;
        }
    }
    public void SaveGame()
    {
        GameSaveData data = new GameSaveData();
        SavePlayer(data);
        SaveBoard(data);
        SaveOrders(data);
        SaveStateScene(data);
        if(currentData != null) data.nodes = currentData.nodes;
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("GAME SAVED");
        Debug.Log("Link:" + savePath);
    }
    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("NO SAVE FILE");

            return;
        }
        string json = File.ReadAllText(savePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);
        Debug.Log("Link:" + savePath);
        LoadPlayer(data);
        LoadBoard(data);
        EnergyRegenManager.Instance.ApplyOfflineRegen(data);
        LoadSceneState(data);
        LoadOrders(data);
        currentData = data;
        BuildNodeLookup();
        Debug.Log("GAME LOADED");
    }
    void SavePlayer(GameSaveData data)
    {
        data.energy = EnergyManager.Instance.current;
        data.exploreEnergy = ExplorationEnergyManager.Instance.CurrentEnergy;
        data.energyTimestamp = System.DateTime.UtcNow.ToString("o");
        data.exploreTimestamp = System.DateTime.UtcNow.ToString("o");
    }
    void LoadPlayer(GameSaveData data)
    {
        EnergyManager.Instance.Set(data.energy);
        ExplorationEnergyManager.Instance.Set(data.exploreEnergy);
    }
    void SaveBoard(GameSaveData data)
    {
        data.boardItems.Clear();
        for (int i = 0; i < BoardManager.Instance.slots.Length; i++)
        {
            BoardSlot slot = BoardManager.Instance.slots[i];
            if (slot.currentItem == null) continue;
            ItemSaveData save = new ItemSaveData();
            save.mergeGroup = slot.currentItem.Model.Data.mergeGroup;
            save.level = slot.currentItem.Model.Data.level;
            save.slotIndex = i;
            data.boardItems.Add(save);
        }
    }
    void LoadBoard(GameSaveData data)
    {
        BoardManager.Instance.ClearBoard();
        foreach (ItemSaveData save in data.boardItems)
        {
            BoardSlot slot = BoardManager.Instance.slots[save.slotIndex];
            ItemData itemData = ItemDatabase.Instance.Get(save.mergeGroup, save.level);
            if (itemData == null)
            {
                Debug.LogError("Missing ItemData");
                continue;
            }
            ItemFactory.Instance.SpawnToSlot( slot, itemData);
        }
    }
    void SaveOrders(GameSaveData data)
    {
        data.activeOrders.Clear();
        for (int i = 0; i < OrderManager.Instance.ActiveOrders.Count; i++)
        {
            RuntimeOrder order = OrderManager.Instance.ActiveOrders[i];
            if (order == null) continue;
            data.activeOrders.Add(order.orderIndex);
        }
    }
    void LoadOrders(GameSaveData data){
        OrderManager.Instance.ActiveOrders.Clear();
        if ( data.activeOrders == null || data.activeOrders.Count == 0){
            OrderManager.Instance.FillOrders();
            return;
        }
        foreach (int save  in data.activeOrders)
        {
            OrderData dataOrder = OrderManager.Instance.GetOrderData(save);
            RuntimeOrder order = new RuntimeOrder(dataOrder, save);
            OrderManager.Instance.ActiveOrders.Add(order);
        }
    }
    void SaveStateScene(GameSaveData data){
        data.currentScene = SceneLoader.Instance.currentMap;
        data.mapSpawnX = SceneLoader.Instance.currentMapSpawn.x;
        data.mapSpawnY = SceneLoader.Instance.currentMapSpawn.y;    
        data.mapSpawnZ = SceneLoader.Instance.currentMapSpawn.z;
    }
    void LoadSceneState(GameSaveData data)
    {
        if (string.IsNullOrEmpty(data.currentScene)){
            SceneLoader.Instance.currentMap = "ForestCamp";
            return;
        }
        SceneLoader.Instance.RestoreMapState(data);
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
        currentData.nodes.Clear();
        foreach (var kv in nodeLookup)
        {
            NodeSaveData save = new NodeSaveData();
            save.nodeId = kv.Key;
            save.unlocked = kv.Value;
            currentData.nodes.Add(save);
        }
        Debug.Log("Nodes Saved");
    }
    public void LoadNodesForScene(string sceneName){
        if (currentData == null || currentData.currentScene != sceneName) return;
        ExplorationNode[] nodes = FindObjectsOfType<ExplorationNode>();
        foreach (ExplorationNode node in nodes){
            if(!nodeLookup.ContainsKey(node.nodeId)) continue;
            else if(nodeLookup[node.nodeId]){
                    node.Unlock();
            }
        }
        Debug.Log("Node restore " + sceneName);
    }
}