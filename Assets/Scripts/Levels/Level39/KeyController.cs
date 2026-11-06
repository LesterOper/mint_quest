using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Levels;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class KeyController : MonoBehaviour, IDraggableItems
{
    [SerializeField] private GameObject spriteMask;
    [SerializeField] private GameObject backDoor;
    [SerializeField] private GameObject mintBackDoor;
    [SerializeField] private SpriteRenderer mat;
    [SerializeField] private SpriteRenderer keyHole;

    private void Start()
    {
        Camera.main.orthographic = false;
    }

    public void moveItem(Vector2 pos)
    {
        Context.SoundPlayer.PlaySoundClick("right_do", AudioMixerController.MasterMixerGroup.SFX, 1);
        transform.DOMove(pos, 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Context.SoundPlayer.PlaySoundClick("key_1", AudioMixerController.MasterMixerGroup.SFX, 1);
            Invoke(nameof(SpecialEffect), 0.25f);
        });
    }

    public void attachToParentObject()
    {
    }

    private void SpecialEffect()
    {
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(backDoor.transform.DORotate(new Vector3(0, -107f, 0), 0.8f).SetEase(Ease.Linear));
        sequence.Join(GetComponent<SpriteRenderer>().DOFade(0, 0.35f)).SetEase(Ease.Linear);
        sequence.Join(keyHole.DOFade(0, 0.35f).SetEase(Ease.Linear));
        sequence.Join(mat.DOFade(0, 0.35f).SetEase(Ease.Linear));
        sequence.InsertCallback(0.8f, () => Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM));
    }
}
