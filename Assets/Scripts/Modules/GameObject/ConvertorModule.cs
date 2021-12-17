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
}
