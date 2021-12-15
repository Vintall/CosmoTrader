using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] BuyEnergyPanel buy_energy_panel;
    [SerializeField] SellOrePanel sell_ore_panel;

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
