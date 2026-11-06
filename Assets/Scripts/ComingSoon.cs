using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ComingSoon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comingSoon;
    
    private void Start()
    {
        GameObject.Find("TintBtn_Black").SetActive(false);
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(comingSoon.DOFade(1.2f, 0.75f).SetEase(Ease.Linear));
        sequence.Join(comingSoon.transform.DOScale(1, 0.75f).SetEase(Ease.Linear));
        sequence.Append(comingSoon.DOFade(0.75f, 0.75f).SetEase(Ease.Linear));
        sequence.Join(comingSoon.transform.DOScale(0.75f, 0.75f).SetEase(Ease.Linear));
        sequence.SetLoops(-1, LoopType.Restart);
    }
    
    
}
