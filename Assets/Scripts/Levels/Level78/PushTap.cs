using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class PushTap : MonoBehaviour, IClickItem
{
    [SerializeField] private RingRotateController ring;
    public void tapOnItem()
    {
        ring.TryToPushLight();
    }
}
