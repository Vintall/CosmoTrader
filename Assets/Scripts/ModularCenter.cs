using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularCenter : MonoBehaviour
{
    Ship editing_ship;
    bool is_editing;
    Transform active_module;

    public void BuyModule(string id)
    {
        GameObject new_object = Instantiate(GameData.Instance.ModulePrefab);
        GameModule new_object_game_module = new_object.GetComponent<GameModule>();
        ScriptableModuleLevelsContainer<ScriptableBaseModule> module_data = GameData.Instance.GetModuleByID(id);
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

        new_object_game_module.module.GenerateObject(module_data, 1);
        new_object_game_module.SpriteRenderer.sprite = module_data.ExactLevel(new_object_game_module.module.CurrentLevel).Sprite;

        new_object_game_module.RefreshCollider();
    }

    public void StartEditing()
    {
        
    }
    public void StartEditing(Ship player_ship)
    {
        editing_ship = player_ship;
    }
}
