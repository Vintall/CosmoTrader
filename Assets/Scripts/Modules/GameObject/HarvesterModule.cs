using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesterModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableHarvester> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableHarvester> buffer = new ScriptableModuleLevelsContainer<ScriptableHarvester>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableHarvester)i);

            return buffer;
        }
    }
    public ScriptableHarvester GetDataLevel(int level)
    {
        return (ScriptableHarvester)data.ExactLevel(level);
    }
    public ScriptableHarvester GetDataLevel()
    {
        return (ScriptableHarvester)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.harvesters_consume_per_mine += GetDataLevel().ConsumeMWPerCollecting;
        ship.harvesters_takes_ore_per_trip += GetDataLevel().CollectOrePerTrip;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
