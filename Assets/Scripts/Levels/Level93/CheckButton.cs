using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class CheckButton : MonoBehaviour, IClickItem
{
    [SerializeField] private CounterSquares _squares;
    public void tapOnItem()
    {
        Invoke(nameof(InvokeMethod), 0.25f);
    }

    private void InvokeMethod()
    {
        _squares.Count();
    }
}
