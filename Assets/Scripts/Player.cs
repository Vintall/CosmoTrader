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
        int energy_takes = (int)(distance * ship_component.engines_consume_per_100_km / 100 - ship_component.generators_generate_per_100_km * distance);
        if (energy_takes > ship_component.MW_have)
        {
            UIController.Instance.OverviewPanel.AddLog("Недостаточно энергии для перемещения\nоставшаяся энергия будет потрачена на разогрев движков\n" +
                "есть 15 секунд, чтобы добыть энергию для перемещения к выбраной точке", 5);
            StartCoroutine(EnergySecondeChance(energy_takes, pos));
            //ship_component.MWHave = 0;
            return;
        }
        ship_component.MWHave -= energy_takes;
        PosOnMap = pos;
        Debug.Log("Moved to (" + pos.x + "; " + pos.y + ")");
        UIController.Instance.MapGrid.SetPlayerMark(pos);
    }
    IEnumerator EnergySecondeChance(int energy_takes, Vector2Int pos)
    {
        yield return new WaitForSeconds(15);
        if (Ship.MWHave < energy_takes)
            GameController.Instance.GameOver("у вас закончилась энергия и вы застряли в космосе", false);
        else
        {
            MoveTo(pos);
        }
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

        if (map.Asteroid != null)
            if (pos == map.Asteroid.Pos)
                AroundAsteroid();
    }
    public void UpdateUIStates()
    {
        UIController.Instance.OverviewPanel.ChangeStates(
            Ship.Money,
            Ship.MWHave,
            Ship.MW_capacity,
            Ship.Durability,
            Ship.max_durability,
            Ship.ore_have,
            Ship.ore_capacity);

        UIController.Instance.ShopPanel.SellOrePanel.ChangeStates(Ship.ore_have, Ship.ore_capacity);
        UIController.Instance.ShopPanel.BuyEnergyPanel.ChangeStates(Ship.ore_have, Ship.ore_capacity);
    }
    bool is_around_planet = false;
    bool is_around_asteroid = false;
    bool is_around_savezone = true;
    public bool is_have_convertor = false;

    public void MineOre()
    {
        if (is_around_planet)
            Ship.MineOre(transform.parent.GetComponent<Map>().ObjectOnPos(pos).GetComponent<Planet>());

        if (is_around_asteroid)
            Ship.MineOre(transform.parent.GetComponent<Map>().Asteroid);
    }
    public void BuyEnergy(float cost, int count)
    {
        while (Ship.Money >= cost && Ship.MWHave < Ship.MW_capacity)  //Нерационально. Тяжёлый код. Можно расчитать и сделать одной итерацией.
        {
            Ship.MWHave += count;
            Ship.Money -= cost;
        }
        UIController.Instance.ShopPanel.BuyEnergyPanel.ChangeStates(Ship.ore_have, Ship.ore_capacity);
    }
    
    public void SellOre(float cost, int count)
    {
        while (Ship.ore_have > 0)   //Нерационально. Тяжёлый код
        {
            Ship.Money += cost;
            Ship.ore_have -= count;
        }
        UIController.Instance.ShopPanel.SellOrePanel.ChangeStates(Ship.ore_have, Ship.ore_capacity);
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
    public void GoToConvectorPanel()
    {
        UIController.Instance.SetConvertorPanel();
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
    void AroundAsteroid()
    {
        is_around_asteroid = true;
        UIController.Instance.OverviewPanel.ShowMineOreButton();
    }
}
