using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeCellPanel : MonoBehaviour
{
    [SerializeField] float cost;
    [SerializeField] int count;
    [SerializeField] int quantity_lower;
    [SerializeField] int quantity_higher;

    public float Cost
    {
        get
        {
            return cost;
        }
    }
    public int Count
    {
        get
        {
            return count;
        }
    }
    public int QuantityLower
    {
        get
        {
            return quantity_lower;
        }
    }
    public int QuantityHigher
    {
        get
        {
            return quantity_higher;
        }
    }
}
