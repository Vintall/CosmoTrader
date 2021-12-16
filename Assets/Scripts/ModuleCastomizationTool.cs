using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCastomizationTool : MonoBehaviour
{
    [SerializeField] ScriptableBaseModule module;
    [SerializeField] SpriteRenderer sprite_renderer;

    [SerializeField, Range(0.1f, 10f)] float sphere_radius = 2;

    private void OnDrawGizmos()
    {
        if (module == null)
            return;

        if (module.Sprite != null)
        {
            sprite_renderer.sprite = module.Sprite;
        }
        else if (sprite_renderer.sprite != null)
            sprite_renderer.sprite = null;


        Gizmos.color = Color.blue;
        
        Vector3 start_pos;
        Vector3 end_pos;
        for (int i = 0; i < module.JointPoints.Count; i++)
        {
            start_pos = new Vector3(module.JointPoints[i].pos.x, module.JointPoints[i].pos.y, 0) + transform.position;
            end_pos = Vector3.up*5;
            Gizmos.DrawLine(start_pos, Quaternion.Euler(0, 0, -module.JointPoints[i].angle) * end_pos + start_pos);

            Gizmos.DrawSphere(start_pos, sphere_radius);
        }
        Gizmos.color = Color.red;

        start_pos = new Vector3(module.MainJointPoint.pos.x, module.MainJointPoint.pos.y, 0) + transform.position;
        end_pos = Vector3.up * 5;

        Gizmos.DrawLine(start_pos, Quaternion.Euler(0, 0, -module.MainJointPoint.angle) * end_pos + start_pos);

        Gizmos.DrawSphere(start_pos, sphere_radius);
    }
}
