using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] MapGrid map_grid;
    [SerializeField] Savezone savezone;
    Planet planet1;
    Planet planet2;
    Asteroid asteroid;
    [SerializeField] Player player;
    Pirats pirats;

    public Savezone Savezone
    {
        get
        {
            return savezone;
        }
    }
    public Planet Planet1
    {
        get
        {
            return planet1;
        }
    }
    public Planet Planet2
    {
        get
        {
            return planet2;
        }
    }
    public Player Player
    {
        get
        {
            return player;
        }
    }

    public void GenerateMap()
    {
        GeneratePlanets();
        GenerateSavezone();
        GeneratePlayer();
    }
    public void SpawnAsteroid(Vector2Int pos)
    {
        Destroy(asteroid.gameObject);
        asteroid = Instantiate(GameData.Instance.AsteroidPrefab).GetComponent<Asteroid>();
        asteroid.Pos = pos;
        asteroid.ore_have = Random.Range(100, 1001);
        string symbols = "QWERTYUIOPASDFGHJKLZXCVBNM123456789";
        asteroid.name = "";

        for (int i = 0; i < 8; i++)
            asteroid.name += symbols[Random.Range(0, symbols.Length)];

        Debug.Log("Asteroid: " + asteroid.name + " was generated");
    }
    public void DestroyAsteroid()
    {

    }
    public Asteroid Asteroid
    {
        get
        {
            return asteroid;
        }
    }
    public Transform ObjectOnPos(Vector2Int pos)
    {
        if (planet1.PosOnMap == pos)
            return planet1.transform;

        if (planet2.PosOnMap == pos)
            return planet2.transform;

        if (savezone.PosOnMap == pos)
            return savezone.transform;

        return null;
    }
    void GenerateSavezone()
    {
        savezone.PosOnMap = new Vector2Int(GameController.Instance.MapSize.x / 2, GameController.Instance.MapSize.y / 2);
    }
    void GeneratePlayer()
    {
        player.PosOnMap = savezone.PosOnMap;
    }
    void GeneratePlanets()
    {
        Vector2Int planet_pos;
        planet1 = Instantiate(GameData.Instance.PlanetPrefab).GetComponent<Planet>();
        planet2 = Instantiate(GameData.Instance.PlanetPrefab).GetComponent<Planet>();

        planet_pos = new Vector2Int(Random.Range(0, (int)GameController.Instance.MapSize.x / 4),
                                    Random.Range(0, GameController.Instance.MapSize.y));

        int planet_name_index = Random.Range(0, GameData.Instance.PlanetNames.Count);

        planet1.GeneratePlanet(planet_pos, 1, GameData.Instance.PlanetNames[planet_name_index]);

        planet_pos = new Vector2Int(Random.Range((int)GameController.Instance.MapSize.x * 3 / 4, (int)GameController.Instance.MapSize.x),
                                    Random.Range(0, GameController.Instance.MapSize.y));

        int planet_buff_index = Random.Range(0, GameData.Instance.PlanetNames.Count);
        while (planet_buff_index == planet_name_index)
            planet_buff_index = Random.Range(0, GameData.Instance.PlanetNames.Count);

        planet_name_index = planet_buff_index;

        planet2.GeneratePlanet(planet_pos, 2, GameData.Instance.PlanetNames[planet_name_index]);
    }
}
