using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    Vector2Int map_size = new Vector2Int(40, 40);
    [SerializeField] Savezone savezone;
    List<Planet> planets;
    [SerializeField] Player player;
    Pirats pirats;

    public Savezone Savezone
    {
        get
        {
            return savezone;
        }
    }
    public Player Player
    {
        get
        {
            return player;
        }
    }
    private void OnDrawGizmos()
    {
        void DrawOneSquare(int i, int j)
        {
            const int modifier = 100;
            Gizmos.DrawLine(new Vector3(i*modifier, j * modifier, 0), new Vector3(i * modifier, (j + 1) * modifier, 0));
            Gizmos.DrawLine(new Vector3(i * modifier, j * modifier, 0), new Vector3((i + 1) * modifier, j * modifier, 0));
            Gizmos.DrawLine(new Vector3((i + 1) * modifier, j * modifier, 0), new Vector3((i + 1) * modifier, (j + 1) * modifier, 0));
            Gizmos.DrawLine(new Vector3(i * modifier, (j + 1) * modifier, 0), new Vector3((i + 1) * modifier, (j + 1) * modifier, 0));
        }
        Gizmos.color = Color.red;
        for (int i = -20; i < 20; i++)
            for (int j = -20; j < 20; j++)
                DrawOneSquare(i, j);
    }
}
