using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Convertor/Level")]
public class ScriptableConvertor : ScriptableBaseModule
{
    [SerializeField] int ore_count_to_one_MW;

    public int OreCountToOneMW
    {
        get
        {
            return ore_count_to_one_MW;
        }
    }
}
