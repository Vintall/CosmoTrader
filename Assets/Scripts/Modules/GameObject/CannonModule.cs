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
}
