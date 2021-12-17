using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleEditorElement : MonoBehaviour
{
    bool pressed = false;
    bool is_choose_point = false;
    int module_id;
    int point_id;

    private void OnMouseDrag()
    {
        List<ModularCenter.ModuleExtraPoints> modules = GameController.Instance.Map.Savezone.ModularCenter.ModuleExtraPointsList;
        pressed = true;
        if (is_choose_point)
        {
            is_choose_point = false;
            modules[module_id].extra_points[point_id].is_used = false;

            transform.parent = GameController.Instance.Map.Savezone.ModularCenter.transform;
            modules[module_id].extra_points[point_id].user = null;

            module_id = -1;
        }

    }
    private void OnMouseUp()
    {
        List<ModularCenter.ModuleExtraPoints> modules = GameController.Instance.Map.Savezone.ModularCenter.ModuleExtraPointsList;
        pressed =false;
        if(is_choose_point)
        {
            modules[module_id].extra_points[point_id].is_used = true;

            transform.parent = modules[module_id].module;
            modules[module_id].extra_points[point_id].user = transform;
           //gameObject.transform.rotation = Quaternion.Euler(0,0,-choosed_module.BaseModule.Data.ExactLevel(choosed_module.BaseModule.CurrentLevel).JointPoints[choosed_module.extra_points[point_id].num_in_parent].angle);
        }
    }
    const float attract_radius = 10;
    private void Update()
    {
        List<ModularCenter.ModuleExtraPoints> modules = GameController.Instance.Map.Savezone.ModularCenter.ModuleExtraPointsList;
        if (pressed)
        {
            Vector3 point_a;
            Vector3 point_b;
            Vector3 joint_point;
            Vector3 main_point;


            int this_module_lvl = gameObject.GetComponent<GameModule>().module.CurrentLevel;
            int module_lvl;
            for (int i = 0; i < modules.Count; i++)
            {
                module_lvl = modules[i].BaseModule.CurrentLevel;
                for (int j = 0; j < modules[i].extra_points.Count; j++)
                {
                    if (!modules[i].extra_points[j].is_used && modules[i].is_avaliable)
                    {
                        int num_in_parent = modules[i].extra_points[j].num_in_parent;
                        joint_point = modules[i].BaseModule.Data.ExactLevel(module_lvl).JointPoints[num_in_parent].pos;
                        main_point = gameObject.GetComponent<GameModule>().module.Data.ExactLevel(this_module_lvl).MainJointPoint.pos;
                        point_a = new Vector3(modules[i].module.position.x, modules[i].module.position.y, 0) + joint_point;
                        point_a = Camera.main.WorldToScreenPoint(point_a);

                        point_b = main_point + new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                        point_b = Camera.main.WorldToScreenPoint(point_b);

                        float dist = Vector2.Distance(point_a, point_b);
                        point_a = new Vector3(modules[i].module.position.x, modules[i].module.position.y, 0) + joint_point;
                        point_b = main_point;

                        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                        
                        if (dist < attract_radius)
                        {
                            
                            gameObject.transform.position = point_a - point_b;
                            //modules[i].extra_points[j].is_used = true;

                            module_id = i;
                            point_id = j;
                            is_choose_point = true;

                            //transform.parent = modules[i].module;
                            //modules[i].extra_points[j].user = transform;
                            return;
                        }
                    }
                }
            }
            is_choose_point = false;
            gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
