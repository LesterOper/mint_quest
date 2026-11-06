using System;
using System.Collections;
using System.Collections.Generic;
using Swimmer.Library.Common;
using Swimmer.Library.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelProgressElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lvlText;
    [SerializeField] private Image _lvlImage;
    [SerializeField] private Button _lvlButton;
    [Space] 
    [SerializeField] private Sprite _unlockedSprite;
    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Color _lockedColor;
    
    private int _lvlIndex;
    private bool _isComplete;
    
    private void Start()
    {
        _lvlButton.onClick.AddListener(LoadLevelClick);
    }

    public void SetupLevel(int lvlIndex, bool isComplete)
    {
        _lvlIndex = lvlIndex;
        _isComplete = isComplete;
        if (LevelUtils.IsLevelOpen(lvlIndex) && LevelUtils.GetLastOpenLevel() == lvlIndex)
        {
            _lvlImage.sprite = _lockedSprite;
            _lvlText.text = "" + lvlIndex;
        }
        else if (isComplete)
        {
            _lvlImage.sprite = _unlockedSprite;
            _lvlText.text = "" + lvlIndex;
        }
        else
        {
            _lvlImage.sprite = _lockedSprite;
            _lvlText.text = "" + lvlIndex;
            _lvlText.color = _lockedColor;
            _lvlButton.interactable = false;
        }
    }
    
    private void LoadLevelClick()
    {
        if (_isComplete)
        {
            Context.SoundPlayer.PlaySoundClick("ts3");
            //PlayerPrefs.SetInt(AppConstans.LEVEL_INDEX_SELECTED, _lvlIndex);
            Context.DataHelper.SetLong(AppConstans.CURRENT_LEVEL, _lvlIndex);
            PlayerPrefs.SetInt(AppConstans.WHERE_DID_WE_LOAD_THE_LEVEL, 1);
            SceneManager.LoadScene("level_scene");
        }
    }

    public int GetRandomPosition()
    {
        int xRandPos = (int)ContextManager.DataHelper.GetLong(AppConstans.LEVEL_SELECT_DIALOG_RANDOM_POSITION_ELEMENT + _lvlIndex);
        if (xRandPos != 0)
        {
            return xRandPos;
        }
        else
        {
            if (Random.value > 0.5f)
            {
                int value = Random.Range(-280, 0);
                ContextManager.DataHelper.SetLong(AppConstans.LEVEL_SELECT_DIALOG_RANDOM_POSITION_ELEMENT + _lvlIndex, value);
                return value;
            }
            else
            {
                int value = Random.Range(1, 281);
                ContextManager.DataHelper.SetLong(AppConstans.LEVEL_SELECT_DIALOG_RANDOM_POSITION_ELEMENT + _lvlIndex, value);
                return value;
            }
        }
    }
}
