using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageModule : BaseModule
{
    public ScriptableModuleLevelsContainer<ScriptableStorage> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableStorage> buffer = new ScriptableModuleLevelsContainer<ScriptableStorage>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableStorage)i);

            return buffer;
        }
    }
}
