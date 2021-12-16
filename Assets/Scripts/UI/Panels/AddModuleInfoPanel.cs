using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddModuleInfoPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI cost_field;
    [SerializeField] TMPro.TextMeshProUGUI durability_field;
    [SerializeField] TMPro.TextMeshProUGUI description_field;
    [SerializeField] TMPro.TextMeshProUGUI building_block_field;

    public void ChangeInfo(ScriptableBaseModule module)
    {
        cost_field.text = module.Cost.ToString();
        durability_field.text = module.Durability.ToString();
        description_field.text = "TEST \n TEST";
        building_block_field.text = "";
        foreach (Structs.ModuleType type in module.BuildingBlockedObjects)
            building_block_field.text += "\n" + type.ToString();
    }
    public void ChangeInfo(string id, int level)
    {
        ScriptableBaseModule module = GameData.Instance.GetModuleByID(id).ExactLevel(level);
        cost_field.text = module.Cost.ToString();
        durability_field.text = module.Durability.ToString();
        description_field.text = module.Description;
        building_block_field.text = "";
        foreach (Structs.ModuleType type in module.BuildingBlockedObjects)
            building_block_field.text += type.ToString() + "\n";
    }
}
