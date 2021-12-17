using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularCenter : MonoBehaviour
{
    Transform editing_ship;
    Transform active_module;
    bool is_editing;

    public List<Transform> all_modules = new List<Transform>();

    public void BuyModule(string id)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> module_data = GameData.Instance.GetModuleByID(id);

        GameObject new_object = Instantiate(GameData.Instance.ModulePrefab);
        GameModule game_module = new_object.GetComponent<GameModule>();
        ModuleEditorElement module_editor_element = new_object.AddComponent<ModuleEditorElement>();

        switch (module_data.ExactLevel(1).Type)
        {
            case Structs.ModuleType.CommandCenter:
                game_module.module = new_object.AddComponent<CommandCenterModule>();
                break;
            case Structs.ModuleType.Body:
                game_module.module = new_object.AddComponent<BodyModule>();
                break;
            case Structs.ModuleType.Engine:
                game_module.module = new_object.AddComponent<EngineModule>();
                break;
            case Structs.ModuleType.Battery:
                game_module.module = new_object.AddComponent<BatteryModule>();
                break;
            case Structs.ModuleType.Storage:
                game_module.module = new_object.AddComponent<StorageModule>();
                break;
            case Structs.ModuleType.Cannon:
                game_module.module = new_object.AddComponent<CannonModule>();
                break;
            case Structs.ModuleType.Harvester:
                game_module.module = new_object.AddComponent<HarvesterModule>();
                break;
            case Structs.ModuleType.Convertor:
                game_module.module = new_object.AddComponent<ConvertorModule>();
                break;
            case Structs.ModuleType.Generator:
                game_module.module = new_object.AddComponent<GeneratorModule>();
                break;
            case Structs.ModuleType.Repairer:
                game_module.module = new_object.AddComponent<RepairerModule>();
                break;
        }

        new_object.transform.parent = transform;
        game_module.module.GenerateObject(module_data, 1);
        game_module.SpriteRenderer.sprite = module_data.ExactLevel(game_module.module.CurrentLevel).Sprite;

        module_editor_element.AddExtraPoints();
        all_modules.Add(new_object.transform);


        game_module.RefreshCollider();
        
        //is_editing = true;

        RefreshModularCenter();
    }
    
    public void RemoveModule(Transform module)
    {
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
