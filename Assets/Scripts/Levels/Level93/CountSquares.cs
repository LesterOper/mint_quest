using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using TMPro;
using UnityEngine;

public class CountSquares : MonoBehaviour, IClickItem
{
    [SerializeField] private CounterSquares counter;
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private string num;
    private bool isClick;
    public void tapOnItem()
    {
        if (textField.text.Length >= 2) return;

        textField.text += num;
    }
}
