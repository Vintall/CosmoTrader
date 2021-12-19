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
    [SerializeField] TMPro.TextMeshProUGUI loger;
    [SerializeField] RectTransform bar_background;
    [SerializeField] Transform mine_ore_button;
    [SerializeField] OverviewTradePanel trade_panel;

    [SerializeField] RectTransform ore_movable_bar;
    [SerializeField] RectTransform energy_movable_bar;
    [SerializeField] RectTransform durability_movable_bar;
    #endregion

    public void AddLog(string message)
    {
        loger.text = message;
        StartCoroutine(LogClear());
    }
    public void AddLog(string message, float seconds)
    {
        loger.text = message;
        StartCoroutine(LogClear(seconds));
    }
    IEnumerator LogClear()
    {
        yield return new WaitForSeconds(1);
        loger.text = "";
    }
    IEnumerator LogClear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        loger.text = "";
    }
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
        //ore_movable_bar.offsetMax.Set(bar_background.rect.width * ((float)ore_have / ore_max) - bar_background.rect.width, 0);
        if (ore_max != 0) 
        ore_movable_bar.localScale = new Vector3((float)ore_have / ore_max, 1, 1);
        ore_text_field.text = ore_have.ToString() + " / " + ore_max.ToString();
    }
    public void ChangeDurabilityInfo(int durability_have, int durability_max)
    {
        //durability_movable_bar.offsetMax.Set(bar_background.rect.width * ((float)durability_have / durability_max) - bar_background.rect.width, 0);
        if (durability_max != 0)
            durability_movable_bar.localScale = new Vector3((float)durability_have / durability_max, 1,1);
        durability_text_field.text = durability_have.ToString() + " / " + durability_max.ToString();
    }
    public void ChangeEnergyInfo(int energy_have, int energy_max)
    {
        //energy_movable_bar.offsetMax.Set(bar_background.rect.width * ((float)energy_have / (float)energy_max) - bar_background.rect.width, 0);
        //energy_movable_bar.offsetMax.Set(bar_background.rect.width * ((float)energy_have / (float)energy_max) - bar_background.rect.width, 0);
        if (energy_max != 0)
            energy_movable_bar.localScale = new Vector3((float)energy_have / (float)energy_max, 1, 1);
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
    public void EndGameButtonCallback()
    {
        GameController.Instance.GameOver("Пользователь завершил игру", false);
    }
}
