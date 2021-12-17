using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularCenter : MonoBehaviour
{
    [SerializeField] Sprite main_point_texture;
    [SerializeField] Sprite extra_point_texture;
    [SerializeField] GameObject point_prefab;

    Transform editing_ship;
    Transform active_module;

    bool is_editing;

    List<(int, Transform, bool)> all_extra_point = new List<(int, Transform, bool)>();

    public class ExtraPoint
    {
        public Transform point_object;
        public Transform user;    //Модуль, прикреплённый к этой точке
        public bool is_used;      
        public int num_in_parent; //Номер точки в модуле

        public ExtraPoint(int num_in_parent)
        {
            this.num_in_parent = num_in_parent;
        }
    }
    public class ModuleExtraPoints
    {
        public Transform module;
        public bool is_avaliable;
        public List<ExtraPoint> extra_points;
        

        public ModuleExtraPoints()
        {
            extra_points = new List<ExtraPoint>();
            is_avaliable = true;
            module = null;
        }
        public BaseModule BaseModule
        {
            get
            {
                return module.gameObject.GetComponent<BaseModule>();
            }
        }
    }
    List<ModuleExtraPoints> module_extra_points = new List<ModuleExtraPoints>();
    public List<ModuleExtraPoints> ModuleExtraPointsList
    {
        get
        {
            return module_extra_points;
        }
    }
    public void BuyModule(string id)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> module_data = GameData.Instance.GetModuleByID(id);
        GameObject new_object = Instantiate(GameData.Instance.ModulePrefab);
        GameModule new_object_game_module = new_object.GetComponent<GameModule>();
        new_object.AddComponent<ModuleEditorElement>();
        switch (module_data.ExactLevel(1).Type)
        {
            case Structs.ModuleType.CommandCenter:
                new_object_game_module.module = new_object.AddComponent<CommandCenterModule>();
                break;
            case Structs.ModuleType.Body:
                new_object_game_module.module = new_object.AddComponent<BodyModule>();
                break;
            case Structs.ModuleType.Engine:
                new_object_game_module.module = new_object.AddComponent<EngineModule>();
                break;
            case Structs.ModuleType.Battery:
                new_object_game_module.module = new_object.AddComponent<BatteryModule>();
                break;
            case Structs.ModuleType.Storage:
                new_object_game_module.module = new_object.AddComponent<StorageModule>();
                break;
            case Structs.ModuleType.Cannon:
                new_object_game_module.module = new_object.AddComponent<CannonModule>();
                break;
            case Structs.ModuleType.Harvester:
                new_object_game_module.module = new_object.AddComponent<HarvesterModule>();
                break;
            case Structs.ModuleType.Convertor:
                new_object_game_module.module = new_object.AddComponent<ConvertorModule>();
                break;
            case Structs.ModuleType.Generator:
                new_object_game_module.module = new_object.AddComponent<GeneratorModule>();
                break;
            case Structs.ModuleType.Repairer:
                new_object_game_module.module = new_object.AddComponent<RepairerModule>();
                break;
        }
        new_object.transform.parent = transform;
        new_object_game_module.module.GenerateObject(module_data, 1);
        new_object_game_module.SpriteRenderer.sprite = module_data.ExactLevel(new_object_game_module.module.CurrentLevel).Sprite;

        AddExtraPoints(new_object.transform);
        
        new_object_game_module.RefreshCollider();
        //editing_ship.CommandCenter = (CommandCenterModule)new_object_game_module.module;
        is_editing = true;

        RefreshModularCenter();
    }
    public void AddExtraPoints(Transform module)
    {
        ScriptableBaseModule module_data = module.GetComponent<GameModule>().module.Data.ExactLevel(module.GetComponent<GameModule>().module.CurrentLevel);
        module_extra_points.Add(new ModuleExtraPoints());
        module_extra_points[module_extra_points.Count - 1].module = module;

        for (int i = 0; i < module_data.JointPoints.Count; i++)
        {
            module_extra_points[module_extra_points.Count - 1].extra_points.Add(new ExtraPoint(i));
            module_extra_points[module_extra_points.Count - 1].extra_points[i].point_object = Instantiate(point_prefab).transform;
            module_extra_points[module_extra_points.Count - 1].extra_points[i].point_object.parent = module_extra_points[module_extra_points.Count - 1].module;
            module_extra_points[module_extra_points.Count - 1].extra_points[i].point_object.GetComponent<SpriteRenderer>().sprite = extra_point_texture;
            module_extra_points[module_extra_points.Count - 1].extra_points[i].point_object.position = module_data.JointPoints[i].pos;
            module_extra_points[module_extra_points.Count - 1].extra_points[i].point_object.position -= new Vector3(0, 0, 0.1f);
        }
        Transform main_point_transform = Instantiate(point_prefab).transform;
        main_point_transform.parent = module;
        main_point_transform.position = module_data.MainJointPoint.pos;
        main_point_transform.position -= new Vector3(0, 0, 0.1f);
        main_point_transform.GetComponent<SpriteRenderer>().sprite = main_point_texture;
    }
    public void RemoveModule(Transform module)
    {
        foreach ((int, Transform, bool) joint_point in all_extra_point)
            if (module == joint_point.Item2)
                all_extra_point.Remove(joint_point);

        Destroy(module);
        UIController.Instance.ModularCenterPanel.CloseRemoveButton();
    }
    public void RemoveModule()
    {
        RemoveModule(active_module);
    }
    public void RefreshModularCenter()
    {
        if (transform.childCount == 0)
        {
            UIController.Instance.ModularCenterPanel.CreateButtonsPanel.BlockAllButtons();
            UIController.Instance.ModularCenterPanel.CreateButtonsPanel.AllowExactButton(0);
        }
        else
            UIController.Instance.ModularCenterPanel.CreateButtonsPanel.AllowAllButtons();


    }
    const int point_radius = 20;
    private void OnGUI()
    {
        //if (!is_editing)
        //    return;

        //if (Event.current.type.Equals(EventType.Repaint))
        //{
        //    Vector2 module_pos;
        //    Vector2 point_pos;
        //    foreach (ModuleExtraPoints module in module_extra_points)
        //    {
        //        module_pos = new Vector2(module.module.position.x, module.module.position.y);
        //        foreach (ExtraPoint point in module.extra_points)
        //        {
        //            point_pos = module.BaseModule.Data.ExactLevel(module.BaseModule.CurrentLevel).JointPoints[point.num_in_parent].pos;

        //            Vector2 result_pos = new Vector2(module_pos.x + point_pos.x, module_pos.y - point_pos.y);

        //            result_pos = Camera.main.WorldToScreenPoint(result_pos);
        //            //result_pos = new Vector2(result_pos.x, result_pos.y);

        //            result_pos -= new Vector2(point_radius, point_radius);

        //            Graphics.DrawTexture(new Rect(
        //            result_pos,
        //            new Vector2(point_radius * 2, point_radius * 2)), extra_point_texture);

        //            point_pos = module.BaseModule.Data.ExactLevel(module.BaseModule.CurrentLevel).MainJointPoint.pos;
        //            result_pos = module_pos + point_pos;
        //            result_pos = Camera.main.WorldToScreenPoint(result_pos);
        //            result_pos = new Vector2(result_pos.x, Screen.width / 2 - result_pos.y);
        //            result_pos -= new Vector2(point_radius, point_radius);

        //            Graphics.DrawTexture(new Rect(
        //            result_pos,
        //            new Vector2(point_radius * 2, point_radius * 2)), main_point_texture);
        //        }
        //    }
        //}
    }
    private void Start()
    {
        StartEditing();
    }
    public void StartEditing()
    {
        editing_ship = Instantiate(GameData.Instance.ShipPrefab).transform;
        editing_ship.parent = this.transform;
        //Ship editing_ship_script = editing_ship.GetComponent<Ship>();
        RefreshModularCenter();
    }
    public void StartEditing(Transform player_ship)
    {
        editing_ship = player_ship;
        editing_ship.parent = this.transform;
    }
}
