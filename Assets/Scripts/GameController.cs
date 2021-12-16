using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Map map;
    private static GameController instance;

    public Map Map
    {
        get
        {
            return map;
        }
    }
    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    public void StartGame()
    {

    }
    private void Awake()
    {
        instance = this;
    }

}
