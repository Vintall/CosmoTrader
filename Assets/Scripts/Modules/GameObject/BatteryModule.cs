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
    public override void GetStates(ref Ship ship)
    {
        ScriptableBattery current_level_data = GetData.ExactLevel(current_level);
        int capacity = current_level_data.MWCapacity;
    }
}
