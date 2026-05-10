using System;
using System.Collections.Generic;

[Serializable]
public class GameSaveData
{
    public int energy;

    public int exploreEnergy;

    public List<ItemSaveData> boardItems = new List<ItemSaveData>();

    public List<NodeSaveData> nodes = new List<NodeSaveData>();
}