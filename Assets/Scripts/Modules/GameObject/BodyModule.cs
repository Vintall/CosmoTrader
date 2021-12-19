using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyModule : BaseModule
{
    List<BaseModule> modules = new List<BaseModule>();

    public ScriptableModuleLevelsContainer<ScriptableBody> GetData
    {
        get
        {
            ScriptableModuleLevelsContainer<ScriptableBody> buffer = new ScriptableModuleLevelsContainer<ScriptableBody>();
            foreach (ScriptableBaseModule i in data.AllLevels)
                buffer.AllLevels.Add((ScriptableBody)i);

            return buffer;
        }
    }
    public ScriptableBody GetDataLevel(int level)
    {
        return (ScriptableBody)data.ExactLevel(level);
    }
    public ScriptableBody GetDataLevel()
    {
        return (ScriptableBody)data.ExactLevel(CurrentLevel);
    }
    //public override void GetStates(ref Ship ship)
    // {
    //foreach (BaseModule module in modules)
    //    module.GetStates(ref ship);
    //}
    public void SetAllChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            modules.Add(transform.GetChild(i).GetComponent<GameModule>().module);
        }
    }
    public override void GetStates(ref Ship ship)
    {
        ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
    }
}
