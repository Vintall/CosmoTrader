using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCell : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite_renderer;
    public Vector2Int pos_on_grid;
    Color default_color;

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return sprite_renderer;
        }
    }
    private void OnMouseUpAsButton()
    {
        if (is_click_available) 
        transform.parent.GetComponent<MapGrid>().OnCellClick(pos_on_grid);
    }
    bool is_drag = false;
    Vector2 point_drag_start;
    Vector2 point_drag_start_mouse;
    Vector2 delta;
    const float drag_radius = 1;
    private void OnMouseDown()
    {
        point_drag_start = transform.parent.position;
        point_drag_start_mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

        delta = new Vector2(point_drag_start_mouse.x - point_drag_start.x,
                                        point_drag_start_mouse.y - point_drag_start.y);
        is_drag = true;
        is_drag_available = false;
    }
    private void OnMouseUp()
    {
        is_drag = false;
        is_drag_available = false;
    }
    bool is_click_available = false;
    bool is_drag_available = false;
    void Update()
    {
        Vector2 screen_to_world_pos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        is_click_available = true;

        if (is_drag_available)
        {
            
            transform.parent.position = screen_to_world_pos - delta;
            is_click_available = false;
        }

        if (!is_drag)
            return;

        if (Vector2.Distance(point_drag_start_mouse, screen_to_world_pos) > drag_radius)
        {
            is_drag_available = true;
        }

        
        
    }
    public void GenerateCell(Color color, Vector2Int pos)
    {
        default_color = color;
        sprite_renderer.color = default_color;
        pos_on_grid = pos;
    }
    public void ChangeColor(Color color)
    {
        sprite_renderer.color = color;
    }
    public void ChangeColorToDefault()
    {
        sprite_renderer.color = default_color;
    }
}
