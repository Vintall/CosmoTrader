using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairerModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableRepairer> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableRepairer> buffer = new ScriptableModuleLevelsContainer<ScriptableRepairer>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableRepairer)i);

            return buffer;
        }
    }
    public ScriptableRepairer GetDataLevel(int level)
    {
        return (ScriptableRepairer)data.ExactLevel(level);
    }
    public ScriptableRepairer GetDataLevel()
    {
        return (ScriptableRepairer)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.repairers_deals_durability_per_10_min += GetDataLevel().RepairDurabilityPerUse;
        ship.repairers_consume_mw += 10;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
