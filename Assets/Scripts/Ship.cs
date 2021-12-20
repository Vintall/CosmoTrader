using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    CommandCenterModule command_center;
    public CommandCenterModule CommandCenter
    {
        get
        {
            return command_center;
        }
        set
        {
            command_center = value;
        }
    }

    public float money = 0;  // Важное

    public int MW_capacity = 0;
    public int MW_have = 0;  // Важное

    public int max_durability = 0;
    public int durability = 0;  // Важное

    public int ore_capacity = 0;
    public int ore_have = 0;  // Важное

    public int engines_consume_per_100_km = 0;
    public int engines_consume_per_battle = 0;

    public int cannons_consume_per_shot = 0;
    public int cannons_deals_damage = 0;

    public int harvesters_left_to_take = 0;
    public int harvesters_takes_ore_per_trip = 0;
    public int harvesters_consume_per_mine = 0;

    public int convertors_best_ore_count_per_MW = int.MaxValue;

    public int generators_generate_per_100_km = 0;

    public int repairers_deals_durability_per_10_min = 0;
    public int repairers_consume_mw = 0;

    public int OreHave
    {
        get
        {
            return ore_have;
        }
        set
        {
            ore_have = value;

            if (ore_have > ore_capacity)
                ore_have = ore_capacity;

            UpdateUIStates();
        }
    }
    public float Money
    {
        get
        {
            return money;

        }
        set
        {
            money = value;

            if (money > 5000)
                GameController.Instance.GameOver("Пользователь собрал 5000 криптовалюты", true);
        }
    }
    public int ShotToPirates()
    {
        MW_have -= cannons_consume_per_shot;
        if (MW_have < 0)
            GameController.Instance.GameOver("При борьбе с пиратами у Вас закончилась энергия", false);
        return cannons_deals_damage;
    }
    public int Durability
    {
        get
        {
            return durability;
        }
        set
        {
            durability = value;
            if (durability <= 0)
                GameController.Instance.GameOver("закончилась прочность корабля", false);

            UpdateUIStates();
        }
    }
    public int MWHave
    {
        get
        {
            return MW_have;
        }
        set
        {
            MW_have = value;
            if (MW_have <= 0) 
            {
                MW_have = 0;
                //UIController.Instance.OverviewPanel.AddLog("у вас закончилась энергия\n");

                //GameController.Instance.GameOver(" и вы не можете связаться с орбитальной станцией", false);
            }
            UpdateUIStates();
        }
    }
    float time_repairers;
    bool start_repairers = false;
    public void RepairersStart()
    {
        start_repairers = true;
        time_repairers = Time.realtimeSinceStartup + 3;
    }
    private void FixedUpdate()
    {
        if (start_repairers)
            if (Time.realtimeSinceStartup > time_repairers)
            {
                time_repairers = Time.realtimeSinceStartup;
                RepairersStart();
                if (repairers_consume_mw > MWHave)
                    return;

                Durability += repairers_deals_durability_per_10_min;
                MWHave -= repairers_consume_mw;
            }
    }
    public void UpdateUIStates()
    {
        UIController.Instance.OverviewPanel.ChangeStates(
            money,
            MWHave,
            MW_capacity,
            Durability,
            max_durability,
            ore_have,
            ore_capacity);

        UIController.Instance.ShopPanel.SellOrePanel.ChangeStates(ore_have, ore_capacity);
        UIController.Instance.ShopPanel.BuyEnergyPanel.ChangeStates(ore_have, ore_capacity);
    }
    public void ExchangeOreToEnergy()
    {
        if (ore_have >= convertors_best_ore_count_per_MW)
        {
            ore_have -= convertors_best_ore_count_per_MW;
            MWHave += 1;
        }
    }
    public void MineOre(Planet planet)
    {
        if (MWHave < harvesters_consume_per_mine)
            return;
        bool is_mine = false;
        while (MWHave >= harvesters_consume_per_mine &&
              OreHave < ore_capacity &&
              harvesters_left_to_take > 0)
        {
            MWHave -= harvesters_consume_per_mine;
            OreHave += harvesters_takes_ore_per_trip;
            harvesters_left_to_take -= Mathf.Min(harvesters_left_to_take, harvesters_takes_ore_per_trip);
            is_mine = true;
        }
        if (is_mine)
        {
            int pirates_atack_chance = Random.Range(0, 100);
            if (pirates_atack_chance <= 40)
            {
                UIController.Instance.OverviewPanel.AddLog("На вас напали пираты");
                StartCoroutine(PiratesLog());
            }
        }
    }
    IEnumerator PiratesLog()
    {
        yield return new WaitForSeconds(1);
        GameController.Instance.SpawnPirates();
    }
    public void MineOre(Asteroid asteroid)
    {
        if (MWHave < harvesters_consume_per_mine)
            return;

        while (MWHave >= harvesters_consume_per_mine &&
              OreHave < ore_capacity &&
              asteroid.ore_have > 0 &&
              harvesters_left_to_take > 0)
        {
            MWHave -= harvesters_consume_per_mine;
            int asteroid_ore_have = asteroid.ore_have;
            OreHave += Mathf.Min(asteroid_ore_have, harvesters_takes_ore_per_trip, harvesters_left_to_take);
            asteroid.OreHave -= Mathf.Min(asteroid_ore_have, harvesters_takes_ore_per_trip, harvesters_left_to_take);
            harvesters_left_to_take -= Mathf.Min(asteroid_ore_have, harvesters_takes_ore_per_trip, harvesters_left_to_take);
        }
    }
    public List<BaseModule> GetAllModules()
    {
        List<BaseModule> modules = new List<BaseModule>();
        List<BaseModule> bodies = command_center.GetAllChilds();
        List<BaseModule> buff;
        modules.Add(command_center);
        foreach (BaseModule b in bodies)
        {
            buff = b.GetAllChilds();

            foreach (BaseModule final_module in buff)
                modules.Add(final_module);
            modules.Add(b);
        }
        return modules;
    }
    public void GetModulesStates()
    {
        Ship buff_ship = new Ship();
        List<BaseModule> all_modules = GetAllModules();
        foreach (BaseModule b in all_modules)
        {
            b.GetStates(ref buff_ship);
        }
        this.MW_capacity= buff_ship.MW_capacity;

        this.ore_capacity = buff_ship.ore_capacity;

        this.engines_consume_per_100_km = buff_ship.engines_consume_per_100_km;
        this.engines_consume_per_battle = buff_ship.engines_consume_per_battle;

        this.cannons_consume_per_shot = buff_ship.cannons_consume_per_shot;
        this.cannons_deals_damage = buff_ship.cannons_deals_damage;

        this.harvesters_takes_ore_per_trip = buff_ship.harvesters_takes_ore_per_trip;
        this.harvesters_consume_per_mine = buff_ship.harvesters_consume_per_mine;

        this.convertors_best_ore_count_per_MW = buff_ship.convertors_best_ore_count_per_MW;

        this.generators_generate_per_100_km = buff_ship.generators_generate_per_100_km;

        this.repairers_deals_durability_per_10_min = buff_ship.repairers_deals_durability_per_10_min;
        this.repairers_consume_mw = buff_ship.repairers_consume_mw;

        this.max_durability = buff_ship.max_durability;
        this.durability = max_durability;

    }
}
