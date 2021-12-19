using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellOrePanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI max_ore_capacity;
    [SerializeField] TMPro.TextMeshProUGUI ore_have;
    [SerializeField] TMPro.TextMeshProUGUI money_have;
    [SerializeField] RectTransform bar;
    [SerializeField] RectTransform movable_bar;
    [SerializeField] StockPanel stock;
    [SerializeField] Transform buy_energy_button;


    public void BuyEnergyButtonCallback() //Swap to buy-energy panel
    {
        transform.parent.GetComponent<ShopPanel>().SetBuyEnergyPanel(true);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameController.Instance.Map.Player.GoToOverview();
    }
    public void OpenPanel()
    {
        int ore_have = GameController.Instance.Map.Player.Ship.OreHave;

        for (int i = 0; i < stock.Panels.Count; i++)
        {
            stock.Panels[i].gameObject.SetActive(false);

            if (ore_have >= stock.Panels[i].QuantityLower && ore_have < stock.Panels[i].QuantityHigher)
                stock.Panels[i].gameObject.SetActive(true);
        }
        gameObject.SetActive(true);
    }
    public void SellButtonCallback(string button_id)
    {
        int id = int.Parse(button_id);
        float cost = stock.Panels[id].Cost;
        int count = stock.Panels[id].Count;

        GameController.Instance.Map.Player.SellOre(cost, count);
    }
}
