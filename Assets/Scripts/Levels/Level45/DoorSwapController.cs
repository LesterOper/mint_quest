using System;
using System.Collections;
using System.Collections.Generic;
using App;
using App.Global;
using DG.Tweening;
using Swimmer.Library;
using Swimmer.Library.Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoorSwapController : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;
    [SerializeField] private Vector2[] pos;
    [SerializeField] private GameObject openDoor;
    [SerializeField] private Transform mintBack;
    [SerializeField] private List<SpriteRenderer> circles;
    private bool isShuffle;
    private int countDetected = 0;

    public bool IsShuffle
    {
        get => isShuffle;
        set => isShuffle = value;
    }

    private void Start()
    {
        Invoke(nameof(StartShuffle), 0.5f);
    }

    private void StartShuffle()
    {
        isShuffle = true;
        openDoor.transform.DOLocalRotate(new Vector3(0f, -60f, 0f), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
            {
                openDoor.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        Invoke(nameof(DoorSwap), 0.3f);
                    });
            });
    }
    
    public void DoorSwap()
    {
        float time = 0.35f;
        List<Vector2> poss = new List<Vector2>(pos);
        int rand;
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < 6; i++)
        {
            sequence.Append(doors[0].transform.DOMoveY(-2.55f, time).SetEase(Ease.Linear));
            sequence.Join(doors[1].transform.DOMoveY(-2.55f, time).SetEase(Ease.Linear));
            sequence.Join(doors[2].transform.DOMoveY(-2.55f, time).SetEase(Ease.Linear));
            
            for (int k = 0; k < 3; k++)
            {
                rand = Random.Range(0, poss.Count);
                sequence.Append(doors[k].transform.DOMove(poss[rand], time).OnComplete(() => Context.SoundPlayer.PlaySoundClick("turn_move_1")));
                poss.RemoveAt(rand);
            }
            poss = new List<Vector2>(pos);
            time -= 0.05f;
        }

        sequence.OnComplete(() => isShuffle = false);
    }

    public void IncrementAndCheckCountDetected()
    {
        countDetected++;
        circles[countDetected - 1].DOFade(0, 0.2f).SetEase(Ease.Linear);
        if (countDetected == 3)
        {
            foreach (GameObject door in doors)
            {
                door.transform.DOMoveX(0f, 0.3f).SetEase(Ease.Linear);
                door.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear);
                door.GetComponent<BoxCollider2D>().enabled = false;
            }

            openDoor.transform.DORotate(new Vector3(0f, -110f, 0f), 0.3f).OnComplete(() =>
                {
                    mintBack.DOScale(new Vector3(6f, 6f, 6f), 0.3f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        Messenger.Broadcast(Messages.ACTIVATE_COOL_BUTTOM);
                    });
                });
            openDoor.GetComponent<SpriteRenderer>().DOFade(0, 0.3f).SetEase(Ease.Linear);
        }
        else
        {
            StartShuffle();
        }
    }

    public void ResetAccordingMistake()
    {
        countDetected = 0;
        circles.ForEach(obj => { obj.DOFade(1, 0.2f).SetEase(Ease.Linear); });
        StartShuffle();
    }
}
