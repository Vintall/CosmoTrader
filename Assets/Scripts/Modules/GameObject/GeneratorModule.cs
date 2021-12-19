using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableGenerator> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableGenerator> buffer = new ScriptableModuleLevelsContainer<ScriptableGenerator>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableGenerator)i);

            return buffer;
        }
    }
    public ScriptableGenerator GetDataLevel(int level)
    {
        return (ScriptableGenerator)data.ExactLevel(level);
    }
    public ScriptableGenerator GetDataLevel()
    {
        return (ScriptableGenerator)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.generators_generate_per_100_km += GetDataLevel().GiveMWPerHundredKm;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
