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
}
