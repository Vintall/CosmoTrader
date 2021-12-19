using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffGridCollidersWhenHover : MonoBehaviour
{
    public void OnMouseEnter()
    {
        UIController.Instance.MapGrid.OffColliders();
        Debug.Log("Enter");
    }
    public void OnMouseExit()
    {
        UIController.Instance.MapGrid.OnColliders();
        Debug.Log("Leave");
    }
}
