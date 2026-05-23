using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public int energy;
    public int exploreEnergy;
    public string energyTimestamp;
    public string exploreTimestamp;
    public List<ItemSaveData> boardItems = new List<ItemSaveData>();
    public List<NodeSaveData> nodes = new List<NodeSaveData>();
}