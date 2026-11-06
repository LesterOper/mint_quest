using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class ButtonsOrderClick : MonoBehaviour, IClickItem
{
    [SerializeField] private ButtonsOrderHelper _helper;
    public void tapOnItem()
    {
        _helper.CheckOrder(gameObject);
    }
}
