using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class SwitchTap : MonoBehaviour, IClickItem
{
    [SerializeField] private CameraShootHelper _helper;
    public void tapOnItem()
    {
        _helper.ObjectiveActive();
    }
}
