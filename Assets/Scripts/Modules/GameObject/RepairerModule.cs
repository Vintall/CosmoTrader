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
}
