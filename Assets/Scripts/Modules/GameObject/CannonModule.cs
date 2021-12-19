using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableCannon> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableCannon> buffer = new ScriptableModuleLevelsContainer<ScriptableCannon>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableCannon)i);

            return buffer;
        }
    }
    public ScriptableCannon GetDataLevel(int level)
    {
        return (ScriptableCannon)data.ExactLevel(level);
    }
    public ScriptableCannon GetDataLevel()
    {
        return (ScriptableCannon)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.cannons_consume_per_shot += GetDataLevel().ConsumeMWPerShot;
        ship.cannons_deals_damage += GetDataLevel().DamagePerShot;
        //ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
        base.GetStates(ref ship);
    }
}
