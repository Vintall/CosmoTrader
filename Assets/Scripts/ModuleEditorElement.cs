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

        public Point()
        {
        }
    }
    public List<Point> extra_points = new List<Point>();
    public Point main_point;

    public void AddExtraPoints()
    {
        ScriptableBaseModule module_data = GetComponent<GameModule>().module.Data.ExactLevel(GetComponent<GameModule>().module.CurrentLevel);
        
        for (int i = 0; i < module_data.JointPoints.Count; i++)
        {
            extra_points.Add(new Point());
            extra_points[i].point_object = Instantiate(GameData.Instance.EditorPointPrefab).transform;
            extra_points[i].point_object.parent = this.transform;
            extra_points[i].point_object.GetComponent<EditorPoint>().ChooseExtraPoint();

            extra_points[i].point_object.position = module_data.JointPoints[i].pos;
            extra_points[i].point_object.position -= new Vector3(0, 0, 0.1f);
            extra_points[i].type = PointType.Extra;
        } //Добавление дополнительных точек
        main_point = new Point();
        main_point.point_object = Instantiate(GameData.Instance.EditorPointPrefab).transform;
        main_point.point_object.parent = this.transform;
        main_point.point_object.position = module_data.MainJointPoint.pos;
        main_point.point_object.position -= new Vector3(0, 0, 0.1f);
        main_point.point_object.GetComponent<EditorPoint>().ChooseMainPoint();
        main_point.type = PointType.Main;
    }

    private void OnMouseDrag()
    {
        List<Transform> modules = GameController.Instance.Map.Savezone.ModularCenter.all_modules;
        List<ModuleEditorElement> moduleEditorElements = new List<ModuleEditorElement>();
        foreach (Transform module in modules)
                moduleEditorElements.Add(module.GetComponent<ModuleEditorElement>());
        

        pressed = true;

        if (is_choose_point)
        {
            is_choose_point = false;
            moduleEditorElements[module_id].extra_points[point_id].is_used = false;

            transform.parent = GameController.Instance.Map.Savezone.ModularCenter.transform;
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

        if (is_choose_point)
        {
            moduleEditorElements[module_id].extra_points[point_id].is_used = true;

            transform.parent = modules[module_id];
            moduleEditorElements[module_id].extra_points[point_id].user = transform;//gameObject.transform.rotation = Quaternion.Euler(0,0,-choosed_module.BaseModule.Data.ExactLevel(choosed_module.BaseModule.CurrentLevel).JointPoints[choosed_module.extra_points[point_id].num_in_parent].angle);
        }
        else
        {
            is_choose_point = false;

        }
    }
    const float attract_radius = 10;
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

                ScriptableBaseModule module_data = moduleEditorElements[i].GetComponent<GameModule>().module.Data.ExactLevel(this_module_lvl);
                if (module_data.Type == Structs.ModuleType.CommandCenter && this_module_data.Type != Structs.ModuleType.Body) //Ограничение на слоты в командном центре. Можно ставить только корпуса.
                    continue;

                if (module_data.Type == Structs.ModuleType.Body && this_module_data.Type == Structs.ModuleType.Body) //Ограничение на слоты в командном центре. Можно ставить только корпуса.
                    continue;

                module_lvl = moduleEditorElements[i].GetComponent<GameModule>().module.CurrentLevel;

                for (int j = 0; j < moduleEditorElements[i].extra_points.Count; j++)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (!moduleEditorElements[i].extra_points[j].is_used)
                    {
                        joint_point = moduleEditorElements[i].GetComponent<GameModule>().module.Data.ExactLevel(module_lvl).JointPoints[j].pos;
                        joint_point_angle = moduleEditorElements[i].GetComponent<GameModule>().module.Data.ExactLevel(module_lvl).JointPoints[j].angle;
                        main_point = gameObject.GetComponent<GameModule>().module.Data.ExactLevel(this_module_lvl).MainJointPoint.pos;
                        point_a = new Vector3(moduleEditorElements[i].transform.position.x, moduleEditorElements[i].transform.position.y, 0) + joint_point;
                        point_a = Camera.main.WorldToScreenPoint(point_a);

                        point_b = main_point + new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
                        point_b = Camera.main.WorldToScreenPoint(point_b);

                        float dist = Vector2.Distance(point_a, point_b);
                        point_a = new Vector3(moduleEditorElements[i].transform.position.x, moduleEditorElements[i].transform.position.y, 0) + joint_point;
                        point_b = main_point;


                        if (dist < attract_radius)
                        {
                            gameObject.transform.position = point_a - point_b;
                            
                            gameObject.transform.RotateAround(point_b+transform.position, new Vector3(0, 0, 1), joint_point_angle);

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
