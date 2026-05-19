using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    string savePath;

    private void Awake()
    {
        Instance = this;

        savePath =
            Application.persistentDataPath
            + "/save.json";
    }
    public void InitializeGame()
    {
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
        //LoadNodes(data);
        Debug.Log("GAME LOADED");
    }
    void SavePlayer(GameSaveData data)
    {
        data.energy = EnergyManager.Instance.CurrentEnergy;
        data.exploreEnergy = ExplorationEnergyManager.Instance.CurrentEnergy;
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

        foreach (ExplorationNode node in nodes)
        {
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