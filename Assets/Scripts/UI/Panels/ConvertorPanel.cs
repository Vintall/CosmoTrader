using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertorPanel : MonoBehaviour
{
    [SerializeField] Transform exit_button;
    [SerializeField] Transform confirm_exchange_button;
    [SerializeField] TMPro.TextMeshProUGUI exchange_description; 

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
    public void OpenPanel(int ore_count)
    {
        gameObject.SetActive(true);
        exchange_description.text = "Обмен " + ore_count +" руды на 1 МВт";
    }
    public void ExchangePressed()
    {
        GameController.Instance.Map.Player.ship.GetComponent<Ship>().ExchangeOreToEnergy();
    }
}
