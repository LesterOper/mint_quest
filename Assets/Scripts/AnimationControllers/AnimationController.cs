using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DefaultNamespace;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    [Header("Start menu")]
    [SerializeField] private GameObject btn_start;
    [SerializeField] private GameObject btn_setting;
    [SerializeField] private GameObject btn_levels;
    [SerializeField] private GameObject currentLevel;

    private Vector3 btnStartPos, btnSettingPos, btnLevelsPos;
    [Header("Target objects")]
    [SerializeField] private GameObject btnStartTarget;
    [SerializeField] private GameObject btnSettingsTarget;
    [SerializeField] private GameObject btnLevelsTarget;

    private MintDialogsContainer _dialogsContainer;

    private void Start()
    {
        _dialogsContainer = FindObjectOfType<MintDialogsContainer>();

        
        Messenger.Broadcast(Messages.ANIM_EFFECTS, new Bundle().Set(Messages.OBJECT_IANIMATE, gameObject));
        btnStartPos = btn_start.transform.position;
        btnSettingPos = btn_setting.transform.position;
        btnLevelsPos = btn_levels.transform.position;
    }

    public void StartButtonsAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(btn_start.transform.DOMove(btnStartTarget.transform.position, 0.5f).SetEase(Ease.InSine | Ease.Linear));
        sequence.Append(btn_setting.transform.DOMove(btnSettingsTarget.transform.position, 0.5f)).SetEase(Ease.InSine | Ease.Linear);
        sequence.Join(btn_levels.transform.DOMove(btnLevelsTarget.transform.position, 0.5f)).SetEase(Ease.InSine | Ease.Linear);
        sequence.Join(currentLevel.GetComponent<TextMeshProUGUI>().DOFade(1, 0.7f)).SetEase(Ease.InSine | Ease.Linear);
    }

    public void ShowSettingsWithAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(btn_start.transform.DOMove(btnStartPos, 0.5f).SetEase(Ease.InSine));
        sequence.Append(btn_setting.transform.DOMove(btnSettingPos, 0.5f)).SetEase(Ease.InSine);
        sequence.Join(btn_levels.transform.DOMove(btnLevelsPos, 0.5f)).SetEase(Ease.InSine);
        sequence.Join(currentLevel.GetComponent<TextMeshProUGUI>().DOFade(0, 0.7f)).SetEase(Ease.InSine)
            .OnComplete((() => MintDialogsContainer._instance.ShowSettingsDialog()));
    }
    
    public void ShowSelectLevelsWithAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(btn_start.transform.DOMove(btnStartPos, 0.5f).SetEase(Ease.InSine));
        sequence.Append(btn_setting.transform.DOMove(btnSettingPos, 0.5f)).SetEase(Ease.InSine);
        sequence.Join(btn_levels.transform.DOMove(btnLevelsPos, 0.5f)).SetEase(Ease.InSine);
        sequence.Join(currentLevel.GetComponent<TextMeshProUGUI>().DOFade(0, 0.7f)).SetEase(Ease.InSine)
            .OnComplete((() => MintDialogsContainer._instance.ShowSelectLevelsDialog()));
    }
    
    

    public void LoadGameSceneWithAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(btn_start.transform.DOMove(btnStartPos, 0.5f).SetEase(Ease.InSine));
        sequence.Append(btn_setting.transform.DOMove(btnSettingPos, 0.5f)).SetEase(Ease.InSine);
        sequence.Join(btn_levels.transform.DOMove(btnLevelsPos, 0.5f)).SetEase(Ease.InSine);
        sequence.Join(currentLevel.GetComponent<TextMeshProUGUI>().DOFade(0, 0.7f)).SetEase(Ease.InSine).OnComplete(
            () => { SceneManager.LoadScene("level_scene"); });
    }
}
