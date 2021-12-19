using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyEnergyPanel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] TMPro.TextMeshProUGUI max_power_capacity;
    [SerializeField] TMPro.TextMeshProUGUI power_have;
    [SerializeField] TMPro.TextMeshProUGUI money_have;
    [SerializeField] RectTransform bar;
    [SerializeField] RectTransform movable_bar;
    [SerializeField] StockPanel stock;
    [SerializeField] Transform sell_ore_button;
    #endregion

    #region Properties
    public Transform SellOreButton
    {
        get
        {
            return sell_ore_button;
        }
    }
    public int PowerMaxCapacity
    {
        set
        {
            max_power_capacity.text = value.ToString() + " МВт";
        }
    }
    public int PowerHave
    {
        set
        {
            power_have.text = value.ToString() + " МВт";
        }
    }
    public float MoneyHave
    {
        set
        {
            money_have.text = value.ToString();
        }
    }
    #endregion

    public void SellOreButtonCallback() //Swap to ore-sell panel
    {
        if (is_savezone)
            transform.parent.GetComponent<ShopPanel>().SetSellOrePanel();
    }
    public void ChangeStates(int power_have, int max_capacity, float money)
    {
        PowerMaxCapacity = max_capacity;
        PowerHave = power_have;
        MoneyHave = money;
        movable_bar.offsetMax = new Vector2(bar.rect.width * ((float)power_have / max_capacity) - bar.rect.width, 0);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameController.Instance.Map.Player.GoToOverview();
    }
    bool is_savezone;
    public void OpenPanel(bool is_savezone)
    {
        stock.Panels[0].gameObject.SetActive(is_savezone);
        stock.Panels[1].gameObject.SetActive(is_savezone);
        stock.Panels[2].gameObject.SetActive(!is_savezone);
        stock.Panels[3].gameObject.SetActive(is_savezone);
        stock.Panels[4].gameObject.SetActive(!is_savezone);
        stock.Panels[5].gameObject.SetActive(is_savezone);

        int mw_have = GameController.Instance.Map.Player.Ship.MW_capacity - GameController.Instance.Map.Player.Ship.MWHave;
        for (int i = 0; i < stock.Panels.Count; i++)
        {
            stock.Panels[i].gameObject.SetActive(true);

            
            if (!(mw_have >= stock.Panels[i].QuantityLower && mw_have < stock.Panels[i].QuantityHigher))
                stock.Panels[i].gameObject.SetActive(false);
        }

        this.is_savezone = is_savezone;
        gameObject.SetActive(true);
    }
    public void BuyButtonCallback(string button_id)
    {
        int id = int.Parse(button_id);
        float cost = stock.Panels[id].Cost;
        int count = stock.Panels[id].Count;

        GameController.Instance.Map.Player.BuyEnergy(cost, count);
    }
}
