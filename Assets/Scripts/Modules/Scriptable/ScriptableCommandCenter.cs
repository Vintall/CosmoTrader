using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/ScriptableModuls/CommandCenter/Level")]
public class ScriptableCommandCenter : ScriptableBaseModule
{
    [SerializeField] int bodies_count;

    public int BodiesCount
    {
        get
        {
            return bodies_count;
        }
    }
}
