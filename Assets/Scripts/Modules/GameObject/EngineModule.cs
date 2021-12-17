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
}
