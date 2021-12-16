using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyEnergyPanel : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] TMPro.TextMeshProUGUI max_power_capacity;
    [SerializeField] TMPro.TextMeshProUGUI power_have;
    [SerializeField] TMPro.TextMeshProUGUI money_have;
    [SerializeField] RectTransform bar;
    [SerializeField] RectTransform movable_bar;
    [SerializeField] List<UnityEngine.UI.Button> offer_button;
    #endregion

    #region Properties
    public int PowerMaxCapacity
    {
        set
        {
            max_power_capacity.text = value.ToString() + " МВт";
        }
    }
    public int PowerHave
    {
        set
        {
            power_have.text = value.ToString() + " МВт";
        }
    }
    public float MoneyHave
    {
        set
        {
            money_have.text = value.ToString();
        }
    }
    #endregion

    public void ChangeStates(int power_have, int max_capacity, float money)
    {
        PowerMaxCapacity = max_capacity;
        PowerHave = power_have;
        MoneyHave = money;
        movable_bar.offsetMax = new Vector2(bar.rect.width * ((float)power_have / max_capacity) - bar.rect.width, 0);
    }
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel()
    {
        gameObject.SetActive(true);
    }
}
