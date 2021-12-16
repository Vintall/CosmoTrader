using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savezone : MonoBehaviour
{
    Vector2 point_on_map;
    [SerializeField] ModularCenter modular_center;

    public ModularCenter ModularCenter
    {
        get
        {
            return modular_center;
        }
    }

    public Vector2 PointOnMap
    {
        get
        {
            return point_on_map;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
