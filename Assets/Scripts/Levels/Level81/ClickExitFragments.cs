using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickExitFragments : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 down, up;
    public void OnPointerDown(PointerEventData eventData)
    {
        down = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        up = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        if (Vector2.Distance(down, up) <= 1f)
        {
            GetComponent<IClickItem>().tapOnItem();
            enabled = false;
        }
    }
}
