using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableStorage> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableStorage> buffer = new ScriptableModuleLevelsContainer<ScriptableStorage>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableStorage)i);

            return buffer;
        }
    }
    public ScriptableStorage GetDataLevel(int level)
    {
        return (ScriptableStorage)data.ExactLevel(level);
    }
    public ScriptableStorage GetDataLevel()
    {
        return (ScriptableStorage)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.ore_capacity += GetDataLevel().OreCapacity;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
