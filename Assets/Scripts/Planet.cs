using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    Vector2Int pos;
    public byte planet_num;
    string planet_name;

    public Vector2Int PosOnMap
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;

            if (planet_num == 1) 
                UIController.Instance.MapGrid.SetPlanet1Mark(pos);
            else
                UIController.Instance.MapGrid.SetPlanet2Mark(pos);

        }
    }
    public void GeneratePlanet(Vector2Int pos, byte num, string name)
    {
        planet_num = num;
        planet_name = name;
        PosOnMap = pos;
    }
}
