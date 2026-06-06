using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    // ENERGY
    public int energy;
    public int exploreEnergy;
    public string energyTimestamp;
    public string exploreTimestamp;
    // MAP
    public string currentScene;
    public float mapSpawnX;
    public float mapSpawnY;
    public float mapSpawnZ;
    // BOARD
    public List<ItemSaveData> boardItems = new List<ItemSaveData>();
    // NODE
    public List<NodeSaveData> nodes = new List<NodeSaveData>();
    // ORDER
    public List<int> activeOrders = new List<int>();
    // TUTORIAL
    public int tutorialStep;
}