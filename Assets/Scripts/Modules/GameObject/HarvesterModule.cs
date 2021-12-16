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
}
