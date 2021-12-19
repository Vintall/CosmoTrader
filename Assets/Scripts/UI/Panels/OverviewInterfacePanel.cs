using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewInterfacePanel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] TMPro.TextMeshProUGUI balance_text_field;
    [SerializeField] TMPro.TextMeshProUGUI ore_text_field;
    [SerializeField] TMPro.TextMeshProUGUI energy_text_field;
    [SerializeField] TMPro.TextMeshProUGUI durability_text_field;
    [SerializeField] RectTransform bar_background;
    [SerializeField] Transform mine_ore_button;
    [SerializeField] OverviewTradePanel trade_panel;

    [SerializeField] RectTransform ore_movable_bar;
    [SerializeField] RectTransform energy_movable_bar;
    [SerializeField] RectTransform durability_movable_bar;
    #endregion

    public OverviewTradePanel TradePanel
    {
        get
        {
            return trade_panel;
        }
    }
    public void PlayerEnvironmentClear()
    {
        HideMineOreButton();
        trade_panel.HideModularCenterButton();
        trade_panel.HideSavezoneIcon();
        //trade_panel.HideShopButton();
        
    }
    public void ShowMineOreButton()
    {
        mine_ore_button.gameObject.SetActive(true);
    }
    public void HideMineOreButton()
    {
        mine_ore_button.gameObject.SetActive(false);
    }
    public void MineOreButtonCallback()
    {
        GameController.Instance.Map.Player.MineOre();
    }
    public void ChangeBalanceInfo(float balance)
    {
        balance_text_field.text = balance.ToString();
    }
    public void ChangeOreInfo(int ore_have, int ore_max)
    {
        ore_movable_bar.offsetMax = new Vector2(bar_background.rect.width * ((float)ore_have / ore_max) - bar_background.rect.width, 0);
        ore_text_field.text = ore_have.ToString() + " / " + ore_max.ToString();
    }
    public void ChangeDurabilityInfo(int durability_have, int durability_max)
    {
        durability_movable_bar.offsetMax = new Vector2(bar_background.rect.width * ((float)durability_have / durability_max) - bar_background.rect.width, 0);
        durability_text_field.text = durability_have.ToString() + " / " + durability_max.ToString();
    }
    public void ChangeEnergyInfo(int energy_have, int energy_max)
    {
        energy_movable_bar.offsetMax = new Vector2(bar_background.rect.width * ((float)energy_have / energy_max) - bar_background.rect.width, 0);
        energy_text_field.text = energy_have.ToString() + "МВт" + " / " + energy_max.ToString() + "МВт";
    }
    public void ChangeStates(float balance, 
                             int energy_have, int energy_max, 
                             int durability_have, int durability_max,
                             int ore_have, int ore_max)
    {
        ChangeBalanceInfo(balance);
        ChangeOreInfo(ore_have, ore_max);
        ChangeDurabilityInfo(durability_have, durability_max);
        ChangeEnergyInfo(energy_have, energy_max);
    }
}
