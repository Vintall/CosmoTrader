using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Engine/Level")]
public class ScriptableEngine : ScriptableBaseModule
{
    [SerializeField] int mw_consume_per_hundred_km;
    [SerializeField] int mw_consume_per_battle;


    public int MWConsumePerBattle
    {
        get
        {
            return mw_consume_per_battle;
        }
    }
    public int MWConsumePerHundredKm
    {
        get
        {
            return mw_consume_per_hundred_km;
        }
    }
}
