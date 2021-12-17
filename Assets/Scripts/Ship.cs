using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    CommandCenterModule command_center;

    public CommandCenterModule CommandCenter
    {
        get
        {
            return command_center;
        }
        set
        {
            command_center = value;
        }
    }
    public void Attack()
    {

    }
    public void MoveTo()
    {

    }
}
