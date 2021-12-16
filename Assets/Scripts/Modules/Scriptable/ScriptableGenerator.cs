using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Generator/Level")]
public class ScriptableGenerator : ScriptableBaseModule
{
    [SerializeField] int give_mw_per_hundred_km;

    public int GiveMWPerHundredKm
    {
        get
        {
            return give_mw_per_hundred_km;
        }
    }
}
