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

public class Level_10 : MonoBehaviour, IClickItem
{
    [SerializeField] public Sprite _colorfull;
    private Rigidbody2D _rigidbody;
    private bool isSoundPlayed;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void tapOnItem()
    {
        transform.DOScale(new Vector2(0.5f, 0.5f), 0.2f).OnComplete(() =>
        {
            GetComponent<SpriteRenderer>().sprite = _colorfull;
            transform.DOScale(new Vector2(1f, 1f), 0.2f).OnComplete(() => { _rigidbody.constraints = RigidbodyConstraints2D.None; });
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<GameController>().CoolButtonLevelCompleteEffect();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, 1);
    }
}
