using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularCenterPanel : MonoBehaviour
{
    [SerializeField] AddModuleInfoPanel add_module_info_panel;
    [SerializeField] UpgradeModuleInfoPanel upgrade_module_info_panel;
    [SerializeField] ModulesCreateButtonsPanel create_buttons_panel;
    [SerializeField] Transform update_button;
    [SerializeField] Transform remove_button;
    [SerializeField] Transform exit_button;
    [SerializeField] TMPro.TextMeshProUGUI balance;

    public TMPro.TextMeshProUGUI Balance
    {
        get
        {
            return balance;
        }
    }
    public UpgradeModuleInfoPanel UpgradeModuleInfoPanel
    {
        get
        {
            return upgrade_module_info_panel;
        }
    }
    public AddModuleInfoPanel AddModuleInfoPanel
    {
        get
        {
            return add_module_info_panel;
        }
    }
    public ModulesCreateButtonsPanel CreateButtonsPanel
    {
        get
        {
            return create_buttons_panel;
        }
    }
    public Transform ExitButton
    {
        get
        {
            return exit_button;
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

    #region ButtonsCallbacks

    public void RemoveButtonPressed()
    {
        GameController.Instance.Map.Savezone.ModularCenter.RemoveSelectedModule();
    }
    public void UpdateButtonPressed()
    {
        GameController.Instance.Map.Savezone.ModularCenter.UpdateModuleLevel();
    }
    public void ShowUpdateButton()
    {
        update_button.gameObject.SetActive(true);
    }
    public void HideUpdateButton()
    {
        update_button.gameObject.SetActive(false);
    }
    public void CloseModularCenterPanelButton()
    {
        this.gameObject.SetActive(false);
        GameController.Instance.Map.Savezone.ModularCenter.EndEditing();
    }
    public void OpenRemoveButton()
    {
        remove_button.gameObject.SetActive(true);
    }
    public void CloseRemoveButton()
    {
        remove_button.gameObject.SetActive(false);
    }
    public void BuyButtonsPressed(string id)
    {
        GameController.Instance.Map.Savezone.ModularCenter.BuyModule(id);
    }
    public void BuyButtonsEntered(string id)
    {
        add_module_info_panel.ChangeInfo(id, 1);
        OpenAddModuleInfoPanel();
    }
    public void BuyButtonsLeaved(string id)
    {
        CloseAddModuleInfoPanel();
    }
    #endregion
}
