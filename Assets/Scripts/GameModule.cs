using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModule : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite_renderer;
    [SerializeField] PolygonCollider2D polygon_collider;

    public BaseModule module;

    public void RefreshCollider()
    {
        Destroy(polygon_collider);
        gameObject.AddComponent<PolygonCollider2D>();
    }
    public PolygonCollider2D PolygonCollider
    {
        get
        {
            return polygon_collider;
        }
    }
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            return sprite_renderer;
        }
    }
}
