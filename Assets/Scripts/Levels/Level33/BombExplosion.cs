using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using TMPro;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject bombWithChilds;
    [SerializeField] private GameObject explosion;
    private int countCutWire = 0;
    private void Start()
    {
        InitEffect();
        StartCoroutine(Timer());
    }
    
    public void IncrementAndCheckCountCutWire()
    {
        countCutWire++;
    }

    private void Restart()
    {
        FindObjectOfType<GameController>().RestartLevel();
    }

    private void DefeatEffect()
    {
        Invoke(nameof(StartDestroyingBomb), 0.6f);
    }

    private void StartDestroyingBomb()
    {
        bombWithChilds.transform.DOScale(new Vector3(0, 0, 1), 0.55f).SetEase(Ease.Linear);
        bombWithChilds.transform
            .DORotate(new Vector3(0, 0, 720f), 0.6f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
        Invoke(nameof(Restart), 1.2f);
    }
    
    private IEnumerator Timer()
    {
        int time = 20;
        while (time > 0)
        {
            time--;
            timer.DOFade(0.2f, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
            {
                if (time >= 10)
                {
                    timer.text = "0:" + time;
                }
                else
                    timer.text = "0:0" + time;
                timer.DOFade(1, 0.15f).SetEase(Ease.Linear);
            });
            yield return new WaitForSeconds(1);
        }
        
        if(countCutWire == 0) 
            CompleteLevelEffect();
        else 
            DefeatEffect();
    }

    private void CompleteLevelEffect()
    {
        var bomb = GameObject.Find("bomb");

        Sequence sequence = DOTween.Sequence();
        sequence.InsertCallback(0.6f, () =>
        {
            Context.SoundPlayer.PlaySoundClick("bomb", AudioMixerController.MasterMixerGroup.MASTER, 1);
            bomb.SetActive(false);
            explosion.SetActive(true);
        });
        sequence.Join(explosion.transform.DOScale(new Vector3(4f, 4f, 4f), 0.7f));
        sequence.OnComplete(() => Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL));
    }

    private void InitEffect()
    {
        bombWithChilds.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), 0.75f).SetEase(Ease.Linear);
        bombWithChilds.transform.DORotate(new Vector3(0, 0, 720f), 0.75f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
    }
}
