using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    static GameData instance;
    public static GameData Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] List<ScriptableCommandCenterContainer> command_center_list;
    [SerializeField] List<ScriptableBodyContainer> body_list;
    [SerializeField] List<ScriptableEngineContainer> engine_list;
    [SerializeField] List<ScriptableBatteryContainer> battery_list;
    [SerializeField] List<ScriptableStorageContainer> storage_list;
    [SerializeField] List<ScriptableCannonContainer> cannon_list;
    [SerializeField] List<ScriptableHarvesterContainer> harvester_list;
    [SerializeField] List<ScriptableConvertorContainer> convertor_list;
    [SerializeField] List<ScriptableGeneratorContainer> generator_list;
    [SerializeField] List<ScriptableRepairerContainer> repairer_list;
    [SerializeField] GameObject module_prefab;
    [SerializeField] GameObject ship_prefab;
    [SerializeField] GameObject editor_point_prefab;

    List<ScriptableModuleLevelsContainer<ScriptableBaseModule>> modules;

    public GameObject EditorPointPrefab
    {
        get
        {
            return editor_point_prefab;
        }
    }
    public GameObject ModulePrefab
    {
        get
        {
            return module_prefab;
        }
    }
    public GameObject ShipPrefab
    {
        get
        {
            return ship_prefab;
        }
    }
    public List<ScriptableModuleLevelsContainer<ScriptableBaseModule>> AllModules
    {
        get
        {
            return modules;
        }
    }
    public ScriptableModuleLevelsContainer<ScriptableBaseModule> GetModuleByID(string id)
    {
        foreach(ScriptableModuleLevelsContainer<ScriptableBaseModule> module in modules)
            if(module.ID == id)
                return module;

        return null;
    }
    private void Awake()
    {
        instance = this;

        modules = new List<ScriptableModuleLevelsContainer<ScriptableBaseModule>>();
        //ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        //List<ScriptableBaseModule> levels;

        //foreach (ScriptableCommandCenterContainer container in command_center_list)
        //{
        //    buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

        //    levels = new List<ScriptableBaseModule>();
        //    foreach (ScriptableCommandCenter obj in container.AllLevels)
        //        levels.Add(obj);

        //    buff.SetContainer(container.ID, levels);
        //    modules.Add(buff);
        //}

        FillModulesList(command_center_list);
        FillModulesList(body_list);
        FillModulesList(engine_list);
        FillModulesList(battery_list);
        FillModulesList(storage_list);
        FillModulesList(cannon_list);
        FillModulesList(harvester_list);
        FillModulesList(convertor_list);
        FillModulesList(generator_list);
        FillModulesList(repairer_list);

        //FillModulesList(command_center_list[0].AllLevels, command_center_list[0].ID);


        //Debug.Log("GameData => AllModules");
        //foreach (ScriptableModuleLevelsContainer<ScriptableBaseModule> module in modules)
        //{
        //    Debug.Log("ID: " + module.ID + "\n                    Type: " + module.ExactLevel(1).Type);
        //}
    }
    //void FillModulesList(List<ScriptableBaseModule> list, string id)
    //{
    //    ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();


    //    buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

    //    buff.SetContainer(id, list);
    //    modules.Add(buff);
    //}
    void FillModulesList(List<ScriptableCommandCenterContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableCommandCenterContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableCommandCenter obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableBodyContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableBodyContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableBody obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableEngineContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableEngineContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableEngine obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableBatteryContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableBatteryContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableBattery obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableStorageContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableStorageContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableStorage obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableCannonContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableCannonContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableCannon obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableHarvesterContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableHarvesterContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableHarvester obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableConvertorContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableConvertorContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableConvertor obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableGeneratorContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableGeneratorContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableGenerator obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
    void FillModulesList(List<ScriptableRepairerContainer> list)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();
        List<ScriptableBaseModule> levels;

        foreach (ScriptableRepairerContainer container in list)
        {
            buff = new ScriptableModuleLevelsContainer<ScriptableBaseModule>();

            levels = new List<ScriptableBaseModule>();
            foreach (ScriptableRepairer obj in container.AllLevels)
                levels.Add(obj);

            buff.SetContainer(container.ID, levels);
            modules.Add(buff);
        }
    }
}