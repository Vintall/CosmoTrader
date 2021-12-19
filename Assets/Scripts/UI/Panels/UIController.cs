using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] ShopPanel shop_panel;
    [SerializeField] ModularCenterPanel modular_center_panel;
    [SerializeField] OverviewInterfacePanel overview_panel;
    [SerializeField] MapGrid map_grid;
    [SerializeField] ConvertorPanel convertor_panel;
    [SerializeField] GameOverPanel game_over_panel;
    static UIController instance;

    #region  Properties

    public GameOverPanel GameOverPanel
    {
        get
        {
            return game_over_panel;
        }
    }

    public void GameOverUI()
    {
        shop_panel.gameObject.SetActive(false);
        modular_center_panel.gameObject.SetActive(false);
        overview_panel.gameObject.SetActive(false);
        map_grid.gameObject.SetActive(false);
        convertor_panel.gameObject.SetActive(false);
        game_over_panel.gameObject.SetActive(true);
    }
    public MapGrid MapGrid
    {
        get
        {
            return map_grid;
        }
    }
    public ConvertorPanel ConvertorPanel
    {
        get
        {
            return convertor_panel;
        }
    }
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

    public void SetOverviewPanel()
    {
        map_grid.gameObject.SetActive(true);
        overview_panel.gameObject.SetActive(true);
        modular_center_panel.gameObject.SetActive(false);
    }
    public void SetModularCenterPanel()
    {
        modular_center_panel.gameObject.SetActive(true);
        overview_panel.gameObject.SetActive(false);
        map_grid.gameObject.SetActive(false);
    }
    public void SetShopPanel()
    {
        shop_panel.gameObject.SetActive(true);
        overview_panel.gameObject.SetActive(false);
        map_grid.gameObject.SetActive(false);
    }
    public void SetConvertorPanel()
    {
        convertor_panel.OpenPanel(GameController.Instance.Map.Player.Ship.convertors_best_ore_count_per_MW);
    }
    private void Awake()
    {
        instance = this;
    }
}
