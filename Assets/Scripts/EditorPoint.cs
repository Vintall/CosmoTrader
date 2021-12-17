using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPoint : MonoBehaviour
{
    [SerializeField] Sprite main_point;
    [SerializeField] Sprite extra_point;

    public void ChooseMainPoint()
    {
        GetComponent<SpriteRenderer>().sprite = main_point;
    }
    public void ChooseExtraPoint()
    {
        GetComponent<SpriteRenderer>().sprite = extra_point;
    }
}
