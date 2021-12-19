using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeModuleInfoPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI cost_field;
    [SerializeField] TMPro.TextMeshProUGUI durability_field;
    [SerializeField] TMPro.TextMeshProUGUI description_field;
    [SerializeField] TMPro.TextMeshProUGUI level_change_field;

    public void ChangeInfo(ScriptableBaseModule module)
    {
        cost_field.text = module.Cost.ToString();
        durability_field.text = module.Durability.ToString();
        description_field.text = "";
        level_change_field.text = "";
    }
    public void ChangeInfo(string id, int level)
    {
        ScriptableModuleLevelsContainer<ScriptableBaseModule> module = GameData.Instance.GetModuleByID(id);
        if (level == module.LevelsCount)
        {
            level_change_field.text = "MAX";
            cost_field.text = "";
            durability_field.text = "";
            description_field.text = "";
            return;
        }

        cost_field.text = module.ExactLevel(level+1).Cost.ToString();
        durability_field.text = module.ExactLevel(level + 1).Durability.ToString();
        description_field.text = module.ExactLevel(level + 1).Description;

        level_change_field.text = "lvl" + level + " => lvl" + (level + 1);
    }
}
