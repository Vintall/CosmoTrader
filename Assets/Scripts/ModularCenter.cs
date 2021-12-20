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
    public List<Structs.ModuleType> ship_requirments = new List<Structs.ModuleType>
    {
        Structs.ModuleType.CommandCenter,
        Structs.ModuleType.Battery,
        Structs.ModuleType.Storage,
        Structs.ModuleType.Cannon,
        Structs.ModuleType.Harvester,
        Structs.ModuleType.Engine,
        Structs.ModuleType.Body
    };
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

        if (module_data.ExactLevel(1).Cost > editing_ship.GetComponent<Ship>().money)
            return;

        editing_ship.GetComponent<Ship>().money -= module_data.ExactLevel(1).Cost;

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

        UIController.Instance.ModularCenterPanel.Balance.text = editing_ship.GetComponent<Ship>().money.ToString();
        //is_editing = true;

        CheckBasicRequirments();
        RefreshModularCenter();
    }
    public void CheckBasicRequirments()
    {
        bool result = true;
        int body_count = 0;
        int engine_count = 0;
        List<Structs.ModuleType> unused_modules = ship_requirments;

        for (int i = 0; i < all_modules.Count; i++)
        {
            if (all_modules[i].GetComponent<GameModule>().module.Data.ExactLevel(1).Type == Structs.ModuleType.Body)
                body_count++;

            if (all_modules[i].GetComponent<GameModule>().module.Data.ExactLevel(1).Type == Structs.ModuleType.Engine)
                engine_count++;

            for (int j = 0; j < unused_modules.Count; j++)
                if (all_modules[i].GetComponent<GameModule>().module.Data.ExactLevel(all_modules[i].GetComponent<GameModule>().module.CurrentLevel).Type == unused_modules[j])
                {
                    unused_modules.RemoveAt(j);
                    break;
                }
        }


        if (unused_modules.Count == 0 && editing_ship.childCount == 1 && (body_count / (float)engine_count) <= 2) 
            AllowEndEditing();
        else
            BlockEndEditing();
        
    }
    void AllowEndEditing()
    {
        UIController.Instance.ModularCenterPanel.ExitButton.gameObject.SetActive(true);
    }
    void BlockEndEditing()
    {
        UIController.Instance.ModularCenterPanel.ExitButton.gameObject.SetActive(false);
    }
    public void SelectModule(Transform module)
    {
        active_module = module;
        is_module_selected = true;
        UIController.Instance.ModularCenterPanel.OpenRemoveButton();
        CheckBasicRequirments();

    }
    public void DeselectModule()
    {
        is_module_selected = false;
        UIController.Instance.ModularCenterPanel.CloseRemoveButton();
        CheckBasicRequirments();
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
        CheckBasicRequirments();
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
    public void FixedUpdate()
    {
        if (is_editing)
        {
            if (is_module_selected)
            {
                UIController.Instance.ModularCenterPanel.UpgradeModuleInfoPanel.ChangeInfo(
                    active_module.GetComponent<GameModule>().module.Data.ID,
                    active_module.GetComponent<GameModule>().module.CurrentLevel);
            }
            CheckBasicRequirments();
        }

    }
    public void UpdateModuleLevel()
    {

        foreach(ModuleEditorElement.Point point in active_module.GetComponent<ModuleEditorElement>().extra_points)
            if (point.is_used)
                return;
        BaseModule module_data = active_module.GetComponent<GameModule>().module;

        if (module_data.CurrentLevel == module_data.Data.LevelsCount)
            return;

        if (module_data.Data.ExactLevel(module_data.CurrentLevel + 1).Cost > editing_ship.GetComponent<Ship>().money)
            return;

        editing_ship.GetComponent<Ship>().money -= module_data.Data.ExactLevel(module_data.CurrentLevel + 1).Cost;

        module_data.UpdateModule();
        active_module.GetComponent<ModuleEditorElement>().AddExtraPoints();
        active_module.GetComponent<GameModule>().SpriteRenderer.sprite =
             module_data.Data.ExactLevel(module_data.CurrentLevel).Sprite;

        UIController.Instance.ModularCenterPanel.Balance.text = editing_ship.GetComponent<Ship>().money.ToString();
        CheckBasicRequirments();
    }
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
        CheckBasicRequirments();
    }
    public void EndEditing()
    {
        player_on_editor.GetComponent<Player>().is_have_convertor = false;
        UIController.Instance.OverviewPanel.TradePanel.HideConvertorButton();
        for (int j = 0; j < all_modules.Count; j++) 
        {
            for (int i = 0; i < all_modules[j].GetComponent<ModuleEditorElement>().extra_points.Count; i++)
                all_modules[j].GetComponent<ModuleEditorElement>().extra_points[i].DestroyPointObject();

            if (all_modules[j].GetComponent<GameModule>().module.Data.ExactLevel(1).Type == Structs.ModuleType.Convertor)
            {
                player_on_editor.GetComponent<Player>().is_have_convertor = true;
                UIController.Instance.OverviewPanel.TradePanel.ShowConvertorButton();
            }

            all_modules[j].GetComponent<ModuleEditorElement>().main_point.DestroyPointObject();
            all_modules[j].gameObject.SetActive(false);
            Destroy(all_modules[j].GetComponent<ModuleEditorElement>());
        }
        editing_ship.GetComponent<Ship>().RepairersStart();
        player_on_editor.GetComponent<Player>().is_around_savezone = true;

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
        player_on_editor.GetComponent<Player>().Ship.harvesters_left_to_take = player_on_editor.GetComponent<Player>().Ship.harvesters_takes_ore_per_trip;

        is_editing = false;
        player_on_editor = null;
        editing_ship = null;
        UIController.Instance.SetOverviewPanel();
        GameController.Instance.Map.Player.UpdateUIStates();
    }
}
