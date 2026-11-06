using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class TapNumbers : MonoBehaviour, IClickItem
{
    public string number;
    public CalendarHelper Helper;
    public void tapOnItem()
    {
        Helper.InputNumbers(number);
    }
}
