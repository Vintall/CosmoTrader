using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesCreateButtonsPanel : MonoBehaviour
{
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
