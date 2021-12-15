using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularCenterPanel : MonoBehaviour
{
    [SerializeField] RectTransform add_module_info_panel;
    public RectTransform AddModuleInfoPanel
    {
        get
        {
            return add_module_info_panel;
        }
    }
    public void OpenAddModuleInfoPanel()
    {
        AddModuleInfoPanel.gameObject.SetActive(true);
    }
    public void CloseAddModuleInfoPanel()
    {
        AddModuleInfoPanel.gameObject.SetActive(false);
    }
    public void BuyButtonsCallback(string id)
    {
        switch (id)
        {
            case "1": //Command Center
                
                break; 
            case "2": //Body

                break; 
            case "3": //Engine

                break;
            case "4": //Battery

                break;
            case "5": //Storage

                break;
            case "6": //Cannon

                break;
            case "7": //Harvester

                break;
            case "8": //Convertor

                break;
            case "9": //Generator

                break;
            case "10": //Repairer

                break;
        }
    }
}
