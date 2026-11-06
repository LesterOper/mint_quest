using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Levels;
using Swimmer.Library.Common;
using UnityEngine;

public class BombController : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject cut_wire;
    [SerializeField] private Animation scissors;

    public void tapOnItem()
    {
        string name = gameObject.name;

        if (name == "bomb")
        {
            SetBombEffect();
        }
        else
        {
            scissors.gameObject.SetActive(true);
            Context.SoundPlayer.PlaySoundClick("scissors_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
            Vector2 pos = Camera.main.ScreenToWorldPoint(GetComponent<ClickController>().MousePos);
            scissors.transform.position = new Vector3(pos.x, pos.y, 0);
            scissors.Play();
            Invoke(nameof(SetConductorEffect), 0.7f); 
        }
    }

    private void SetConductorEffect()
    {
        scissors.gameObject.SetActive(false);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear));
        sequence.InsertCallback(0.15f, () =>cut_wire.SetActive(true));
        FindObjectOfType<BombExplosion>().IncrementAndCheckCountCutWire();
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void SetBombEffect()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(new Vector3(0.8f, 0.8f), 0.15f).SetEase(Ease.Linear));
        sequence.Append(transform.DOScale(new Vector3(0.7f, 0.7f), 0.15f).SetEase(Ease.Linear));
    }
}
