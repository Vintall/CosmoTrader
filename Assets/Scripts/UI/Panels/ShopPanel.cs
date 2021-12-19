using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] BuyEnergyPanel buy_energy_panel;
    [SerializeField] SellOrePanel sell_ore_panel;

    public void SetShopPanel(bool is_savezone)
    {
        SetBuyEnergyPanel(is_savezone);
    }
    public void SetBuyEnergyPanel(bool is_savezone)
    {
        sell_ore_panel.gameObject.SetActive(false);
        buy_energy_panel.OpenPanel(is_savezone);
    }
    public void SetSellOrePanel()
    {
        buy_energy_panel.gameObject.SetActive(false);
        sell_ore_panel.OpenPanel();
    }
    public SellOrePanel SellOrePanel
    {
        get
        {
            return sell_ore_panel;
        }
    }
    public BuyEnergyPanel BuyEnergyPanel
    {
        get
        {
            return buy_energy_panel;
        }
    }
}
