using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using UnityEngine;

public class ResetCounterSquares : MonoBehaviour, IClickItem
{
    [SerializeField] private CounterSquares counter;
    private bool isClick;
    public void tapOnItem()
    {
        counter.ResetCounter();
    }
}
