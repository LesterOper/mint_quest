using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class CoinsController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private CoinsCollector _collector;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        //Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        transform.DOMove(new Vector3(pos.x, pos.y - 0.5f), 0.15f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                _collector.CheckCoins(gameObject); 
                GetComponent<CircleCollider2D>().enabled = true;
            });
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }
}
