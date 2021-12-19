using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Map map;
    Vector2Int map_size = new Vector2Int(40, 40);
    bool is_game_over = false;
    public Vector2Int MapSize
    {
        get
        {
            return map_size;
        }
    }
    private static GameController instance;

    public Map Map
    {
        get
        {
            return map;
        }
    }
    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        UIController.Instance.MapGrid.GenerateGrid(map_size);

        map.GenerateMap();

        map.Player.ship.GetComponent<Ship>().money = 2500;
        map.Player.ship.GetComponent<Ship>().MW_have = 500000;

        map.Savezone.ModularCenter.StartEditing(map.Player.transform, map.Player.ship);
    }
    public void SpawnPirates()
    {
        int bodies_count = Random.Range(2, 5);
        int elements_on_bodies_count = bodies_count * 4;
        List<ScriptableBaseModule> modules = new List<ScriptableBaseModule>();
        modules.Add(GameData.Instance.GetModuleByID("CommandCenter_1").ExactLevel(1));

        for (int i = 0; i < bodies_count; i++)
            modules.Add(GameData.Instance.GetModuleByID("Body_1").ExactLevel(Random.Range(1, 4)));

        for (int i = 0; i < elements_on_bodies_count-2; i++)
        {
            ScriptableBaseModule module = GameData.Instance.AllModules[Random.Range(0, GameData.Instance.AllModules.Count)].ExactLevel(Random.Range(1, 4));
            while (module.Type == Structs.ModuleType.CommandCenter || module.Type == Structs.ModuleType.Body)
                module = GameData.Instance.AllModules[Random.Range(0, GameData.Instance.AllModules.Count)].ExactLevel(Random.Range(1, 4));

            modules.Add(module);
        }
        modules.Add(GameData.Instance.GetModuleByID("Cannon_1").ExactLevel(Random.Range(1, 4)));
        modules.Add(GameData.Instance.GetModuleByID("Cannon_1").ExactLevel(Random.Range(1, 4)));

        int durability = 0;
        int cannon_damage_per_shot = 0;

        foreach(ScriptableBaseModule module in modules)
        {
            durability += module.Durability;
            if (module.Type == Structs.ModuleType.Cannon)
                cannon_damage_per_shot += ((ScriptableCannon)module).DamagePerShot;
        }
        Fight(durability, cannon_damage_per_shot);
    }
    int pirates_destroyed = 0;
    int battle_num = 0;
    public void Fight(int pirate_durability, int pirate_damage)
    {
        Debug.Log("Атака пиратов");
        battle_num++;
        BattleLoger.battle_num = battle_num;
        BattleLoger.CreateFile();

        BattleLoger.Log("Начальная прочность игрока = " + map.Player.Ship.Durability);
        BattleLoger.Log("Сила атаки игрока = " + map.Player.Ship.cannons_deals_damage);
        BattleLoger.Log("");
        BattleLoger.Log("Начальная прочность пиратов = " + pirate_durability);
        BattleLoger.Log("Сила атаки пиратов = " + pirate_damage);
        BattleLoger.Log("");
        BattleLoger.Log("");
        while (pirate_durability > 0 && map.Player.Ship.Durability > 0) 
        {
            map.Player.Ship.Durability -= pirate_damage;

            BattleLoger.Log("Прочность игрока = " + map.Player.Ship.Durability);

            pirate_durability -= map.Player.Ship.cannons_deals_damage;

            BattleLoger.Log("Прочность пиратов = " + pirate_durability);
            BattleLoger.Log("");
        }
        if (!is_game_over)
        {
            map.Player.Ship.OreHave += 1000;
            map.Player.Ship.MWHave += 100;
            pirates_destroyed++;
            BattleLoger.Log("Пираты уничтожены! ");
            BattleLoger.CloseFile();
            if (pirates_destroyed==3)
            {
                pirates_destroyed = 0;
                SpawnAsteroid();
            }
        }
        else
        {
            BattleLoger.Log("Игра окончена! ");
            BattleLoger.CloseFile();
        }
    }
    public void SpawnAsteroid()
    {
        Vector2Int asteroid_pos;
        NewPos();
        void NewPos()
        {
            asteroid_pos = new Vector2Int(Random.Range(0, map_size.x), Random.Range(0, map_size.y));

            if (asteroid_pos == map.Planet1.PosOnMap ||
                asteroid_pos == map.Planet2.PosOnMap ||
                asteroid_pos == map.Savezone.PosOnMap)
                NewPos();
        }
        asteroid_pos = new Vector2Int(Random.Range(0, map_size.x), Random.Range(0, map_size.y));
        map.SpawnAsteroid(asteroid_pos);
    }
    private void Awake()
    {
        instance = this;
    }
    public void GameOver(string description, bool is_win)
    {
        is_game_over = true;
        if (is_win)
            Debug.Log("Победа");
        else
            Debug.Log("Поражение");
        Destroy(map);
        Debug.Log(description);
    }
}
