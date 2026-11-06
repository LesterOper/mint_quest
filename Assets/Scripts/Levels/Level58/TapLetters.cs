using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Levels;
using TMPro;
using UnityEngine;

public class TapLetters : MonoBehaviour, IClickItem
{
    [SerializeField] private LetterHelper _letterHelper;
    private TextMeshProUGUI letter;

    private void Start()
    {
        letter = GetComponent<TextMeshProUGUI>();
    }

    public void tapOnItem()
    {
        Debug.Log("Tap");
        if (_letterHelper.CheckCorrect(letter))
        {
            letter.DOFade(0f, 0.25f).SetEase(Ease.Linear).OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}
