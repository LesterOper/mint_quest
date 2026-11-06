using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class TapCircle : MonoBehaviour, IClickItem
{
    [SerializeField] private CodeLockController _controller;
    public void tapOnItem()
    {
        _controller.CheckOrder(this);
    }
}
