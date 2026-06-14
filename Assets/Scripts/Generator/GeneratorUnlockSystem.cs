using System.Collections.Generic;
using UnityEngine;

public class GeneratorUnlockSystem :
MonoBehaviour
{
    public static GeneratorUnlockSystem Instance;

    [SerializeField] List<GameObject> generators = new();

    public int unlockedCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        RefreshGenerators();
    }

    public void UnlockNext()
    {
        if (unlockedCount >=
            generators.Count)
            return;
        unlockedCount++;
        Debug.Log(
            "Generator Unlocked");
    }

    public void RefreshGenerators()
    {
        for (int i = 0; i < generators.Count; i++){
            generators[i].SetActive( i < unlockedCount);
        }
    }
    public void Restore(int count)
    {
        if(count == 0)
        {
            Debug.Log( "Invalid generator count in save data");
            unlockedCount = 1;
        }
        else unlockedCount = count;
        RefreshGenerators();
    }
}