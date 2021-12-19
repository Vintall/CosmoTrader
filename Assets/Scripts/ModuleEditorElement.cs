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
    bool is_avaliable;

    public enum PointType
    {
        Main,
        Extra
    }
    public class Point
    {
        public Transform point_object; //Вспомогательная точка (метка для расстановки модулей)
        public Transform user;    //Модуль, прикреплённый к этой точке
        public bool is_used;
        public PointType type;

        public void DestroyPointObject()
        {
            Destroy(point_object.gameObject);
        }
        public Point()
        {
        }
        public void Clear()
        {
            Destroy(point_object.gameObject);

        }
    }
    public List<Point> extra_points = new List<Point>();
    public Point main_point;

    public void AddExtraPoints()
    {
        List<(int, Transform)> users = new List<(int, Transform)>();
        for(int i=0;i<extra_points.Count;i++) 
        {
            if (extra_points[i].is_used)
                users.Add((i, extra_points[i].user));
            extra_points[i].Clear();
        }

        if (main_point != null)
            main_point.Clear();
        extra_points.Clear();
        
        ScriptableBaseModule module_data = GetComponent<GameModule>().module.Data.ExactLevel(GetComponent<GameModule>().module.CurrentLevel);
        
        for (int i = 0; i < module_data.JointPoints.Count; i++)
        {
            extra_points.Add(new Point());
            extra_points[i].point_object = Instantiate(GameData.Instance.EditorPointPrefab).transform;
            extra_points[i].point_object.parent = this.transform;
            extra_points[i].point_object.GetComponent<EditorPoint>().ChooseExtraPoint();

            extra_points[i].point_object.localPosition = module_data.JointPoints[i].pos;
            extra_points[i].point_object.localPosition -= new Vector3(0, 0, 0.1f);
            extra_points[i].type = PointType.Extra;

            
        } //Добавление дополнительных точек
        main_point = new Point();
        main_point.point_object = Instantiate(GameData.Instance.EditorPointPrefab).transform;
        main_point.point_object.parent = this.transform;
        main_point.point_object.localPosition = module_data.MainJointPoint.pos;

        main_point.point_object.localPosition -= new Vector3(0, 0, 0.1f);

        main_point.point_object.GetComponent<EditorPoint>().ChooseMainPoint();
        main_point.type = PointType.Main;
    }

    private void OnMouseDown()
    {
        List<Transform> modules = GameController.Instance.Map.Savezone.ModularCenter.all_modules;
        List<ModuleEditorElement> moduleEditorElements = new List<ModuleEditorElement>();
        foreach (Transform module in modules)
                moduleEditorElements.Add(module.GetComponent<ModuleEditorElement>());
        

        pressed = true;
        GameController.Instance.Map.Savezone.ModularCenter.DeselectModule();
        if (is_choose_point)
        {
            is_choose_point = false;
            moduleEditorElements[module_id].extra_points[point_id].is_used = false;

            transform.parent = GameController.Instance.Map.Savezone.ModularCenter.EditingShip;
            moduleEditorElements[module_id].extra_points[point_id].user = null;

            module_id = -1;
            point_id = -1;
            
        }
    }
    private void OnMouseUp()
    {
        List<Transform> modules = GameController.Instance.Map.Savezone.ModularCenter.all_modules;
        List<ModuleEditorElement> moduleEditorElements = new List<ModuleEditorElement>();
        foreach (Transform module in modules)
                moduleEditorElements.Add(module.GetComponent<ModuleEditorElement>());

        pressed = false;
        GameController.Instance.Map.Savezone.ModularCenter.SelectModule(transform);
        if (is_choose_point)
        {
            moduleEditorElements[module_id].extra_points[point_id].is_used = true;

            transform.parent = modules[module_id];
            moduleEditorElements[module_id].extra_points[point_id].user = transform;
            
        }
        else
        {
            is_choose_point = false;
        }
    }
    const float attract_radius = 30;
    private void Update()
    {
        List<Transform> modules = GameController.Instance.Map.Savezone.ModularCenter.all_modules;
        List<ModuleEditorElement> moduleEditorElements = new List<ModuleEditorElement>();

        foreach (Transform module in modules)
                moduleEditorElements.Add(module.GetComponent<ModuleEditorElement>());

        if (pressed)
        {
            Vector3 point_a;
            Vector3 point_b;
            Vector3 joint_point;
            Vector3 main_point;
            float joint_point_angle;
            is_choose_point = false;

            int this_module_lvl = gameObject.GetComponent<GameModule>().module.CurrentLevel;
            ScriptableBaseModule this_module_data = gameObject.GetComponent<GameModule>().module.Data.ExactLevel(this_module_lvl);

            int module_lvl;

            gameObject.transform.position =
                            new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - this_module_data.MainJointPoint.pos.x,
                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - this_module_data.MainJointPoint.pos.y, 0);

            for (int i = 0; i < moduleEditorElements.Count; i++)
            {
                if (moduleEditorElements[i] == this)
                    continue;
                module_lvl = moduleEditorElements[i].GetComponent<GameModule>().module.CurrentLevel;
                ScriptableBaseModule module_data = moduleEditorElements[i].GetComponent<GameModule>().module.Data.ExactLevel(module_lvl);

                if (module_data.Type == Structs.ModuleType.CommandCenter && this_module_data.Type != Structs.ModuleType.Body) //Ограничение на слоты в командном центре. Можно ставить только корпуса.
                    continue;

                if (module_data.Type == Structs.ModuleType.Body && this_module_data.Type == Structs.ModuleType.Body) //Ограничение на слоты в командном центре. Можно ставить только корпуса.
                    continue;

                if (this_module_data.Type == Structs.ModuleType.CommandCenter)
                    continue;


                

                for (int j = 0; j < moduleEditorElements[i].extra_points.Count; j++)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (!moduleEditorElements[i].extra_points[j].is_used)
                    {
                        ScriptableBaseModule point_user_data;
                        List<Structs.ModuleType> this_module_blocked_types = this_module_data.BuildingBlockedObjects;
                        List<Structs.ModuleType> module_blocked_types;


                        if (j - 1 >= 0 && moduleEditorElements[i].extra_points[j - 1].is_used)
                        {
                            point_user_data = moduleEditorElements[i].extra_points[j - 1].user.GetComponent<GameModule>().module.Data.
                                ExactLevel(moduleEditorElements[i].extra_points[j - 1].user.GetComponent<GameModule>().module.CurrentLevel);

                            module_blocked_types = point_user_data.BuildingBlockedObjects;
                            bool is_continue = false;
                            foreach (Structs.ModuleType blocked_module_type in this_module_blocked_types)
                            {
                                if (point_user_data.Type == blocked_module_type)
                                {
                                    is_continue = true;
                                    break;
                                }
                            }
                            foreach (Structs.ModuleType blocked_module_type in module_blocked_types)
                            {
                                if (this_module_data.Type == blocked_module_type)
                                {
                                    is_continue = true;
                                    break;
                                }
                            }
                            if (is_continue)
                                continue;
                        }
                        if (j + 1 < moduleEditorElements[i].extra_points.Count && moduleEditorElements[i].extra_points[j + 1].is_used)
                        {
                            point_user_data = moduleEditorElements[i].extra_points[j + 1].user.GetComponent<GameModule>().module.Data.
                                ExactLevel(moduleEditorElements[i].extra_points[j + 1].user.GetComponent<GameModule>().module.CurrentLevel);

                            module_blocked_types = point_user_data.BuildingBlockedObjects;
                            bool is_continue = false;
                            foreach (Structs.ModuleType blocked_module_type in this_module_blocked_types)
                            {
                                if (point_user_data.Type == blocked_module_type)
                                {
                                    is_continue = true;
                                    break;
                                }
                            }
                            foreach (Structs.ModuleType blocked_module_type in module_blocked_types)
                            {
                                if (this_module_data.Type == blocked_module_type)
                                {
                                    is_continue = true;
                                    break;
                                }
                            }
                            if (is_continue)
                                continue;
                        }

                        joint_point = moduleEditorElements[i].extra_points[j].point_object.position;

                        joint_point_angle = moduleEditorElements[i].GetComponent<GameModule>().module.Data.ExactLevel(module_lvl).JointPoints[j].angle -
                            moduleEditorElements[i].extra_points[j].point_object.transform.eulerAngles.z;

                        main_point = this.main_point.point_object.position;

                        point_a = joint_point;
                        point_a = Camera.main.WorldToScreenPoint(point_a);

                        point_b = main_point;
                        point_b = Camera.main.WorldToScreenPoint(point_b);

                        float dist = Vector2.Distance(point_a, point_b);
                        point_a = joint_point;
                        point_b = main_point;

                        if (dist < attract_radius)
                        {
                            gameObject.transform.RotateAround(point_b, new Vector3(0, 0, 1), 180 - joint_point_angle);
                            gameObject.transform.position = moduleEditorElements[i].extra_points[j].point_object.position
                                - this.main_point.point_object.position + this.transform.position;

                            switch(GetComponent<GameModule>().module.Data.ExactLevel(1).Type)
                            {
                                case Structs.ModuleType.Body:
                                    gameObject.transform.position += new Vector3(0, 0, 0.1f);
                                    break;
                                default:
                                    gameObject.transform.position -= new Vector3(0, 0, 0.3f);
                                    break;
                            }

                            module_id = i;
                            point_id = j;
                            is_choose_point = true;

                            return;
                        }
                    }
                }
            }
        }
    }
}
