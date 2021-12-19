using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform ship;
    Vector2Int pos;

    public void GenerateShip(Transform ship)
    {
        if (this.ship != null)
            Destroy(this.ship.gameObject);

        this.ship = ship;
    }
    public Ship Ship
    {
        get
        {
            return ship.GetComponent<Ship>();
        }
    }
    public Vector2Int PosOnMap
    {
        get
        {
            return pos;
        }
        set
        {
            pos = value;
            UIController.Instance.MapGrid.SetPlayerMark(pos);
            CheckEnvironment();
            UpdateUIStates();
        }
    }
    public void MoveTo(Vector2Int pos)
    {
        Ship ship_component = ship.GetComponent<Ship>();
        float distance = Vector2.Distance(pos, this.pos) * 1000;
        int energy_takes = (int)(distance * ship_component.engines_consume_per_100_km / 100);
        ship_component.MWHave -= energy_takes;
        PosOnMap = pos;
        Debug.Log("Moved to (" + pos.x + "; " + pos.y + ")");
        UIController.Instance.MapGrid.SetPlayerMark(pos);
    }
    public void CheckEnvironment()
    {
        CloseUI();

        is_around_planet = false;
        is_around_asteroid = false;
        is_around_savezone = false;

        Map map = GameController.Instance.Map;

        if (pos == map.Planet1.PosOnMap || pos == map.Planet2.PosOnMap)
            AroundPlanet();

        if (pos == map.Savezone.PosOnMap)
            AroundSavezone();

    }
    public void UpdateUIStates()
    {
        Ship ship = this.ship.GetComponent<Ship>();
        UIController.Instance.OverviewPanel.ChangeStates(
            ship.money,
            ship.MWHave,
            ship.MW_capacity,
            ship.Durability,
            ship.max_durability,
            ship.ore_have,
            ship.ore_capacity);
    }
    bool is_around_planet = false;
    bool is_around_asteroid = false;
    bool is_around_savezone = true;
    public bool is_have_convertor = false;

    public void MineOre()
    {
        if (is_around_planet)
            Ship.MineOre(transform.parent.GetComponent<Map>().ObjectOnPos(pos).GetComponent<Planet>());

        //if (is_around_asteroid)
        //    Ship.MineOre(transform.parent.GetComponent<Map>().ObjectOnPos(pos).GetComponent<Planet>());
    }
    public void BuyEnergy(float cost, int count)
    {
        while (Ship.money >= cost && Ship.MWHave < Ship.MW_capacity)  //Нерационально. Тяжёлый код. Можно расчитать и сделать одной итерацией.
        {
            Ship.MWHave += count;
            Ship.money -= cost;
        }
    }
    public void SellOre(float cost, int count)
    {
        while (Ship.ore_have > 0)   //Нерационально. Тяжёлый код
        {
            Ship.money += cost;
            Ship.ore_have -= count;
        }
    }
    public void GoToModularCenter()
    {
        GameController.Instance.Map.Savezone.ModularCenter.StartEditing(this.transform, ship);
        UIController.Instance.SetModularCenterPanel();
    }
    public void GoToShop()
    {
        UIController.Instance.SetShopPanel();
        UIController.Instance.ShopPanel.SetShopPanel(is_around_savezone);

    }
    public void GoToOverview()
    {
        UIController.Instance.SetOverviewPanel();
    }
    void CloseUI()
    {
       UIController.Instance.OverviewPanel.PlayerEnvironmentClear();
    }
    void AroundPlanet()
    {
        is_around_planet=true;
        UIController.Instance.OverviewPanel.ShowMineOreButton();
    }
    void AroundSavezone()
    {
        is_around_savezone=true;
        Ship.harvesters_left_to_take = Ship.harvesters_takes_ore_per_trip;
        UIController.Instance.OverviewPanel.TradePanel.ShowModularCenterButton();
        UIController.Instance.OverviewPanel.TradePanel.ShowSavezoneIcon();
    }
    void HaveConvertor()  //не совсем Environment, но то же самое по реализации.
    {

    }
}
