using System.Collections;
using System.Collections.Generic;
using Levels;
using TMPro;
using UnityEngine;

public class EntererCode : MonoBehaviour, IClickItem
{
    [SerializeField] private string num;
    [SerializeField] private CodeChecker _checker;
    public void tapOnItem()
    {
        _checker.CheckPass(num);
    }
}
