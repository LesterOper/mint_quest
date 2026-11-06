using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class KeyFragmentsController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject spriteMask;
    [SerializeField] private KeyFragmentsHelper _helper;
    [SerializeField] private Vector2 endPos;
    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.NONE, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _helper.CheckDragedFragment(gameObject);
        });
        if(spriteMask != null) 
            spriteMask.SetActive(true);
        
    }

    public void attachToParentObject()
    {
        throw new System.NotImplementedException();
    }

    public void Effect()
    {
        transform.DOLocalMove(endPos, 0.3f).SetEase(Ease.Linear);
    }
}
