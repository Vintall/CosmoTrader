using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{



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
        {
            for (int j = -20; j < 20; j++)
            {
                DrawOneSquare(i, j);
                //Gizmos.DrawLine(new Vector3(i, j, 0), new Vector3(i, j + 1, 0));
                //Gizmos.DrawLine(new Vector3(i, j, 0), new Vector3(i + 1, j, 0));
                //Gizmos.DrawLine(new Vector3(i + 1, j, 0), new Vector3(i + 1, j + 1, 0));
                //Gizmos.DrawLine(new Vector3(i, j + 1, 0), new Vector3(i + 1, j + 1, 0));
            }
        }
    }
}
