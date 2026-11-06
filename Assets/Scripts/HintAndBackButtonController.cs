using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using UnityEngine;
using UnityEngine.UI;

public class HintAndBackButtonController : MonoBehaviour
{
    [Header("Back button")]
    [SerializeField] private Image back;
    [SerializeField] private Sprite blackBack;
    [SerializeField] private Sprite mintBack;
    [Space]
    [Header("Hint button")]
    [SerializeField] private Image hint;
    [SerializeField] private Sprite blackHint;
    [SerializeField] private Sprite mintHint;

    private void OnEnable()
    {
        Messenger.AddListener(Messages.BACK_HINT_TO_MINT, SetBackAndHintToMintBackground);
        Messenger.AddListener(Messages.BACK_HINT_TO_BLACK, SetBackAndHintToBlackBackground);
        Messenger.AddListener(Messages.BACK_TO_MINT_HINT_DEACTIVATE, SetBackToMintBackgroundAndHintDeActivate);
    }

    private void SetBackAndHintToMintBackground()
    {
        back.gameObject.SetActive(true);
        hint.gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(back.DOFade(0, 0.2f).OnComplete(() =>
        {
            back.sprite = mintBack;
            back.DOFade(1, 0.2f);
        }));
        sequence.Join(hint.DOFade(0, 0.2f).OnComplete(() =>
        {
            hint.sprite = mintHint;
            hint.DOFade(1, 0.2f);
        }));

    }
    
    private void SetBackAndHintToBlackBackground()
    {
        back.gameObject.SetActive(true);
        hint.gameObject.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(back.DOFade(0, 0.1f).OnComplete(() =>
        {
            back.sprite = blackBack;
            back.DOFade(1, 0.3f);
        }));
        sequence.Join(hint.DOFade(0, 0.1f).OnComplete(() =>
        {
            hint.sprite = blackHint;
            hint.DOFade(1, 0.3f);
        }));
       
    }

    private void SetBackToMintBackgroundAndHintDeActivate()
    {
        back.gameObject.SetActive(true);
        hint.DOFade(0, 0.2f).OnComplete(() =>
        {
            hint.gameObject.SetActive(false);
        });
        back.DOFade(0, 0.2f).OnComplete(() =>
        {
            back.sprite = mintBack;
            back.DOFade(1, 0.2f);
        });
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.BACK_HINT_TO_MINT, SetBackAndHintToMintBackground);
        Messenger.RemoveListener(Messages.BACK_HINT_TO_BLACK, SetBackAndHintToBlackBackground);
        Messenger.RemoveListener(Messages.BACK_TO_MINT_HINT_DEACTIVATE, SetBackToMintBackgroundAndHintDeActivate);
    }
}
