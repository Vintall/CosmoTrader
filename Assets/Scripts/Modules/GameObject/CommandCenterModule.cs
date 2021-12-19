using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenterModule : BaseModule
{
    List<BodyModule> body = new List<BodyModule>();

    
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

    public ScriptableCommandCenter GetDataLevel(int level)
    {
        return (ScriptableCommandCenter)data.ExactLevel(level);
    }
    public ScriptableCommandCenter GetDataLevel()
    {
        return (ScriptableCommandCenter)data.ExactLevel(CurrentLevel);
    }
    public void SetAllChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            body.Add((BodyModule)transform.GetChild(i).GetComponent<GameModule>().module);

            body[body.Count - 1].SetAllChilds();
        }
    }
    public override void GetStates(ref Ship ship)
    {
        ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
    }
}
