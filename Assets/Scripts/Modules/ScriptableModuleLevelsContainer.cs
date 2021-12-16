using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableModuleLevelsContainer<T> : ScriptableObject
{
    [SerializeField] List<T> levels = new List<T>();
    [SerializeField] string id;
    

    public string ID
    {
        get
        {
            return id;
        }
    }
    public List<T> AllLevels
    {
        get
        {
            return levels;
        }
    }
    
    public int LevelsCount
    {
        get
        {
            return levels.Count;
        }
    }

    /// <summary>
    /// level_num в рамках [1; inf) 
    /// </summary>
    /// <param name="level_num"></param>
    /// <returns></returns>
    public T ExactLevel(int level_num)
    {
        return levels[level_num - 1];
    }
    public void SetContainer(string id, List<T> levels)
    {
        this.id = id;
        this.levels = levels;
    }
}
