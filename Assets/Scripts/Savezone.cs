using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savezone : MonoBehaviour
{
    Vector2Int pos;
    [SerializeField] ModularCenter modular_center;

    public ModularCenter ModularCenter
    {
        get
        {
            return modular_center;
        }
    }

    public Vector2Int PosOnMap
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;
            UIController.Instance.MapGrid.SetSavezoneMark(pos);
        }
    }
}
