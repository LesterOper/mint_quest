using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using App;
using App.Global;
using DefaultNamespace;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using Swimmer.Library.Data;
using Swimmer.Library.Dialogs;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Image _mintBackground;
    [SerializeField] private Button btn_back;
    [SerializeField] private Button tint;
    [Space]
    [SerializeField] private Transform _levelParent;
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject _coolButtonOnScene;
    [SerializeField] private GameObject _buttonGameObject;
    [SerializeField] private TintsHelper tintsDialog;
    public RectTransform dialogParent;
    private ContextManager _poolObject;
    private GameObject currentLevel;

    public GameObject CurrentLevel => currentLevel;

    private int _numOfLevel;

    public int NUMBER_OF_LEVEL => _numOfLevel;

    private void OnEnable()
    {
        Messenger.AddListener(Messages.COOL_EFFECT_COMPLETE_LEVEL, CoolButtonLevelCompleteEffect);
        Messenger.AddListener(Messages.COOL_EFFECT_LOAD_NEXT_LEVEL, CoolButtonClickAndLoadNextLevel);
        Messenger.AddListener(Messages.ACTIVATE_COOL_BUTTOM, ActivateCoolButton);
        Messenger.AddListener(Messages.DEACT_BTN_BACK, ActivateBackBtn);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.COOL_EFFECT_COMPLETE_LEVEL, CoolButtonLevelCompleteEffect);
        Messenger.RemoveListener(Messages.COOL_EFFECT_LOAD_NEXT_LEVEL, CoolButtonClickAndLoadNextLevel);
        Messenger.RemoveListener(Messages.ACTIVATE_COOL_BUTTOM, ActivateCoolButton);
        Messenger.RemoveListener(Messages.DEACT_BTN_BACK, ActivateBackBtn);
    }
    
    private void Start()
    {
        CheckScreenForCoolButton();
        _poolObject = FindObjectOfType<ContextManager>();
        
        CreateLevel();

        tint.GetComponent<Image>().DOFade(1, 1f).SetEase(Ease.Linear);
        btn_back.GetComponent<Image>().DOFade(1, 1f).SetEase(Ease.Linear);

        btn_back.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            SceneManager.LoadScene("main_menu");
        });
        tint.onClick.AddListener(() =>
        {
            Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            btn_back.interactable = false;
            ActivateOrNotPromptButton(false);
            MintDialogsContainer._instance.SetTintsPrefab(tintsDialog);
            MintDialogsContainer._instance.SetParent(dialogParent);
            MintDialogsContainer._instance.ShowTintsDialog();
        });
        //Debug.Log("Last open:  " + LevelUtils.GetLastOpenLevel());
    }
    
    public void LoadNextLevel()
    {
        back.SetActive(true);
        LevelUtils.AppendLevelnumber();
        currentLevel.GetComponent<Destroyer>().ClearLevel();
        CreateLevel();
        ActivateOrNotPromptButton(true);
    }

    public void CoolButtonLevelCompleteEffect()
    {
        ActivateOrNotPromptButton(false);
        Context.SoundPlayer.PlaySoundClick("level_finish", AudioMixerController.MasterMixerGroup.SFX, 1);
        /*AnalyticsHelper.SendEventLevelPassed(_numOfLevel);
        if (_numOfLevel % 5 == 0)
        {
            FacebookAnalyticsHelper.SendEventLevelEachFivePassedLevels(_numOfLevel);
        }*/
        var th = FindObjectOfType<TintsHelper>();
        if (th != null)
        {
            th.ForceHide();
            btn_back.interactable = true;
        }

        Sequence anim = DOTween.Sequence();
        anim.Join(_mintBackground.GetComponent<Image>().DOFade(1, 0.6f)).InsertCallback(0.6f, () =>
        {
            SetCameraAndBackgroundParameters();
            _coolButtonOnScene.SetActive(true);
        });
    }

    public void ActivateCoolButton()
    {
        ActivateOrNotPromptButton(false);
        Context.SoundPlayer.PlaySoundClick("level_finish", AudioMixerController.MasterMixerGroup.SFX, 1);
        var th = FindObjectOfType<TintsHelper>();
        if (th != null)
        {
            th.ForceHide();
            btn_back.interactable = true;
        }
        SetCameraAndBackgroundParameters();
        _coolButtonOnScene.SetActive(true);
    }
    
    public void CoolButtonClickAndLoadNextLevel()
    {
        AnalyticsHelper.SendEventLevelPassed(_numOfLevel);
        if (_numOfLevel % 5 == 0)
        {
            FacebookAnalyticsHelper.SendEventLevelEachFivePassedLevels(_numOfLevel);
        }
        LoadNextLevel();
        _mintBackground.GetComponent<Image>().DOFade(0, 0.6f);
    }

    public void RestartLevel()
    {
        Destroy(currentLevel);
        Invoke(nameof(RestartDelayed), 0.5f);
    }

    private void RestartDelayed()
    {
        currentLevel = Instantiate(_poolObject.getPool().GetItem(LevelUtils.GetCurrentLevel(false)), _levelParent);
    }
    
    private void CheckScreenForCoolButton()
    {
        if (Screen.width < 1080)
        {
            _buttonGameObject.transform.localScale = new Vector3(0.7f,0.7f,0.7f);
        }
    }

    public void ActivateOrNotPromptButton(bool isActivate)
    {
        tint.gameObject.SetActive(isActivate);
    }
    
    public void SetCameraAndBackgroundParameters()
    {
        Camera.main.transform.DOMove(new Vector3(0, 0, -10f), 0.15f);
        back.transform.DOMove(new Vector3(0, 0, 0), 0.15f);
        if (!Camera.main.orthographic)
        {
            Camera.main.orthographic = true;
        }
        Camera.main.orthographicSize = 5f;
    }

    private void CreateLevel()
    {
        Context.InterDetect.IsAfter30Level = LevelUtils.GetCurrentLevel() >= 30;
        Context.InterDetect.CountLevelsToShowInterstitial();
        currentLevel = Instantiate(_poolObject.getPool().GetItem(LevelUtils.GetCurrentLevel()), _levelParent);
        _numOfLevel = LevelUtils.GetCurrentLevel();
    }

    public void ActivateBackBtn()
    {
        btn_back.interactable = true;
    }
}