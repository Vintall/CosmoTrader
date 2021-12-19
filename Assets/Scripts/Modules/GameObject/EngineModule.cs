using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableEngine> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableEngine> buffer = new ScriptableModuleLevelsContainer<ScriptableEngine>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableEngine)i);

            return buffer;
        }
    }
    public ScriptableEngine GetDataLevel(int level)
    {
        return (ScriptableEngine)data.ExactLevel(level);
    }
    public ScriptableEngine GetDataLevel()
    {
        return (ScriptableEngine)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.engines_consume_per_100_km += GetDataLevel().MWConsumePerHundredKm;
        ship.engines_consume_per_battle += GetDataLevel().MWConsumePerBattle;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
