using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    [SerializeField] GameObject map_grid_cell_prefab;
    
    [SerializeField] Color cell_1;
    [SerializeField] Color cell_2;
    [SerializeField] Color cell_zone;

    [SerializeField] Transform player_mark;
    [SerializeField] Transform planet_mark_1;
    [SerializeField] Transform planet_mark_2;
    [SerializeField] Transform savezone_mark;
    [SerializeField] Transform asteroid_mark;

    Transform[,] cells;


    public void SetPlayerMark(Vector2Int pos)
    {
        player_mark.parent = cells[pos.x, pos.y];
        player_mark.localPosition = new Vector3(0, 0, -0.2f);
    }
    public void SetPlanet1Mark(Vector2Int pos)
    {
        planet_mark_1.parent = cells[pos.x, pos.y];
        planet_mark_1.localPosition = new Vector3(0, 0, -0.1f);
    }
    public void SetPlanet2Mark(Vector2Int pos)
    {
        planet_mark_2.parent = cells[pos.x, pos.y];
        planet_mark_2.localPosition =new Vector3(0, 0, -0.1f);

    }
    public void SetSavezoneMark(Vector2Int pos)
    {
        savezone_mark.parent = cells[pos.x, pos.y];
        savezone_mark.localPosition = new Vector3(0, 0, -0.1f);

    }
    public void SetAsteroidMark(Vector2Int pos)
    {
        asteroid_mark.parent = cells[pos.x, pos.y];
        asteroid_mark.localPosition = new Vector3(0, 0, -0.1f);

    }
    public void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector2 delta = transform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        if (scroll != 0)
        {
            transform.position = transform.position + new Vector3(delta.x * scroll / transform.localScale.x, delta.y * scroll / transform.localScale.x, 0);
            transform.localScale = transform.localScale + new Vector3(scroll, scroll, 0);
        }
    }
    public void GenerateGrid(Vector2Int size)
    {
        Transform buff;
        cells = new Transform[size.x, size.y];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                buff = Instantiate(map_grid_cell_prefab).transform;
                buff.parent = this.transform;
                
                cells[i,j] = buff;

                if ((i + j) % 2 == 0)
                {
                    buff.GetComponent<MapCell>().GenerateCell(cell_1, new Vector2Int(i, j));
                }
                else
                {
                    buff.GetComponent<MapCell>().GenerateCell(cell_2, new Vector2Int(i, j));
                }
                float pixel_per_unit = buff.GetComponent<MapCell>().SpriteRenderer.sprite.pixelsPerUnit;
                Vector2 rect_size = buff.GetComponent<MapCell>().SpriteRenderer.sprite.rect.size / pixel_per_unit;
                buff.localPosition = new Vector3(rect_size.x * (i-size.x/2), rect_size.y * (j - size.y / 2), 0);
            }
        }
    }
    public void OnCellClick(Vector2Int cell_pos)
    {
        GameController.Instance.Map.Player.MoveTo(cell_pos);
    }
}
