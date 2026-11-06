using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level_27 : MonoBehaviour
{
    [SerializeField] private List<Transform> _transformsCircles;
    [SerializeField] private Transform[] _endTransforms;
    [SerializeField] private bool[] _isComplete;
    [Space]
    [SerializeField] private SpriteRenderer _rectangle_fill;

    [Header("Config Complete Animation")]
    [SerializeField] private float _duration = 1.5f;
    [SerializeField] private int _turns = 5;

    private int _circleCount = 0;
    private int index = 0;
    private GameObject curr;
    [SerializeField] private List<Vector2> startPos;

    private void Start()
    {
        curr = _transformsCircles[index].gameObject;
    }

    public void CheckCircles(GameObject curr)
    {
        if (curr == this.curr)
        {
            _circleCount++;
            _transformsCircles[index].GetComponent<DragController>().enabled = false;
            _transformsCircles[index].GetComponent<CircleCollider2D>().enabled = false;
            _transformsCircles[index].transform.DOScale(new Vector3(1.25f,1.25f,1.25f), 0.05f);
            index++;
            if (_circleCount == _transformsCircles.Count)
            {
                _rectangle_fill.DOFade(1, 1f).SetEase(Ease.Linear);
                Invoke(nameof(CompleteAnimations), 1.2f);
            }

            this.curr = _transformsCircles[index].gameObject;
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error", AudioMixerController.MasterMixerGroup.SFX, 1);
            _circleCount = 0;
            index = 0;
            this.curr = _transformsCircles[index].gameObject;
            ResetAllCircles();
        }
    }
    
    private void CompleteAnimations()
    {
        Sequence anim = DOTween.Sequence();

        anim.Append(_rectangle_fill.transform.DORotate(new Vector3(0, 0, 360) * _turns, _duration, RotateMode.FastBeyond360));
        anim.Join(_rectangle_fill.transform.DOScale(new Vector3(12, 12, 12), _duration));
        anim.InsertCallback(0.3f, () => Messenger.Broadcast(Messages.COOL_EFFECT_COMPLETE_LEVEL));
    }

    private void ResetAllCircles()
    {
        for (int i = 0; i < _transformsCircles.Count; i++)
        {
            _transformsCircles[i].DOLocalMove(startPos[i], 0.15f).SetEase(Ease.Linear);
            _transformsCircles[i].DOScale(1, 0.15f).SetEase(Ease.Linear);
            _transformsCircles[i].GetComponent<CircleCollider2D>().enabled = true;
            _transformsCircles[i].GetComponent<DragController>().enabled = true;
        }
    }

}