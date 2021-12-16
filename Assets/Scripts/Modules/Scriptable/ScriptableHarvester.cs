using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Harvester/Level")]
public class ScriptableHarvester : ScriptableBaseModule
{
    [SerializeField] int consume_mw_per_collecting;
    [SerializeField] int collect_ore_per_trip;

    public int ConsumeMWPerCollecting
    {
        get
        {
            return consume_mw_per_collecting;
        }
    }
    public int CollectOrePerTrip
    {
        get
        {
            return collect_ore_per_trip;
        }
    }
}
