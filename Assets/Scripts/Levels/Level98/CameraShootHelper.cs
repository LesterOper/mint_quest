using System;
using System.Collections;
using System.Collections.Generic;
using App;
using DG.Tweening;
using Swimmer.Library.Common;
using UnityEngine;

public class CameraShootHelper : MonoBehaviour
{
    [Header("Front objects")]
    [SerializeField] private List<SpriteRenderer> frontObjects;
    [Space]
    [Header("Back objects")]
    [SerializeField] private List<SpriteRenderer> backObjects;

    [Space] [Header("Shoot button")] 
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private GameObject shoot;
    [SerializeField] private float xPosF, xPosB;
    [Space]
    [Header("Parameters for rot btn")]
    [SerializeField] private CircleCollider2D fRot, sRot;
    [SerializeField] private SpriteRenderer fRenderer, sRenderer;
    [SerializeField] private Sprite fActive, fDeactive, sActive, sDeactive;

    [Space] [Header("Switch btn")] 
    [SerializeField] private CapsuleCollider2D _capsuleCollider2D;
    [SerializeField] private SpriteRenderer switcher;
    [SerializeField] private Sprite active, deactive;

    [Space] 
    [Header("Shutters")] 
    [SerializeField] private List<GameObject> shutters;
    [SerializeField] private List<Vector2> positions;
    private List<Vector2> startPos;
    private bool isFrontActive = true;
    private bool isActiveSwitch = false;

    private void Start()
    {
        startPos = new List<Vector2>();
        shutters.ForEach(obj =>
        {
            startPos.Add(new Vector2(obj.transform.position.x, obj.transform.position.y));
        });
    }

    public void ActivateSide()
    {
        //Context.SoundPlayer.PlaySoundClick("foto_turn_1", AudioMixerController.MasterMixerGroup.SFX, PlayerPrefs.GetFloat(AppConstans.KEY_VOLUME));
        frontObjects.ForEach(obj => { obj.DOFade(isFrontActive ? 0 : 1, 0.15f).SetEase(Ease.Linear); });
        backObjects.ForEach(obj => { obj.DOFade(isFrontActive ? 1 : 0, 0.15f).SetEase(Ease.Linear); });
        shoot.transform.DOLocalMoveX(isFrontActive ? xPosF : xPosB, 0.15f).SetEase(Ease.Linear);
        _capsuleCollider2D.enabled = isFrontActive;
        _collider2D.enabled = isActiveSwitch && !isFrontActive;
        isFrontActive = !isFrontActive;
        
        ChangeStateButton(isFrontActive);
    }
    
    private void ChangeStateButton(bool isActive)
    {
        fRot.enabled = isActive;
        fRenderer.sprite = isActive ? fActive : fDeactive;
        sRot.enabled = !isActive;
        sRenderer.sprite = isActive ? sDeactive : sActive;
    }


    public void ObjectiveActive()
    {
        for (int i = 0; i < shutters.Count; i++)
        {
            shutters[i].transform
                .DOLocalMove(
                    new Vector3(!isActiveSwitch ? positions[i].x : startPos[i].x,
                        !isActiveSwitch ? positions[i].y : startPos[i].y), 0.15f).SetEase(Ease.Linear);
        }

        switcher.sprite = isActiveSwitch ? deactive : active;
        isActiveSwitch = !isActiveSwitch;
    }
}
