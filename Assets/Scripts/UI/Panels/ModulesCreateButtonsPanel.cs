using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModulesCreateButtonsPanel : MonoBehaviour
{
    [SerializeField] List<Button> buttons;


    public void AllowAllButtons()
    {
        foreach (Button button in buttons)
            button.interactable = true;
    }
    public void BlockAllButtons()
    {
        foreach (Button button in buttons)
            button.interactable = false;
    }
    public void AllowExactButton(int num)
    {
        buttons[num].interactable = true;
    }
    public void AllowExactButton(string id)
    {
        foreach (Button button in buttons)
        {
            if (button.GetComponent<CreateModuleButton>().ModuleId == id)
                button.interactable = true;
        }
    }
    public void BlockExactButton(int num)
    {
        buttons[num].interactable = false;
    }
    public void BlockExactButton(string id)
    {
        foreach (Button button in buttons)
        {
            if (button.GetComponent<CreateModuleButton>().ModuleId == id)
                button.interactable = false;
        }
    }
    public void OnCreateButtonPressed(string id)
    {
        UIController.Instance.ModularCenterPanel.BuyButtonsPressed(id);
    }
    public void OnCreateButtonEnter(string id)
    {
        UIController.Instance.ModularCenterPanel.BuyButtonsEntered(id);
    }
    public void OnCreateButtonLeave(string id)
    {
        UIController.Instance.ModularCenterPanel.BuyButtonsLeaved(id);
    }
}
