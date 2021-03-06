using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModule : MonoBehaviour
{
    [SerializeField] protected ScriptableModuleLevelsContainer<ScriptableBaseModule> data;
    protected int current_level;
    List<Structs.JointPoint> joint_points;

    public void UpdateModule()
    {
        if(current_level < data.LevelsCount)
        {
            current_level++;
            Debug.Log("LvlUP " + current_level);
        }
    }
    public int CurrentLevel
    {
        get
        {
            return current_level;
        }
    }
    public ScriptableModuleLevelsContainer<ScriptableBaseModule> Data
    {
        get
        {
            return data;
        }
    }
    public virtual void GenerateObject(ScriptableModuleLevelsContainer<ScriptableBaseModule> data, int level)
    {
        this.data = data;
        this.current_level = level;
    }
    public virtual void GetStates(ref Ship ship)
    {
        ship.max_durability += data.ExactLevel(CurrentLevel).Durability;
    }
    public List<BaseModule> GetAllChilds()
    {
        List<BaseModule> baseModules = new List<BaseModule>();
        for (int i = 0; i < transform.childCount; i++)
            baseModules.Add(transform.GetChild(i).GetComponent<BaseModule>());

        return baseModules;
    }
    
}