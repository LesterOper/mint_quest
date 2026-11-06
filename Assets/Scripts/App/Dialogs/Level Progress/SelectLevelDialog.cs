using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MenuScripts;
using Swimmer.Library.Common;
using Swimmer.Library.Dialogs;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SelectLevelDialog : Dialog
{
    [SerializeField] private Button _back;
    [SerializeField] private LevelProgressElement _lvlElementPrefab;
    [SerializeField] private Transform _levelsParent;
    [SerializeField] int _levelCount;
    [SerializeField] private float _startY = -640;
    [SerializeField] private float _pointDistance = 150;

    private List<GameObject> levels = new List<GameObject>();
    private GameObject curLvl;


    protected override void Start()
    {
        base.Start();
        LoadLevelsProgress();
        //_back.onClick.AddListener(() =>  Context.SoundPlayer.PlaySoundClick("click_2"));
    }

    public void LoadLevelsProgress()
    {
        for (int index = 1; index <= _levelCount; index++)
        {
            curLvl = Instantiate(_lvlElementPrefab.gameObject, _levelsParent);
            levels.Add(curLvl);

            int randX = Random.Range(-290, 291);
            if (randX > 0)
            {
                randX = +1000;
            }
            else
            {
                randX = -1000;
            }

            var lvlElement = curLvl.GetComponent<LevelProgressElement>();
            
            lvlElement.SetupLevel(index, LevelUtils.IsLevelOpen(index));
            curLvl.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(randX, _startY + (_pointDistance * index), 0);
            curLvl.GetComponent<RectTransform>()
                .DOAnchorPos(new Vector3(lvlElement.GetRandomPosition(), _startY + (_pointDistance * index), 0), 0.5f)
                .SetEase(Ease.Linear);

            
        }
    }
    

    public override void Hide()
    {
       // Context.SoundPlayer.PlaySoundClick("click_2");
        levels.Clear();
        FindObjectOfType<AnimationController>().StartButtonsAnimation();
        FindObjectOfType<MainMenuController>().ActivateButtons();
        
        base.Hide();
    }
}
