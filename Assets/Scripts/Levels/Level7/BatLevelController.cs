using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class BatLevelController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animation anim;
    public void OnPointerClick(PointerEventData eventData)
    {
        Context.SoundPlayer.PlaySoundClick("bat_2", AudioMixerController.MasterMixerGroup.SFX, 1);
        setupSpecialEffect();
    }

    private void setupSpecialEffect()
    {
        GetComponent<PolygonCollider2D>().enabled = false;
            
        var pos = transform;
        anim.Play();
        Invoke(nameof(Effect), 1.1f);
    }

    private void Effect()
    {
        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
    }
}
