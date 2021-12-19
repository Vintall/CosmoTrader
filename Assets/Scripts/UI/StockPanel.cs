using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPanel : MonoBehaviour
{
    [SerializeField] List<TradeCellPanel> panels;

    public List<TradeCellPanel> Panels
    {
        get 
        { 
            return panels; 
        }
    }
}
