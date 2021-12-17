using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateModuleButton : MonoBehaviour
{
    [SerializeField] string module_id;
    [SerializeField] ModulesCreateButtonsPanel modules_create_buttons_panel;


    public string ModuleId
    {
        get
        {
            return module_id;
        }
    }
    public void ButtonEnter()
    {
        modules_create_buttons_panel.OnCreateButtonEnter(module_id);
    }
    public void ButtonExit()
    {
        modules_create_buttons_panel.OnCreateButtonLeave(module_id);
    }
    public void ButtonClick()
    {
        modules_create_buttons_panel.OnCreateButtonPressed(module_id);
    }
}
