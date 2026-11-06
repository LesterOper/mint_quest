using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;

public class MathButtonsController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private RectTransform tr;
    [SerializeField] private CounterHelper _helper;
    [SerializeField] private TextMeshProUGUI num;
    private Vector2 startPos;

    private void Start()
    {
        startPos = tr.anchoredPosition;
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        _helper.CountExpression(Convert.ToInt32(num.text));
        tr.DOAnchorPos(startPos, 0.2f).SetEase(Ease.Linear);
    }

    public void attachToParentObject()
    {
        
    }
}
