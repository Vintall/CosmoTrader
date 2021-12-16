using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleEditorElement : MonoBehaviour
{
    bool pressed = false;
    private void OnMouseDrag()
    {
        pressed = true;
        Debug.Log("OnDrag");
    }
    private void OnMouseUp()
    {
        pressed=false;
        Debug.Log("OnUP");
    }
    private void Update()
    {
        if (pressed)
        {
            gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
