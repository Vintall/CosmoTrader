using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Repairer/Level")]
public class ScriptableRepairer : ScriptableBaseModule
{
    [SerializeField] int repair_durability_per_use;
    [SerializeField] int time_in_minutes;
    [SerializeField] int mw_consume;


    public int MWConsume
    {
        get
        {
            return mw_consume;
        }
    }
    public int RepairDurabilityPerUse
    {
        get
        {
            return repair_durability_per_use;
        }
    }
    public int TimeInMinutes
    {
        get
        {
            return time_in_minutes;
        }
    }
}
