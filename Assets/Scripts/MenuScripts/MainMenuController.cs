using System;
using App;
using Library.Common;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MenuScripts
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button play;
        [SerializeField] private Button settings;
        [SerializeField] private Button levels;

        [SerializeField] private RectTransform parentForDialog;

        [SerializeField] private TextMeshProUGUI numCurLevel;
        
        private AnimationController _animationController;
        
        void Start()
        {
            Context.DataHelper.SetLong(AppConstans.LEVEL_STATUS + 1, 1);
            numCurLevel.text = Context.DataHelper.GetLong(AppConstans.LAST_OPEN_LEVEL_INDEX).ToString();
            MintDialogsContainer._instance.SetParent(parentForDialog);
            MenuUtils.OpenMainMenuOrSelectLevelDialog();
            _animationController = FindObjectOfType<AnimationController>();
            _animationController.StartButtonsAnimation();
            
            PlayerPrefs.SetInt("switchMusicOff", 0);
            PlayerPrefs.SetInt("switchVibrOn", 1);
            
            play.onClick.AddListener(() =>
            {
                Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                FindObjectOfType<AnimationController>().LoadGameSceneWithAnimation();
                play.interactable = false; 
                LevelUtils.SetCurrentLevel(Convert.ToInt32(numCurLevel.text));
                PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 0);
            });
            
            settings.onClick.AddListener(() =>
            {
                Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                _animationController.ShowSettingsWithAnimation();
                settings.interactable = false;
            });
            
            levels.onClick.AddListener(() =>
            {
                Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
                _animationController.ShowSelectLevelsWithAnimation();
                levels.interactable = false;
            });
        }
        
        public void ActivateButtons()
        {
            settings.interactable = true;
            levels.interactable = true;
            play.interactable = true;
        }
        
        public void SetCurrentLevelTextField()
        {
            numCurLevel.text = Context.DataHelper.GetLong(AppConstans.LAST_OPEN_LEVEL_INDEX).ToString();
        }
    }
}
