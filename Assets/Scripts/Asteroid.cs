using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector2Int pos;
    public int ore_have;
    string asteroid_name;

    public string AsteroidName
    {
        get
        {
            return asteroid_name;
        }
        set
        {
            asteroid_name = value;
        }
    }

    public Vector2Int Pos
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;
            UIController.Instance.MapGrid.SetAsteroidMark(pos);
        }
    }
    public int OreHave
    {
        get
        {
            return ore_have;
        }
        set
        {
            ore_have = value;
            if (ore_have <= 0)
            {

                
            }
        }
    }
}
