using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private CodeChecker _checker;
    [SerializeField] private int index;
    private void Start()
    {
        GetComponent<TMP_InputField>().onEndEdit.AddListener(GetTextFromInputField);
    }

    private void GetTextFromInputField(string num)
    {
        //_checker.CheckPass(index, num);
    }
}
