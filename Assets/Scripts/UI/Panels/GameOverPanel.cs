using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI game_result;
    [SerializeField] TMPro.TextMeshProUGUI game_result_description;

    public TMPro.TextMeshProUGUI GameResult
    {
        get
        {
            return game_result;
        }
    }
    public TMPro.TextMeshProUGUI GameResultDescription
    {
        get
        {
            return game_result_description;
        }
    }
    public void ChangeStates(bool result, string desctiptions)
    {
        if (result)
            game_result.text = "Победа";
        else
            game_result.text = "Поражение";

        game_result_description.text = desctiptions;
    }
}
