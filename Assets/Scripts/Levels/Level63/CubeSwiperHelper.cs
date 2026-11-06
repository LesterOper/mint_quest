using System;
using System.Collections;
using System.Collections.Generic;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;

public class CubeSwiperHelper : MonoBehaviour
{
    [SerializeField] private BoxCollider2D swipeTrigger;
    [SerializeField] private List<SpriteRenderer> cubesEdges;
    [SerializeField] private List<string> directions;
    private List<SpriteRenderer> bufEdges;
    private List<string> bufDir;
    private SpriteRenderer _currentEdge;
    private string currentDir;

    private void Start()
    {
        bufEdges = new List<SpriteRenderer>(cubesEdges);
        bufDir = new List<string>(directions);
        _currentEdge = bufEdges[0];
        currentDir = bufDir[0];
    }

    public void ChechDirectionSwipe(string dir)
    {
        if (dir == currentDir)
        {
            Success();
        }
        else
        {
            Context.SoundPlayer.PlaySoundClick("error");
            ResetEdges();
        }
    }

    private void Success()
    {
        _currentEdge.DOFade(1, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
        {
            bufEdges.RemoveAt(0);
            bufDir.RemoveAt(0);
            
            if (bufEdges.Count == 0)
            {
                swipeTrigger.enabled = false;
                Invoke(nameof(Complete), 0.7f);
                return;
            }
            _currentEdge = bufEdges[0];
            currentDir = bufDir[0];
        });
    }

    private void Complete()
    {
        _currentEdge.transform
            .DOScale(new Vector3(10, 10, 10), 0.3f)
            .SetEase(Ease.Linear)
            .OnComplete(() => { Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM); });
    }

    public void ResetEdges()
    {
        bufEdges = new List<SpriteRenderer>(cubesEdges);
        bufDir = new List<string>(directions);
        bufEdges.ForEach(obj => { obj.DOFade(0, 0.25f).SetEase(Ease.Linear); });
        currentDir = bufDir[0];
        _currentEdge = bufEdges[0];
    }
}