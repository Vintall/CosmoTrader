using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Storage/Level")]
public class ScriptableStorage : ScriptableBaseModule
{
    [SerializeField] int ore_capacity;

    public int OreCapacity
    {
        get
        {
            return ore_capacity;
        }
    }
}
