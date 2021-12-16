using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structs
{
    [System.Serializable]
    public enum ModuleType
    {
        CommandCenter = 1,
        Body = 2,
        Engine = 3,
        Battery = 4,
        Storage = 5,
        Cannon = 6,
        Harvester = 7,
        Convertor = 8,
        Generator = 9,
        Repairer = 10
    }
    [System.Serializable]
    public struct JointPoint
    {
        public Vector2 pos;
        [Range(0f,360f)]
        public float angle;
    }
}
