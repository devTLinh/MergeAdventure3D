using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

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
    public void SaveGame()
    {
        GameSaveData data = new GameSaveData();
        SavePlayer(data);
        SaveBoard(data);
        SaveOrders(data);
        //SaveNodes(data);
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
        //LoadNodes(data);
        LoadOrders(data);
        SceneLoader.Instance.RestoreMapState(data);
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
        data.currentScene = SceneLoader.Instance.currentMap;
        Vector3 pos = SceneLoader.Instance.currentMapSpawn;
        data.mapSpawnX = pos.x;
        data.mapSpawnY = pos.y;
        data.mapSpawnZ = pos.z;
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
    void SaveNodes(GameSaveData data)
    {
        data.nodes.Clear();
        ExplorationNode[] nodes = FindObjectsOfType<ExplorationNode>();
        foreach (ExplorationNode node in nodes)
        {
            NodeSaveData save = new NodeSaveData();
            save.nodeID = node.nodeId;
            save.unlocked = node.unlocked;
            data.nodes.Add(save);
        }
    }
    void LoadNodes(GameSaveData data)
    {
        ExplorationNode[] nodes = FindObjectsOfType<ExplorationNode>();
        foreach (ExplorationNode node in nodes){
            foreach (NodeSaveData save in data.nodes)
            {
                if (save.nodeID != node.nodeId) continue;
                if (save.unlocked)
                {
                    node.Unlock();
                }
            }
        }
    }
}