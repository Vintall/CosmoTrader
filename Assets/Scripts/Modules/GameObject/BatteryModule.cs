using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableBattery> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableBattery> buffer = new ScriptableModuleLevelsContainer<ScriptableBattery>();
            foreach(ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableBattery)i);
            
            return buffer;
        }
    }
    public ScriptableBattery GetDataLevel(int level)
    {
        return (ScriptableBattery)data.ExactLevel(level);
    }
    public ScriptableBattery GetDataLevel()
    {
        return (ScriptableBattery)data.ExactLevel(CurrentLevel);
    }
    public override void GetStates(ref Ship ship)
    {
        ship.MW_capacity += GetDataLevel().MWCapacity;
        base.GetStates(ref ship);
    }
}
