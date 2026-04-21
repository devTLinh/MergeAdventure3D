using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Merge Frontier/Generator")]
public class GeneratorData : ScriptableObject {
    public string generatorId;
    public string displayName; 
    public int energyCost = 1; 
    public int maxCharges = 10;
    public float cooldown = 3f;
    public List<GeneratorDropEntry> drops = new List<GeneratorDropEntry>();
}