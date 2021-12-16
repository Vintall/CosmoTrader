using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] ShopPanel shop_panel;
    [SerializeField] ModularCenterPanel modular_center_panel;
    [SerializeField] OverviewInterfacePanel overview_panel;
    static UIController instance;

    #region  Properties

    public OverviewInterfacePanel OverviewPanel
    {
        get
        {
            return overview_panel;
        }
    }
    public ModularCenterPanel ModularCenterPanel
    {
        get
        {
            return modular_center_panel;
        }
    }
    public ShopPanel ShopPanel
    {
        get
        {
            return shop_panel;
        }
    }
    public static UIController Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        overview_panel.ChangeStates(240, 3400, 5345, 53, 123, 3000, 3453);
    }

}
