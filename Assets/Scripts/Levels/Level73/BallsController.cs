using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

public class BallsController : MonoBehaviour
{
    [SerializeField] private GameObject hole;
    [SerializeField] private CircleCollider2D _collider2D;
    private bool isInHole;

    private DragHole _dragHole;
    // Start is called before the first frame update
    void Start()
    {
        _dragHole = hole.GetComponent<DragHole>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isInHole) return;
        var pos = transform.localPosition;

        if (Vector2.Distance(pos, hole.transform.localPosition) <= 0.3f)
        {
            isInHole = true;
            transform.DOMove(hole.transform.localPosition, 0.15f).SetEase(Ease.Linear);
            transform.DOScale(new Vector3(0, 0, 0), 0.25f).SetEase(Ease.Linear);
            _collider2D.enabled = false;
            _dragHole.IncrementBallsCounter();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Context.SoundPlayer.PlaySoundClick("billiards_2", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        Context.SoundPlayer.PlaySoundClick("ts3", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
    }
}
