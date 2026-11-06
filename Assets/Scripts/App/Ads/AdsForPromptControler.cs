using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using I2.Loc;
using Library.Common;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdsForPromptControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textForPrompt;
    [SerializeField] private string numPrompt;
    [SerializeField] private RectTransform target;
    [SerializeField] private Sprite activePrompt;
    [SerializeField] private Color activeColor;
    [SerializeField] private AdsForPromptControler next;
    [SerializeField] private Image indicator;
    [SerializeField] private Sprite activeIndicator;
    private string prompt;
    
    private GameController _controller;

    private void OnEnable()
    {
        Messenger.AddListener(Messages.REWARDED_VIDEO_ERNED_AVAILABLE, Shown);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.REWARDED_VIDEO_ERNED_AVAILABLE, Shown);
    }

    private void Awake()
    {
        _controller = FindObjectOfType<GameController>();
        if (PlayerPrefs.GetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt) == 1)
        {
            GetComponent<Button>().interactable = false;
            
            GetComponent<Image>().DOFade(0, 0f)
                .SetEase(Ease.Linear);
            
            text.DOFade(0, 0f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
            {
                textForPrompt.text =numPrompt + ". " +  LocalizationManager.GetTranslation(
                    AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt,
                    true, 0, true,
                    false, null, LocalizationManager.CurrentLanguage);
                
                textForPrompt.DOFade(1, 0f)
                    .SetEase(Ease.Linear);
                
                CheckForNextPrompt();
            });
        }
        else
        {
            text.text = "" +  LocalizationManager.GetTranslation(AppConstans.LEVEL_TINTS, 
                true, 0, true, 
                false, null,
                LocalizationManager.CurrentLanguage);
            
            text.text += " " + numPrompt;
        }
    }

    void Start()
    {
        if (numPrompt == "1")
        {
            if (PlayerPrefs.GetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt) == 0)
            {
                GetComponent<Image>().sprite = activePrompt;
            }
        }
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3");
            if (PlayerPrefs.GetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt) == 0 && IfPreviousAdsWasShown())
            {
                AnalyticsHelper.SendEventUsedClue("Tint " + numPrompt, _controller.NUMBER_OF_LEVEL);
                Context.AdsHelper.PlayVideo(Shown, AdsHelper.VideoAdsPlace.NONE);
            }
        });
    }

    private void Shown()
    {
        AppMetrica.Instance.ReportEvent("REWARDED VIDEO WAS SHOWN");
        Context.InterDetect.ResetCountForInterstitial();
        GetComponent<Image>().DOFade(0, 0.25f)
            .SetEase(Ease.Linear)
            .OnComplete(() => GetComponent<Button>().interactable = false);
        text.DOFade(0, 0.25f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
        {
            prompt =numPrompt + ". " + LocalizationManager.GetTranslation(
                AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt, 
                true, 0, true,
                false, null, LocalizationManager.CurrentLanguage);
                    
            PlayerPrefs.SetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt, 1);
            textForPrompt.text = prompt;
            textForPrompt.DOFade(1, 0.25f)
                .SetEase(Ease.Linear)
                .OnComplete(() => { CheckForNextPrompt(); });
            indicator.sprite = activeIndicator;
        });
    }

    private bool IfPreviousAdsWasShown()
    {
        if (numPrompt == "2")
        {
            return PlayerPrefs.GetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + 1) == 1;
        }
        if (numPrompt == "3")
        {
            return PlayerPrefs.GetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + 2) == 1;
        }
        return true;
    }

    private void CheckForNextPrompt()
    {
        if (PlayerPrefs.GetInt(AppConstans.PREF_TERM_PROMTS + _controller.NUMBER_OF_LEVEL + "_" + numPrompt) == 1)
        {
            if (next != null)
            {
                next.SetNext();
                text.DOFade(0, 0f)
                    .SetEase(Ease.Linear);
            }
            else
            {
                text.DOFade(0, 0)
                    .SetEase(Ease.Linear);
            }
        }
    }

    private void SetNext()
    {
        text.DOColor(activeColor, 0f)
            .SetEase(Ease.Linear);
        GetComponent<Image>().sprite = activePrompt;
        indicator.DOColor(Color.white, 0.15f)
            .SetEase(Ease.Linear);
        indicator.sprite = activeIndicator;
    }
}