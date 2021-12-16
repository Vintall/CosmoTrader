using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenterModule : BaseModule
{
    List<BodyModule> body;
    public ScriptableModuleLevelsContainer<ScriptableCommandCenter> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableCommandCenter> buffer = new ScriptableModuleLevelsContainer<ScriptableCommandCenter>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableCommandCenter)i);

            return buffer;
        }
    }
}
