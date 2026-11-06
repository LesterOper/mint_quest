using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class TapCanvasToCheckInputField : MonoBehaviour, IClickItem
{
    [SerializeField] private CalendarHelper _helper;
    public void tapOnItem()
    {
        _helper.CheckInputValue();
    }
}
