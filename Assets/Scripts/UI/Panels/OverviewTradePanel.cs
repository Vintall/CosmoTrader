using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewTradePanel : MonoBehaviour
{
    [SerializeField] Transform savezone_icon;
    [SerializeField] Transform shop_button;
    [SerializeField] Transform convertor_button;
    [SerializeField] Transform modular_center_button;

    #region ButtonActivity
    public void ShowSavezoneIcon()
    {
        savezone_icon.gameObject.SetActive(true);
    }
    public void HideSavezoneIcon()
    {
        savezone_icon.gameObject.SetActive(false);
    }
    public void ShowShopButton()
    {
        shop_button.gameObject.SetActive(true);
    }
    public void HideShopButton()
    {
        shop_button.gameObject.SetActive(false);
    }
    public void ShowConvertorButton()
    {
        convertor_button.gameObject.SetActive(true);
    }
    public void HideConvertorButton()
    {
        convertor_button.gameObject.SetActive(false);
    }
    public void ShowModularCenterButton()
    {
        modular_center_button.gameObject.SetActive(true);
    }
    public void HideModularCenterButton()
    {
        modular_center_button.gameObject.SetActive(false);
    }
    #endregion

    #region Callbacks
    public void OnShopButtonClicked()
    {
        GameController.Instance.Map.Player.GoToShop();
    }
    public void OnConvertorButtonClicked()
    {
        UIController.Instance.SetConvertorPanel();
    }
    public void OnModularCenterButtonClicked()
    {
        GameController.Instance.Map.Player.GoToModularCenter();
    }
    #endregion
}
