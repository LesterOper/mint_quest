using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class RefreshButtonController : MonoBehaviour, IClickItem
{
    [SerializeField] private CoinsCollector _collector;
    public void tapOnItem()
    {
        _collector.RefreshCoins();
    }
}
