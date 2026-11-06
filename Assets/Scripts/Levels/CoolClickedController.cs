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
using Random = UnityEngine.Random;

public class CoolClickedController : MonoBehaviour, IClickItem
{
    [SerializeField] private GameObject _thisPrefab;
    private SpriteRenderer _spriteRenderer;
    private bool isSoundPlayed;

    [HideInInspector] public AnimCoolEnum type { get; private set; }
    
    private AnimationLevelCool animController;
    private Vector3 _startPos;
    private Vector3 _startScale;

    private bool _isCoolBtnMoving = true;
    private bool isFirst = true;
    
    private void OnEnable()
    {
        Messenger.AddListener(Messages.COOL_BUTTON_ON_SCENE_NEED_RESET, ResetButton);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(Messages.COOL_BUTTON_ON_SCENE_NEED_RESET, ResetButton);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(this);
    }

    private void Start()
    {
        animController = FindObjectOfType<AnimationLevelCool>();
        GetComponent<ClickController>().enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startPos = transform.position;
        _startScale = transform.localScale;
        isFirst = true;
    }

    public void tapOnItem()
    {
        if (!_isCoolBtnMoving)
        {
            _isCoolBtnMoving = true;
            GetComponent<ClickController>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Sequence animClick = DOTween.Sequence();

            animClick.Append(_spriteRenderer.DOFade(0, 0.1f).SetEase(Ease.Linear));
            animClick.Join(transform.DOScale(new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z * 1.1f), 0.1f));

            animClick.Append(_spriteRenderer.DOFade(1f, 0.1f).SetEase(Ease.Linear));
            animClick.Join(transform.DOScale(new Vector3(_startScale.x, _startScale.y, _startScale.z), 0.1f));

            animClick.OnComplete((() =>
            {
                RandomType();
                animController.SetupAnimCool();
                animClick.Kill();
            }));  
        }
    }

    private void RandomType()
    {
        var rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                type = AnimCoolEnum.FIRST;
                break;
            case 1:
                type = AnimCoolEnum.SECOND;
                break;
            case 2:
                type = AnimCoolEnum.THIRD;
                break;
            case 3:
                type = AnimCoolEnum.FOURTH;
                break;
        }
    }

    private void ResetButton()
    {
        transform.localPosition = _startPos;
        transform.DOScale(_startScale, 0f);
        GetComponent<ClickController>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().simulated = true;
        isFirst = true;
        _thisPrefab.SetActive(false);
        isSoundPlayed = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isSoundPlayed)
        {
            isSoundPlayed = true;
            Context.SoundPlayer.PlaySoundClick("ts_2", AudioMixerController.MasterMixerGroup.MASTER, 1);
        }
        if (isFirst)
        {
            _isCoolBtnMoving = false;
            GetComponent<ClickController>().enabled = true;
            isFirst = false;
        }
    }
}
