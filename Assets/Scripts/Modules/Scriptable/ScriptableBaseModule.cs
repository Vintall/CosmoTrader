using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableBaseModule : ScriptableObject
{
    [SerializeField] int durability;
    [SerializeField] float cost;
    [SerializeField, Multiline] string description;
    [SerializeField] Sprite sprite;
    [SerializeField] Structs.ModuleType type;
    [SerializeField] List<Structs.ModuleType> building_blocked_objects;
    [SerializeField] Structs.JointPoint main_joint_point;
    [SerializeField] List<Structs.JointPoint> joint_points;

    public string Description
    {
        get
        {
            return description;
        }
    }
    public Structs.JointPoint MainJointPoint
    {
        get
        {
            return main_joint_point;
        }
    }
    public List<Structs.JointPoint> JointPoints
    {
        get
        {
            return joint_points;
        }
    }
    public List<Structs.ModuleType> BuildingBlockedObjects
    {
        get
        {
            return building_blocked_objects;
        }
    }
    public Structs.ModuleType Type
    {
        get
        {
            return type;
        }
    }
    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }
    public int Durability
    {
        get
        {
            return durability;
        }
    }
    public float Cost
    {
        get
        {
            return cost;
        }
    }
}
