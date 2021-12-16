using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Cannon/Level")]
public class ScriptableCannon : ScriptableBaseModule
{
    [SerializeField] int damage_per_shot;
    [SerializeField] int consume_mw_per_shot;

    public int DamagePerShot
    {
        get
        {
            return damage_per_shot;
        }
    }
    public int ConsumeMWPerShot
    {
        get
        {
            return consume_mw_per_shot;
        }
    }
}
