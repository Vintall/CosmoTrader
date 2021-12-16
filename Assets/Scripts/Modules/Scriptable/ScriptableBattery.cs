using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/Battery/Level")]
public class ScriptableBattery : ScriptableBaseModule
{
    [SerializeField] int mw_capacity;

    public int MWCapacity
    {
        get
        {
            return mw_capacity;
        }
    }
}
