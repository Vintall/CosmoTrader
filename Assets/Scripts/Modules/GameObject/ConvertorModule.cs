using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertorModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableConvertor> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableConvertor> buffer = new ScriptableModuleLevelsContainer<ScriptableConvertor>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableConvertor)i);

            return buffer;
        }
    }
    public ScriptableConvertor GetDataLevel(int level)
    {
        return (ScriptableConvertor)data.ExactLevel(level);
    }
    public ScriptableConvertor GetDataLevel()
    {
        return (ScriptableConvertor)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        if (GetDataLevel().OreCountToOneMW < ship.convertors_best_ore_count_per_MW)
            ship.convertors_best_ore_count_per_MW = GetDataLevel().OreCountToOneMW;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
