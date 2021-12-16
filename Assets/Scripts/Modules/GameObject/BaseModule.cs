using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModule : MonoBehaviour
{
    [SerializeField] protected ScriptableModuleLevelsContainer<ScriptableBaseModule> data;
    protected int current_level;
    List<Structs.JointPoint> joint_points;

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
    { }

}