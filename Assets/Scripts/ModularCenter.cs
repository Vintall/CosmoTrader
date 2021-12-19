using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularCenter : MonoBehaviour
{
    Transform editing_ship;
    Transform active_module;
    bool is_module_selected;
    Transform player_on_editor;
    bool is_editing;

    public List<Transform> all_modules = new List<Transform>();
    
    public Transform EditingShip
    {
        get
        {
            return editing_ship;
        }
    }
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

        new_object.transform.parent = editing_ship;
        game_module.module.GenerateObject(module_data, 1);
        game_module.SpriteRenderer.sprite = module_data.ExactLevel(game_module.module.CurrentLevel).Sprite;

        module_editor_element.AddExtraPoints();
        all_modules.Add(new_object.transform);


        game_module.RefreshCollider();
        
        //is_editing = true;

        RefreshModularCenter();
    }
    public void SelectModule(Transform module)
    {
        active_module = module;
        is_module_selected = true;
        UIController.Instance.ModularCenterPanel.OpenRemoveButton();

    }
    public void DeselectModule()
    {
        is_module_selected = false;
        UIController.Instance.ModularCenterPanel.CloseRemoveButton();
    }

    public void RemoveSelectedModule()
    {
        if (!is_module_selected)
            return;

        if (active_module.parent != this.transform)
            return;

        Destroy(active_module.gameObject);
        all_modules.Remove(active_module);
        is_module_selected = false;
        UIController.Instance.ModularCenterPanel.CloseRemoveButton();
    }
    public void RefreshModularCenter()
    {
        if (editing_ship.childCount == 0)
        {
            UIController.Instance.ModularCenterPanel.CreateButtonsPanel.BlockAllButtons();
            UIController.Instance.ModularCenterPanel.CreateButtonsPanel.AllowExactButton(0);
        }
        else
            UIController.Instance.ModularCenterPanel.CreateButtonsPanel.AllowAllButtons();

    }
    public void UpdateModuleLevel()
    {
        foreach(ModuleEditorElement.Point point in active_module.GetComponent<ModuleEditorElement>().extra_points)
        {
            if (point.is_used)
                return;
        }
        active_module.GetComponent<GameModule>().module.UpdateModule();
        active_module.GetComponent<ModuleEditorElement>().AddExtraPoints();
        active_module.GetComponent<GameModule>().SpriteRenderer.sprite =
             active_module.GetComponent<GameModule>().module.Data.ExactLevel(active_module.GetComponent<GameModule>().module.CurrentLevel).Sprite;
    }
    private void Start()
    {
        //StartEditing();
    }
    //public void StartEditing()
    //{
    //    player_on_editor = GameController.Instance.Map.Player.transform;
    //    editing_ship = Instantiate(GameData.Instance.ShipPrefab).transform;
    //    editing_ship.parent = this.transform;
    //    RefreshModularCenter();
    //    is_editing = true;
    //}
    public void StartEditing(Transform player, Transform player_ship)
    {
        player_ship.parent = this.transform;
        editing_ship = player_ship;
        player_on_editor = player;
        is_editing = true;
        if (editing_ship.childCount > 0)
        {
            List<Transform> modules = new List<Transform>();

            modules.Add(editing_ship.GetChild(0));
            for (int i = 0; i < editing_ship.GetChild(0).childCount; i++)
            {
                Transform body = editing_ship.GetChild(0).GetChild(i);
                modules.Add(body);

                for (int j = 0; j < body.childCount; j++)
                {
                    modules.Add(body.GetChild(j));
                }
            }
            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].transform.parent = player_ship;
                modules[i].rotation = Quaternion.Euler(0, 0, 0);
                modules[i].gameObject.AddComponent<ModuleEditorElement>().AddExtraPoints();
                modules[i].gameObject.SetActive(true);
            }
        }
    }
    public void EndEditing()
    {
        for (int j = 0; j < all_modules.Count; j++) 
        {
            for (int i = 0; i < all_modules[j].GetComponent<ModuleEditorElement>().extra_points.Count; i++)
                all_modules[j].GetComponent<ModuleEditorElement>().extra_points[i].DestroyPointObject();

            all_modules[j].GetComponent<ModuleEditorElement>().main_point.DestroyPointObject();
            all_modules[j].gameObject.SetActive(false);
            Destroy(all_modules[j].GetComponent<ModuleEditorElement>());
        }
        player_on_editor.GetComponent<Player>().ship = editing_ship;
        player_on_editor.GetComponent<Player>().ship.parent = player_on_editor.transform;
        player_on_editor.GetComponent<Player>().ship.GetComponent<Ship>().CommandCenter = 
            (CommandCenterModule)player_on_editor.GetComponent<Player>().ship.GetChild(0).GetComponent<GameModule>().module;
        

        StartCoroutine(Crutch());
    }
    IEnumerator Crutch() //Не успевает удалять объекты? Наверное они в стеке после метода
    {
        yield return new WaitForSeconds(0.1f);
        player_on_editor.GetComponent<Player>().ship.GetComponent<Ship>().CommandCenter.SetAllChilds();
        player_on_editor.GetComponent<Player>().ship.GetComponent<Ship>().GetModulesStates();

        is_editing = false;
        player_on_editor = null;
        editing_ship = null;
        UIController.Instance.SetOverviewPanel();
        GameController.Instance.Map.Player.UpdateUIStates();
    }
}
